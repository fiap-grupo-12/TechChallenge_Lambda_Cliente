using AutoMapper;
using FIAP.TechChallenge.LambdaCliente.Application.Models.Response;
using FIAP.TechChallenge.LambdaCliente.Application.UseCases.Interfaces;
using FIAP.TechChallenge.LambdaCliente.Domain.Repositories;

namespace FIAP.TechChallenge.LambdaCliente.Application.UseCases
{
    public class ObterClientesUseCase(
        IClienteRepository clienteRepository, IMapper mapper) : IObterClientesUseCase
    {
        private readonly IClienteRepository _clienteRepository = clienteRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IList<ClienteResponse>> Execute()
        {
            var result = await _clienteRepository.GetAll();

            return _mapper.Map<IList<ClienteResponse>>(result);
        }
    }
}
