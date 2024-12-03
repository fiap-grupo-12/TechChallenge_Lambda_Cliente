using Amazon.DynamoDBv2.Model;
using AutoMapper;
using FIAP.TechChallenge.LambdaCliente.Application.Models.Input;
using FIAP.TechChallenge.LambdaCliente.Application.Models.Response;
using FIAP.TechChallenge.LambdaCliente.Domain.Entities;
using FIAP.TechChallenge.LambdaCliente.Infra.Configurations;
using Xunit;

namespace FIAP.TechChallenge.LambdaCliente.UnitTests.Infra;

public class AutoMapperTest
{
    private readonly IMapper _mapper;

    public AutoMapperTest()
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MapperConfig());
        });

        _mapper = mappingConfig.CreateMapper();
    }

    [Fact]
    public void Map_CriarClienteInput_To_Cliente()
    {
        var input = new CriarClienteInput
        {
            Nome = "Pedro Geromel",
            CPF = "19619300050",
            Email = "gerodeus@teste.com"
        };

        var result = _mapper.Map<Cliente>(input);

        Assert.Equal(input.Nome, result.Nome);
        Assert.Equal(input.CPF, result.CPF);
        Assert.Equal(input.Email, result.Email);
    }

    [Fact]
    public void Map_Cliente_To_CriarClienteInput()
    {
        var cliente = new Cliente
        {
            Nome = "Renato Gaúcho",
            CPF = "92957637006",
            Email = "renato@teste.com"
        };

        var result = _mapper.Map<CriarClienteInput>(cliente);

        Assert.Equal(cliente.Nome, result.Nome);
        Assert.Equal(cliente.CPF, result.CPF);
        Assert.Equal(cliente.Email, result.Email);
    }

    [Fact]
    public void Map_ClienteResponse_To_Cliente()
    {
        var response = new ClienteResponse
        {
            Nome = "Luiz Suares",
            CPF = "50671337084",
            Email = "teste@teste.com"
        };

        var result = _mapper.Map<Cliente>(response);

        Assert.Equal(response.Nome, result.Nome);
        Assert.Equal(response.CPF, result.CPF);
        Assert.Equal(response.Email, result.Email);
    }

    [Fact]
    public void Map_Cliente_To_ClienteResponse()
    {
        var cliente = new Cliente
        {
            Nome = "Maria Oliveira",
            CPF = "83957231051",
            Email = "teste@teste.com"
        };

        var result = _mapper.Map<ClienteResponse>(cliente);

        Assert.Equal(cliente.Nome, result.Nome);
        Assert.Equal(cliente.CPF, result.CPF);
        Assert.Equal(cliente.Email, result.Email);
    }
}
