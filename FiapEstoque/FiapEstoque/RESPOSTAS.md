### Sobre API e HTTP

**3.1)** Explique a diferença entre os códigos HTTP 200, 201, 204 e 404. Em qual situação cada um é retornado no seu Controller?

200 - OK: Significa que a solicitação foi bem-sucedida.

201 - Created: A requisição foi bem sucedida e um novo recurso foi criado como resultado.

204 - No Content: A requisição foi um sucesso, mas não há conteúdo para retornar.

404 - Not Found: Indica que o recurso solicitado não existe ou a URL está incorreta.

**3.2)** O que o atributo `[ApiController]` faz? O que acontece se você enviar um JSON com o campo obrigatório vazio?

O ApiController indica que o controller é usado para criar uma API e ativa recursos automáticos, como validação de dados enviados na requisição e respostas de erro padronizadas.

Se um JSON for enviado com um campo obrigatório vazio, a validação falha e a API retorna HTTP 400 sem executar o método do controller.

**3.3)** Por que o método `GetById` retorna `NotFound()` em vez de retornar `null`? Qual a diferença para o cliente da API?

O método GetById retorna NotFound() para informar ao cliente da API que o recurso solicitado não existe.
Se o seu método retornasse null, o cliente entenderia isso como um sucesso.

A principal diferença é a forma que o cliente interpreta o retorno:
NotFound(): indica que o recurso com aquele ID não foi encontrado.<br>
null: indica que a requisição foi sucesso, mas sem indicar que o recurso não existe.

### Sobre Entity Framework Core

**3.4)** O que é o Change Tracker do EF Core? Explique o que acontece internamente quando você chama `_ctx.SeuDbSet.Add(objeto)` seguido de `SaveChangesAsync()`.

O Change Tracker do EF Core é um componente interno do DbContext responsável por monitorar as alterações feitas nas entidades.

Quando você chama \_ctx.SeuDbSet.Add(objeto) ele marca o objeto como novo para ser salvo no banco.<br>
Quando executa await \_ctx.SaveChangesAsync() ele cria o comando SQL INSERT e salva o objeto no banco de dados.

**3.5)** Qual a diferença entre `FindAsync(id)` e `ToListAsync()`? Qual SQL cada um gera?

FindAsync(id): busca um registro específico pelo ID.<br>
SELECT \* FROM Tabela WHERE Id = @id

ToListAsync(): busca todos os registros da tabela.<br>
SELECT \* FROM Tabela

**3.6)** Por que usamos `EntityState.Modified` no PUT ao invés de buscar o objeto primeiro e alterar campo a campo?

A principal razão é a performance e simplicidade, pois evita uma consulta de banco de dados antes da atualização.

### Sobre Mensageria

**3.7)** Qual a diferença entre comunicação síncrona e assíncrona? Dê um exemplo real (fora do projeto) de cada uma.

Comunicação síncrona: A requisição é enviada e o sistema espera a resposta para continuar.<br>
Exemplo: uma ligação, você fala e espera a outra pessoa responder.

Comunicação assíncrona: A requisição é enviada e o sistema não precisa esperar a resposta para continuar.<br>
Exemplo: enviar uma mensagem, você envia a mensagem e a outra pessoa responde depois.

**3.8)** O que é o ACK (Acknowledge) no RabbitMQ? O que acontece se o Consumer processar a mensagem mas NÃO enviar o ACK?

O ACK é uma confirmação enviada pelo consumidor ao broker, indicando que a mensagem foi processada.

Se o consumer não enviar o ACK a mensagem pode ser enviada novamente.

**3.9)** Por que o `RabbitMqConsumer` herda de `BackgroundService` e não de `ControllerBase`? Qual a diferença de ciclo de vida?

Porque ele precisa rodar continuamente em segundo plano e o BackgroundService faz isso, e o ControllerBase só executa quando tem requisições.

O ciclo de vida do BackgroundService dura enquanto a aplicação estiver rodando, e do ControllerBase é o tempo de cada requisição.

**3.10)** Se o RabbitMQ estiver fora do ar no momento do POST, o que acontece? O produto é salvo no Oracle? A API retorna erro? Sugira uma melhoria para tratar esse caso.

Se o RabbitMQ estiver fora do ar, a mensagem não é enviada, o produto pode ser salvo e a API pode retornar erro.

Uma melhoria seria salvar o produto no banco e a mensagem em uma tabela outbox no mesmo POST.
