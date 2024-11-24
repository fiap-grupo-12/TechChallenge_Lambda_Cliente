using AutoMapper;
using FIAP.TechChallenge.LambdaCliente.Application.UseCases;
using FIAP.TechChallenge.LambdaCliente.Application.UseCases.Interfaces;
using FIAP.TechChallenge.LambdaCliente.Domain.Repositories;
using FIAP.TechChallenge.LambdaCliente.Infra.Configurations;
using FIAP.TechChallenge.LambdaCliente.UnitTests.Resources;
using FluentAssertions;
using Moq;
using Xunit;

namespace FIAP.TechChallenge.LambdaCliente.UnitTests.UseCases
{
    public class ObterClientesUseCaseTests
    {
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;
        private readonly Mock<IObterClientesUseCase> _useCase;
        private readonly IMapper _mapper;

        public ObterClientesUseCaseTests()
        {
            _clienteRepositoryMock = new Mock<IClienteRepository>();
            _mapper = new MapperConfiguration(c => c.AddProfile<MapperConfig>()).CreateMapper();
        }

        [Fact]
        public async void ObterTodosOsClientes_WithSuccess()
        {
            var clienteMock = ClienteMock.GetClienteMock();

            _clienteRepositoryMock
                .Setup(x => x.GetAll())
                .ReturnsAsync(clienteMock);

            var useCase = new ObterClientesUseCase(_clienteRepositoryMock.Object, _mapper);

            var result = await useCase.Execute();

            Assert.NotNull(result);
            result.Count.Should().Be(2);
            result.Should().BeEquivalentTo(clienteMock);
        }
    }
}
