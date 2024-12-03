using Amazon.Lambda.Core;
using Amazon.Lambda.Annotations;
using Amazon.Lambda.Annotations.APIGateway;
using FromBodyAttribute = Amazon.Lambda.Annotations.APIGateway.FromBodyAttribute;
using FIAP.TechChallenge.LambdaCliente.Api.Request;
using FIAP.TechChallenge.LambdaCliente.Application.UseCases.Interfaces;
using Amazon.Lambda.APIGatewayEvents;
using FIAP.TechChallenge.LambdaCliente.Application.Models.Response;
using System.Diagnostics.CodeAnalysis;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace FIAP.TechChallenge.LambdaCliente.Api;

public class Function(ICriarClienteUseCase criarClienteUseCase,
    IObterClientesUseCase obterClienteUseCase,
    IObterClientePorCpfUseCase obterClientePorCpfUseCase)
{
    private readonly ICriarClienteUseCase _criarClienteUseCase = criarClienteUseCase;
    private readonly IObterClientesUseCase _obterClientesUseCase = obterClienteUseCase;
    private readonly IObterClientePorCpfUseCase _obterClientePorCpfUseCase = obterClientePorCpfUseCase;

    [LambdaFunction(ResourceName = "Handler")]
    public async Task<APIGatewayProxyResponse?> Handler(APIGatewayProxyRequest request, ILambdaContext context)
    {
        bool methodOk = false;
        List<object> parameters = [];

        LambdaHttpMethod httpMethod = Enum.Parse<LambdaHttpMethod>(request.HttpMethod, true);
        return await GetResponseAsync(request,
                                      methodOk,
                                      parameters,
                                      httpMethod);
    }

    [HttpApi(LambdaHttpMethod.Get, "/Cliente/{cpf}")]
    public async Task<ClienteResponse> ObterClientePorCpf(string cpf)
        => await _obterClientePorCpfUseCase.Execute(cpf);

    [HttpApi(LambdaHttpMethod.Get, "/Cliente")]
    public async Task<IList<ClienteResponse>> ObterTodosOsClientes()
        => await _obterClientesUseCase.Execute();

    [HttpApi(LambdaHttpMethod.Post, "/Cliente")]
    public async Task<ClienteResponse> CriarClientes([FromBody] CriarClienteRequest request)
        => await _criarClienteUseCase.Execute(request.ToInput());

    private async Task<APIGatewayProxyResponse?> GetResponseAsync(APIGatewayProxyRequest request,
        bool methodOk,
        List<object> parameters,
        LambdaHttpMethod httpMethod)
    {
        try
        {
            foreach (var method in this.GetType().GetMethods().Where(x => x.Name != "Handler"))
            {
                foreach (var attributes in method.CustomAttributes.Where(x => x.ConstructorArguments.Count > 1))
                {
                    int methodType = (int)attributes.ConstructorArguments.FirstOrDefault(x => x.ArgumentType.Name == "LambdaHttpMethod").Value;
                    var pathType = attributes.ConstructorArguments.FirstOrDefault(x => x.ArgumentType.Name == "String").Value.ToString();

                    methodOk = httpMethod == (LambdaHttpMethod)methodType && string.Equals(pathType, request.Resource, StringComparison.CurrentCultureIgnoreCase);
                }
                if (methodOk)
                {
                    foreach (var parameter in method.GetParameters())
                        if (parameter.CustomAttributes.Count() > 0)
                            parameters.Add(Newtonsoft.Json.JsonConvert.DeserializeObject(request.Body, Type.GetType(parameter.ParameterType.AssemblyQualifiedName)));
                        else
                            foreach (var stringParameters in request.PathParameters.Where(x => x.Key == parameter.Name))
                                parameters.Add(stringParameters.Value);

                    var resultAsync = method.Invoke(this, [.. parameters]);

                    if (resultAsync is Task task)
                    {
                        await task;
                        var resultProperty = task.GetType().GetProperty("Result");

                        return new APIGatewayProxyResponse
                        {
                            StatusCode = 200,
                            Body = Newtonsoft.Json.JsonConvert.SerializeObject(resultProperty?.GetValue(task)),
                            Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
                        };
                    }
                }
            }
        }
        catch (Exception ex)
        {
            return new APIGatewayProxyResponse
            {
                StatusCode = 400,
                Body = ex.Message,
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }

        return null;
    }
}