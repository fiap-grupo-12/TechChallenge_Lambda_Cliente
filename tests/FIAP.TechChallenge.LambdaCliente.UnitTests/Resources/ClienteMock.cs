using FIAP.TechChallenge.LambdaCliente.Application.Models.Response;
using FIAP.TechChallenge.LambdaCliente.Domain.Entities;

namespace FIAP.TechChallenge.LambdaCliente.UnitTests.Resources
{
    public class ClienteMock
    {
        public static List<Cliente> GetClienteMock() =>
        [
            new Cliente
            {
                Id = new Guid(),
                Nome = "Client Teste",
                CPF = "10927749041",
                Email = "teste@teste.com"
            },
            new Cliente
            {
                Id = new Guid(),
                Nome = "Client Teste 2",
                CPF = "13272968024",
                Email = "teste@teste.com"
            }
        ];

        public static List<ClienteResponse> GetClienteResponseMock() =>
        [
            new ClienteResponse
            {
                Id = new Guid(),
                Nome = "Client Teste",
                CPF = "10927749041",
                Email = "teste@teste.com"
            },
            new ClienteResponse
            {
                Id = new Guid(),
                Nome = "Client Teste 2",
                CPF = "13272968024",
                Email = "teste@teste.com"
            }
        ];
    }
}
