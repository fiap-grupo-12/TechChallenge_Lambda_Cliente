using FIAP.TechChallenge.LambdaCliente.Api.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace FIAP.TechChallenge.LambdaCliente.Api;

[Amazon.Lambda.Annotations.LambdaStartup]
[ExcludeFromCodeCoverage]
public class Startup
{
    public Startup()
    { }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddProjectDependencies();

        services.AddCors();
        services.AddControllers();
    }
}
