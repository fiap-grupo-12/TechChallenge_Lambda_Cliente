using FIAP.TechChallenge.LambdaCliente.Application.Models.Response;

namespace FIAP.TechChallenge.LambdaCliente.Application.UseCases.Interfaces;

public interface IObterClientePorCpfUseCase : IUseCaseAsync<string, ClienteResponse>
{
}
