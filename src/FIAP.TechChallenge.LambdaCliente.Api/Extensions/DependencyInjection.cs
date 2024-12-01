using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using AutoMapper;
using FIAP.TechChallenge.LambdaCliente.Infra.Repositories;
using FIAP.TechChallenge.LambdaCliente.Application.UseCases;
using FIAP.TechChallenge.LambdaCliente.Application.UseCases.Interfaces;
using FIAP.TechChallenge.LambdaCliente.Domain.Repositories;
using FIAP.TechChallenge.LambdaCliente.Infra.Configurations;
using System.Diagnostics.CodeAnalysis;

namespace FIAP.TechChallenge.LambdaCliente.Api.Extensions;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static void AddProjectDependencies(this IServiceCollection services)
    {
        services.AddAWSService<IAmazonDynamoDB>();
        services.AddTransient<IDynamoDBContext, DynamoDBContext>();

        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MapperConfig());
        });

        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);

        services.AddTransient<IClienteRepository, ClienteRepository>();
        
        services.AddTransient<ICriarClienteUseCase, CriarClienteUseCase>();
        services.AddTransient<IObterClientesUseCase, ObterClientesUseCase>();
        services.AddTransient<IObterClientePorCpfUseCase, ObterClientePorCpfUseCase>();
    }
}
