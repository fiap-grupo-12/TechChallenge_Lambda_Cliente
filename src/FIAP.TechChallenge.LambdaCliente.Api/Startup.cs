using FIAP.TechChallenge.LambdaCliente.Api.Extensions;

namespace FIAP.TechChallenge.LambdaCliente.Api;

[Amazon.Lambda.Annotations.LambdaStartup]
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
