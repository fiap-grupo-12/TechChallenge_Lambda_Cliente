using FIAP.TechChallenge.LambdaCliente.Application.Models.Input;

namespace FIAP.TechChallenge.LambdaCliente.Application.UseCases.Interfaces;

public interface ICriarClienteUseCase : IUseCaseAsync<CriarClienteInput, bool>
{
}
