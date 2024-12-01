using FIAP.TechChallenge.LambdaCliente.Application.Models.Input;
using FIAP.TechChallenge.LambdaCliente.Application.Models.Response;

namespace FIAP.TechChallenge.LambdaCliente.Application.UseCases.Interfaces;

public interface ICriarClienteUseCase : IUseCaseAsync<CriarClienteInput, ClienteResponse>
{
}
