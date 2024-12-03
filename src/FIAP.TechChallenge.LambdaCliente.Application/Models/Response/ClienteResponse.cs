using System.Diagnostics.CodeAnalysis;

namespace FIAP.TechChallenge.LambdaCliente.Application.Models.Response;

public class ClienteResponse
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string CPF { get; set; }
    public string Email { get; set; }
}
