using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace FiapEstoque.Messaging;

    public static class RabbitMqProducer
{
    public static void Publish<T>(
    T message)
    {
        var factory = new ConnectionFactory
        { HostName = "localhost" };
        using var conn =
        factory.CreateConnection();
        using var ch =
        conn.CreateModel();
        ch.QueueDeclare("item-criado",
        false, false, false, null);
        var json = JsonSerializer
        .Serialize(message);
        var body = Encoding.UTF8
        .GetBytes(json);
        ch.BasicPublish("",
        "item-criado", null, body);
    }
}