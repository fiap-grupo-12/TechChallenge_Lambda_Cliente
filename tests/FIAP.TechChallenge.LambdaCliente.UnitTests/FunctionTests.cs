using Xunit;
using Moq;
using Amazon.Lambda.APIGatewayEvents;
using FIAP.TechChallenge.LambdaCliente.Api;
using FIAP.TechChallenge.LambdaCliente.Application.UseCases.Interfaces;
using FIAP.TechChallenge.LambdaCliente.Application.Models.Response;
using FIAP.TechChallenge.LambdaCliente.Api.Request;
using Amazon.Lambda.Core;
using FIAP.TechChallenge.LambdaCliente.Application.Models.Input;

namespace FIAP.TechChallenge.LambdaCliente.UnitTests
{
    public class FunctionTests
    {
        private readonly Mock<ICriarClienteUseCase> _mockCriarClienteUseCase;
        private readonly Mock<IObterClientesUseCase> _mockObterClientesUseCase;
        private readonly Mock<IObterClientePorCpfUseCase> _mockObterClientePorCpfUseCase;
        private readonly Function _function;

        public FunctionTests()
        {
            _mockCriarClienteUseCase = new Mock<ICriarClienteUseCase>();
            _mockObterClientesUseCase = new Mock<IObterClientesUseCase>();
            _mockObterClientePorCpfUseCase = new Mock<IObterClientePorCpfUseCase>();
            _function = new Function(_mockCriarClienteUseCase.Object, _mockObterClientesUseCase.Object, _mockObterClientePorCpfUseCase.Object);
        }

        [Fact]
        public async Task ObterClientePorCpf_ShouldReturnClienteResponse()
        {
            // Arrange
            var cpf = "12345678901";
            var expectedResponse = new ClienteResponse { Nome = "John Doe", CPF = cpf };

            _mockObterClientePorCpfUseCase.Setup(x => x.Execute(cpf)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _function.ObterClientePorCpf(cpf);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.Nome, result.Nome);
            Assert.Equal(expectedResponse.CPF, result.CPF);
        }

        [Fact]
        public async Task ObterTodosOsClientes_ShouldReturnListOfClienteResponse()
        {
            // Arrange
            var expectedResponse = new List<ClienteResponse>
        {
            new ClienteResponse { Nome = "Jane Doe", CPF = "98765432100" }
        };

            _mockObterClientesUseCase.Setup(x => x.Execute()).ReturnsAsync(expectedResponse);

            // Act
            var result = await _function.ObterTodosOsClientes();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(expectedResponse[0].Nome, result[0].Nome);
        }

        [Fact]
        public async Task CriarClientes_ShouldReturnClienteResponse()
        {
            // Arrange
            var request = new CriarClienteRequest { Nome = "New Client", CPF = "55566677788" };
            var expectedResponse = new ClienteResponse { Nome = request.Nome, CPF = request.CPF };

            _mockCriarClienteUseCase.Setup(x => x.Execute(It.IsAny<CriarClienteInput>())).ReturnsAsync(expectedResponse);

            // Act
            var result = await _function.CriarClientes(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.Nome, result.Nome);
            Assert.Equal(expectedResponse.CPF, result.CPF);
        }

        [Fact]
        public async Task Handler_ShouldReturn200Response_WhenRequestIsValid()
        {
            // Arrange
            var request = new APIGatewayProxyRequest
            {
                HttpMethod = "GET",
                Resource = "/Cliente",
                PathParameters = new Dictionary<string, string>()
            };
            var context = new Mock<ILambdaContext>().Object;
            _mockObterClientesUseCase.Setup(x => x.Execute()).ReturnsAsync(new List<ClienteResponse>());

            // Act
            var response = await _function.Handler(request, context);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(200, response?.StatusCode);
            Assert.NotEmpty(response?.Headers);
        }

        [Fact]
        public async Task Handler_ShouldReturn400Response_WhenExceptionIsThrown()
        {
            // Arrange
            var request = new APIGatewayProxyRequest { HttpMethod = "GET", Resource = "/Cliente" };
            var context = new Mock<ILambdaContext>().Object;

            _mockObterClientesUseCase.Setup(x => x.Execute()).ThrowsAsync(new Exception("Simulated error"));

            // Act
            var response = await _function.Handler(request, context);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(400, response?.StatusCode);
            Assert.Contains("Simulated error", response?.Body);
        }
    }
}