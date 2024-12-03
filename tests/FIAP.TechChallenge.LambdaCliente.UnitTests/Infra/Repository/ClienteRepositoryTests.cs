using Amazon.DynamoDBv2.DataModel;
using FIAP.TechChallenge.LambdaCliente.Domain.Entities;
using FIAP.TechChallenge.LambdaCliente.Infra.Repositories;
using Moq;
using System;
using Xunit;

namespace FIAP.TechChallenge.LambdaCliente.UnitTests.Infra.Repository
{
    public class ClienteRepositoryTests
    {
        private readonly Mock<IDynamoDBContext> _mockContext;
        private readonly ClienteRepository _clienteRepository;

        public ClienteRepositoryTests()
        {
            _mockContext = new Mock<IDynamoDBContext>();
            _clienteRepository = new ClienteRepository(_mockContext.Object);
        }

        [Fact]
        public async Task GetAll_DeveRetornarClientes()
        {
            var clientes = new List<Cliente>
        {
            new Cliente { CPF = "12345678901", Nome = "Cliente 1" },
            new Cliente { CPF = "09876543210", Nome = "Cliente 2" }
        };

            var asyncSearchMock = new Mock<AsyncSearch<Cliente>>();
            asyncSearchMock.Setup(x => x.GetRemainingAsync(default))
                           .ReturnsAsync(clientes);

            _mockContext.Setup(x => x.ScanAsync<Cliente>(It.IsAny<IEnumerable<ScanCondition>>(), default))
                        .Returns(asyncSearchMock.Object);

            var result = await _clienteRepository.GetAll();

            Assert.Equal(2, result.Count);
            Assert.Contains(result, c => c.CPF == "12345678901");
        }

        [Fact]
        public async Task GetByCpf_DeveRetornarClliente_QuandoExistir()
        {
            var cpf = "12345678901";
            var cliente = new Cliente { CPF = cpf, Nome = "Cliente Teste" };
            var clientes = new List<Cliente> { cliente };

            var asyncSearchMock = new Mock<AsyncSearch<Cliente>>();
            asyncSearchMock.Setup(x => x.GetRemainingAsync(default))
                           .ReturnsAsync(clientes);

            _mockContext.Setup(x => x.ScanAsync<Cliente>(It.IsAny<IEnumerable<ScanCondition>>(), default))
                        .Returns(asyncSearchMock.Object);

            var result = await _clienteRepository.GetByCpf(cpf);

            Assert.NotNull(result);
            Assert.Equal(cpf, result.CPF);
        }

        [Fact]
        public async Task Post_DeveSalvarCliente_ComSucesso()
        {
            var cliente = new Cliente { CPF = "12345678901", Nome = "Cliente Novo" };

            _mockContext.Setup(x => x.SaveAsync(It.IsAny<Cliente>(), default))
                        .Returns(Task.CompletedTask);

            var result = await _clienteRepository.Post(cliente);

            Assert.NotNull(result);
            Assert.Equal(cliente.CPF, result.CPF);
        }

        [Fact]
        public async Task GetAll_ThrowException()
        {
            var asyncSearch = new MockAsyncSearch<Cliente>(new Exception("Erro simulado no DynamoDB"));

            _mockContext.Setup(x => x.ScanAsync<Cliente>(It.IsAny<IEnumerable<ScanCondition>>(), default))
                        .Returns(asyncSearch);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _clienteRepository.GetAll());
            Assert.Contains("Erro ao consultar clientes", exception.Message);
        }

        [Fact]
        public async Task GetByCpf_ThrowException()
        {
            // Arrange
            var asyncSearch = new MockAsyncSearch<Cliente>(new Exception("Erro simulado no DynamoDB"));

            _mockContext.Setup(x => x.ScanAsync<Cliente>(It.IsAny<IEnumerable<ScanCondition>>(), default))
                        .Returns(asyncSearch);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _clienteRepository.GetByCpf("12345678901"));
            Assert.Contains("Erro ao consultar cliente", exception.Message);
        }

        [Fact]
        public async Task Post_ThrowException()
        {
            var cliente = new Cliente { CPF = "12345678901", Nome = "Cliente Novo" };

            _mockContext.Setup(x => x.SaveAsync(It.IsAny<Cliente>(), default))
                        .ThrowsAsync(new Exception("Erro simulado no DynamoDB"));
            var exception = await Assert.ThrowsAsync<Exception>(() => _clienteRepository.Post(cliente));
            Assert.Contains("Erro ao cadastrar cliente", exception.Message);
        }
    }

    public class MockAsyncSearch<T> : AsyncSearch<T>
    {
        private readonly Exception _exception;

        public MockAsyncSearch(Exception exception)
        {
            _exception = exception;
        }

        public override Task<List<T>> GetRemainingAsync(CancellationToken cancellationToken = default)
        {
            if (_exception != null)
            {
                throw _exception;
            }

            return Task.FromResult(new List<T>());
        }
    }
}
