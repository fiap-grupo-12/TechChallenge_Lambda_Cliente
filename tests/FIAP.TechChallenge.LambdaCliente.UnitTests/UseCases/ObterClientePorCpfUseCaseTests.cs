using AutoMapper;
using FIAP.TechChallenge.LambdaCliente.Application.UseCases;
using FIAP.TechChallenge.LambdaCliente.Application.UseCases.Interfaces;
using FIAP.TechChallenge.LambdaCliente.Domain.Repositories;
using FIAP.TechChallenge.LambdaCliente.Infra.Configurations;
using FIAP.TechChallenge.LambdaCliente.UnitTests.Resources;
using FluentAssertions;
using Moq;
using Xunit;

namespace FIAP.TechChallenge.LambdaCliente.UnitTests.UseCases;

public class ObterClientePorCpfUseCaseTests
{
    private readonly Mock<IClienteRepository> _clienteRepositoryMock;
    private readonly Mock<IObterClientePorCpfUseCase> _useCase;
    private readonly IMapper _mapper;

    public ObterClientePorCpfUseCaseTests()
    {
        _clienteRepositoryMock = new Mock<IClienteRepository>();
        _mapper = new MapperConfiguration(c => c.AddProfile<MapperConfig>()).CreateMapper();
    }


    [Theory]
    [InlineData("13272968024")]
    public async void ObterClientePorCnpj_WithSuccess(string cpf)
    {
        var clienteMock = ClienteMock.GetClienteMock();

        _clienteRepositoryMock.
            Setup(x => x.GetByCpf(It.IsAny<string>()))
            .ReturnsAsync(clienteMock[1]);

        var useCase = new ObterClientePorCpfUseCase(_clienteRepositoryMock.Object, _mapper);

        var result = await useCase.Execute(cpf);

        Assert.NotNull(result);
        result.Should().BeEquivalentTo(clienteMock[1]);
    }
}
