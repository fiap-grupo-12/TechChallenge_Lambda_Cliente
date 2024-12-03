using AutoMapper;
using FIAP.TechChallenge.LambdaCliente.Application.UseCases.Interfaces;
using FIAP.TechChallenge.LambdaCliente.Application.UseCases;
using FIAP.TechChallenge.LambdaCliente.Domain.Repositories;
using FIAP.TechChallenge.LambdaCliente.UnitTests.Resources;
using Moq;
using Xunit;
using FIAP.TechChallenge.LambdaCliente.Infra.Configurations;
using FIAP.TechChallenge.LambdaCliente.Domain.Entities;
using FluentAssertions;
using FIAP.TechChallenge.LambdaCliente.Api.Request;

namespace FIAP.TechChallenge.LambdaCliente.UnitTests.UseCases
{
    public class CriarClienteUseCaseTests
    {
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;
        private readonly Mock<IObterClientesUseCase> _useCase;
        private readonly IMapper _mapper;

        public CriarClienteUseCaseTests()
        {
            _clienteRepositoryMock = new Mock<IClienteRepository>();
            _mapper = new MapperConfiguration(c => c.AddProfile<MapperConfig>()).CreateMapper();
        }

        [Fact]
        public async void ObterTodosOsClientes_WithSuccess()
        {
            var request = new CriarClienteRequest
            {
                Nome = "Client Teste",
                CPF = "10927749041",
                Email = "teste@teste.com"
            };

            var clienteMock = ClienteMock.GetClienteMock().First();

            var clienteResponse = ClienteMock.GetClienteResponseMock().First();

            _clienteRepositoryMock
                .Setup(x => x.Post(It.IsAny<Cliente>()))
                .ReturnsAsync(clienteMock);

            var useCase = new CriarClienteUseCase(_clienteRepositoryMock.Object, _mapper);

            var input = request.ToInput();
            var result = await useCase.Execute(input);

            request.Should().BeEquivalentTo(input);
            Assert.NotNull(result);
            result.Should().BeEquivalentTo(clienteResponse);
            _clienteRepositoryMock.Verify(it => it.Post(It.IsAny<Cliente>()), Times.Once);
        }
    }
}
