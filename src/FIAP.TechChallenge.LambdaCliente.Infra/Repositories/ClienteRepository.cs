using Amazon.DynamoDBv2.DataModel;
using FIAP.TechChallenge.LambdaCliente.Domain.Entities;
using FIAP.TechChallenge.LambdaCliente.Domain.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace FIAP.TechChallenge.LambdaCliente.Infra.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly IDynamoDBContext _context;

    public ClienteRepository(IDynamoDBContext context)
    {
        _context = context;
    }

    public async Task<IList<Cliente>> GetAll()
    {
        try
        {
            return await _context.ScanAsync<Cliente>(default).GetRemainingAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao consultar clientes. {ex}");
        }
    }

    public async Task<Cliente> GetByCpf(string cpf)
    {
        try
        {
            var condition = new List<ScanCondition>()
            {
                new("cpf", Amazon.DynamoDBv2.DocumentModel.ScanOperator.Equal, cpf)
            };

            var clientes = await _context.ScanAsync<Cliente>(default)
                .GetRemainingAsync();

            return clientes.FirstOrDefault(it => it.CPF.Equals(cpf, StringComparison.Ordinal));
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao consultar cliente. {ex}");
        }
    }

    public async Task<Cliente> Post(Cliente cliente)
    {
        try
        {
            cliente.Id = Guid.NewGuid();
            await _context.SaveAsync(cliente);
            return cliente;
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao cadastrar cliente. {ex}");
        }
    }
}
