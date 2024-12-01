using FIAP.TechChallenge.LambdaCliente.Application.Models.Input;
using FIAP.TechChallenge.LambdaCliente.Application.UseCases.Interfaces;
using FIAP.TechChallenge.LambdaCliente.Domain.Repositories;
using FIAP.TechChallenge.LambdaCliente.Domain.Entities;
using AutoMapper;
using FIAP.TechChallenge.LambdaCliente.Application.Models.Response;

namespace FIAP.TechChallenge.LambdaCliente.Application.UseCases;

public class CriarClienteUseCase(IClienteRepository clienteRepository,
    IMapper mapper) : ICriarClienteUseCase
{
    private readonly IClienteRepository _clienteRepository = clienteRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ClienteResponse> Execute(CriarClienteInput request)
    {
        var cliente = new Cliente()
        {
            CPF = request.CPF,
            Nome = request.Nome,
            Email = request.Email
        };

        var result = await _clienteRepository.Post(cliente);

        return _mapper.Map<ClienteResponse>(result);
    }
}
