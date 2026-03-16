### Sobre API e HTTP

**3.1)** Explique a diferença entre os códigos HTTP 200, 201, 204 e 404. Em qual situação cada um é retornado no seu Controller?

200 - OK: Significa que a solicitação foi bem-sucedida.

201 Created: A requisição foi bem sucedida e um novo recurso foi criado como resultado.

204 No Content: A requisição foi um sucesso, mas não há conteúdo para retornar.

404 - Not Found: Indica que o recurso solicitado não existe ou a URL está incorreta.

**3.2)** O que o atributo `[ApiController]` faz? O que acontece se você enviar um JSON com o campo obrigatório vazio?

O atributo [ApiController] indica que o controller é usado para criar uma API e ativa recursos automáticos, como validação de dados enviados na requisição e respostas de erro padronizadas.

Se um JSON for enviado com um campo obrigatório vazio, a validação falha automaticamente e a API retorna HTTP 400 sem executar o método do controller.

**3.3)** Por que o método `GetById` retorna `NotFound()` em vez de retornar `null`? Qual a diferença para o cliente da API?

O método GetById retorna NotFound() para informar ao cliente da API que o recurso solicitado não existe.
Se o seu método retornasse null, o framework geralmente entenderia isso como um sucesso.

A principal diferença está na forma como o cliente interpreta o resultado:
NotFound() (404): indica claramente que o recurso com aquele ID não foi encontrado.
null (200): indica que a requisição foi bem-sucedida, mas sem deixar claro que o recurso não existe.

### Sobre Entity Framework Core

**3.4)** O que é o Change Tracker do EF Core? Explique o que acontece internamente quando você chama `_ctx.SeuDbSet.Add(objeto)` seguido de `SaveChangesAsync()`.

O Change Tracker do EF Core é um componente interno do DbContext responsável por monitorar as alterações feitas nas entidades.

Quando você chama \_ctx.SeuDbSet.Add(objeto) ele marca o objeto como novo para ser salvo no banco.
Quando executa await \_ctx.SaveChangesAsync() ele cria o comando SQL INSERT e salva o objeto no banco de dados.

**3.5)** Qual a diferença entre `FindAsync(id)` e `ToListAsync()`? Qual SQL cada um gera?

FindAsync(id): busca um registro específico pelo ID.
`SELECT \* FROM Tabela WHERE Id = @id`

ToListAsync(): busca todos os registros da tabela.
`SELECT \* FROM Tabela`

**3.6)** Por que usamos `EntityState.Modified` no PUT ao invés de buscar o objeto primeiro e alterar campo a campo?

### Sobre Mensageria

**3.7)** Qual a diferença entre comunicação síncrona e assíncrona? Dê um exemplo real (fora do projeto) de cada uma.

**3.8)** O que é o ACK (Acknowledge) no RabbitMQ? O que acontece se o Consumer processar a mensagem mas NÃO enviar o ACK?

**3.9)** Por que o `RabbitMqConsumer` herda de `BackgroundService` e não de `ControllerBase`? Qual a diferença de ciclo de vida?

**3.10)** Se o RabbitMQ estiver fora do ar no momento do POST, o que acontece? O produto é salvo no Oracle? A API retorna erro? Sugira uma melhoria para tratar esse caso.
