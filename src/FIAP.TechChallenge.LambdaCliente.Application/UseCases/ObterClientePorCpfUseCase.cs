using AutoMapper;
using FIAP.TechChallenge.LambdaCliente.Application.Models.Response;
using FIAP.TechChallenge.LambdaCliente.Application.UseCases.Interfaces;
using FIAP.TechChallenge.LambdaCliente.Domain.Repositories;

namespace FIAP.TechChallenge.LambdaCliente.Application.UseCases;

public class ObterClientePorCpfUseCase(IClienteRepository clienteRepository,
    IMapper mapper) : IObterClientePorCpfUseCase
{
    private readonly IClienteRepository _clienteRepository = clienteRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ClienteResponse> Execute(string cpf)
    {
        var result = await _clienteRepository.GetByCpf(cpf);

        return _mapper.Map<ClienteResponse>(result);
    }
}
