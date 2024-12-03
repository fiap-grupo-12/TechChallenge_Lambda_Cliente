using Amazon.DynamoDBv2.DataModel;
using System.Diagnostics.CodeAnalysis;

namespace FIAP.TechChallenge.LambdaCliente.Domain.Entities;

[DynamoDBTable("ClienteTable")]
public class Cliente
{
    [DynamoDBHashKey("id")]
    public Guid Id { get; set; }

    [DynamoDBProperty("nome")]
    public string Nome { get; set; }

    [DynamoDBProperty("cpf")]
    public string CPF { get; set; }

    [DynamoDBProperty("email")]
    public string Email { get; set; }
}
