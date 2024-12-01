using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2;
using Amazon.Lambda.APIGatewayEvents;
using TechTalk.SpecFlow;
using Amazon.Lambda.TestUtilities;
using FIAP.TechChallenge.LambdaCliente.Api;
using FIAP.TechChallenge.LambdaCliente.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP.TechChallenge.LambdaCliente.IntegrationTests.steps
{
    [Binding]
    public class BuscarClientesSteps
    {
        private readonly ScenarioContext _context;
        private readonly AmazonDynamoDBClient _dynamoDbClient;
        private APIGatewayProxyResponse _response;
        private Function _function;

        public BuscarClientesSteps(ScenarioContext context)
        {
            _context = context;

            var serviceProvider = new ServiceCollection()
                .AddSingleton<ClienteRepository>()
                .AddSingleton<Function>()
                .BuildServiceProvider();

            // Resolvendo a função Lambda a partir do DI
            _function = serviceProvider.GetService<Function>();
        }

        [Given(@"um cliente com CPF ""(.*)"" está cadastrado no DynamoDB")]
        public async Task GivenUmClienteComCPF(string cpf)
        {
            var request = new PutItemRequest
            {
                TableName = "ClienteTable",
                Item = new Dictionary<string, AttributeValue>
            {
                { "id", new AttributeValue { S = "1234" } },
                { "cpf", new AttributeValue { S = cpf } },
                { "nome", new AttributeValue { S = "João Silva" } }
            }
            };

            await _dynamoDbClient.PutItemAsync(request);
        }

        [When(@"eu faço uma requisição GET para ""(.*)""")]
        public async Task WhenEuFacoUmaRequisicaoGETPara(string path)
        {
            // Criar o request para a função Lambda
            var request = new APIGatewayProxyRequest
            {
                Path = path,
                HttpMethod = "GET",
                PathParameters = new Dictionary<string, string> { { "cpf", path.Split('/').Last() } }
            };

            var context = new TestLambdaContext();

            // Chamar a função Lambda
            _response = await _function.Handler(request, context);
        }

        [Then(@"o status da resposta deve ser (.*)")]
        public void ThenOStatusDaRespostaDeveSer(int statusCode)
        {
            // Verificar o status da resposta
            Assert.Equals(statusCode, _response.StatusCode);
        }

        //[Then(@"o corpo da resposta deve conter ""(.*)"" e ""(.*)""")]
        //public void ThenOCorpoDaRespostaDeveConter(string key, string value)
        //{
        //    var responseBody = JsonConvert.DeserializeObject<Dictionary<string, string>>(_response.Body);
        //    Assert.Equals(value, responseBody[key]);
        //}
    }
}
