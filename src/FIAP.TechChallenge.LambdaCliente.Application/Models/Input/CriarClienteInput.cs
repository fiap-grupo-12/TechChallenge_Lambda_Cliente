using System.Diagnostics.CodeAnalysis;

namespace FIAP.TechChallenge.LambdaCliente.Application.Models.Input;

[ExcludeFromCodeCoverage]
public class CriarClienteInput
{
    public string Nome { get; set; }
    public string CPF { get; set; }
    public string Email { get; set; }
}
