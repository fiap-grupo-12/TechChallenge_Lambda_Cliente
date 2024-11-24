using FIAP.TechChallenge.LambdaCliente.Application.Models.Input;
using FIAP.TechChallenge.LambdaCliente.Application.UseCases.Interfaces;
using FIAP.TechChallenge.LambdaCliente.Domain.Repositories;
using FIAP.TechChallenge.LambdaCliente.Domain.Entities;

namespace FIAP.TechChallenge.LambdaCliente.Application.UseCases;

public class CriarClienteUseCase : ICriarClienteUseCase
{
    private readonly IClienteRepository _clienteRepository;

    public CriarClienteUseCase(
        IClienteRepository clienteRepository) => _clienteRepository = clienteRepository;

    public async Task<bool> Execute(CriarClienteInput request)
    {
        var cliente = new Cliente()
        {
            CPF = request.CPF,
            Nome = request.Nome,
            Email = request.Email
        };
        await _clienteRepository.Post(cliente);

        return true;
    }
}
