using Amazon.DynamoDBv2.DataModel;

namespace FIAP.TechChallenge.LambdaCliente.Domain.Entities;

public class Cliente : EntityBase
{
    [DynamoDBProperty("nome")]
    public string Nome { get; set; }

    [DynamoDBProperty("cpf")]
    public string CPF { get; set; }

    [DynamoDBProperty("email")]
    public string Email { get; set; }
}
