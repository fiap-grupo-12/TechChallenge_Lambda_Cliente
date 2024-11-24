using Amazon.DynamoDBv2.DataModel;

namespace FIAP.TechChallenge.LambdaCliente.Domain.Entities;

public abstract class EntityBase
{
    [DynamoDBProperty("id")]
    public int Id { get; set; }
}
