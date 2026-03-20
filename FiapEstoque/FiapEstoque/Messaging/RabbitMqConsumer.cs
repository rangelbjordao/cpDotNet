using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using FiapEstoque.Models;

namespace FiapEstoque.Messaging
{
    public class RabbitMqConsumer : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken ct)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            var conn = factory.CreateConnection();
            var ch = conn.CreateModel();
            ch.QueueDeclare("item-criado", false, false, false, null);
            var consumer = new EventingBasicConsumer(ch);
            consumer.Received += (s, e) => {
                var msg = Encoding.UTF8.GetString(e.Body.ToArray());
                Console.WriteLine($"[Consumer] Recebido: {msg}");

                var item = JsonSerializer.Deserialize<Item>(msg);
                if (item != null && item.Quantidade < 0)
                {
                    Console.WriteLine($"[Consumer] ALERTA: Item '{item.Nome}' com quantidade negativa!");
                }

                ch.BasicAck(e.DeliveryTag, false); // ACK manual
            };
            ch.BasicConsume("item-criado", false, consumer); // autoAck=false
            await Task.Delay(Timeout.Infinite, ct); // fica vivo
        }
    }
}
