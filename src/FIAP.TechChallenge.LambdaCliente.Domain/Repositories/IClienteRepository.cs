using FIAP.TechChallenge.LambdaCliente.Domain.Entities;

namespace FIAP.TechChallenge.LambdaCliente.Domain.Repositories;

public interface IClienteRepository
{
    Task<IList<Cliente>> GetAll();
    Task<Cliente> GetByCpf(string cpf);
    Task<Cliente> GetById(int id);
    Task<Cliente> Post(Cliente cliente);
}
