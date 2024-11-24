using Amazon.Lambda.Core;
using Amazon.Lambda.Annotations;
using Amazon.Lambda.Annotations.APIGateway;
using Microsoft.AspNetCore.Mvc;
using FromBodyAttribute = Amazon.Lambda.Annotations.APIGateway.FromBodyAttribute;
using FIAP.TechChallenge.LambdaCliente.Api.Request;
using FIAP.TechChallenge.LambdaCliente.Application.UseCases.Interfaces;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace FIAP.TechChallenge.LambdaCliente.Api;

[ApiController]
[Route("api/Cliente")]
public class Functions(ICriarClienteUseCase criarClienteUseCase,
    IObterClientesUseCase obterClienteUseCase,
    IObterClientePorCpfUseCase obterClientePorCpfUseCase) : Controller
{
    private readonly ICriarClienteUseCase _criarClienteUseCase = criarClienteUseCase;
    private readonly IObterClientesUseCase _obterClientesUseCase = obterClienteUseCase;
    private readonly IObterClientePorCpfUseCase _obterClientePorCpfUseCase = obterClientePorCpfUseCase;


    [HttpGet]
    [LambdaFunction(ResourceName = "ObterClientePorCPF")]
    [HttpApi(LambdaHttpMethod.Get, "/cliente/{cpf}")]
    public async Task<IActionResult> ObterClientePorCpf(string cpf)
    {
        try
        {
            var result = await _obterClientePorCpfUseCase.Execute(cpf);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet]
    [LambdaFunction(ResourceName = "ObterTodosOsClientes")]
    [HttpApi(LambdaHttpMethod.Get, "/cliente")]
    public async Task<IActionResult> ObterTodosOsClientes()
    {
        try
        {
            var result = await _obterClientesUseCase.Execute();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpPost]
    [LambdaFunction(ResourceName = "CriarClientes")]
    [HttpApi(LambdaHttpMethod.Post, "/cliente")]
    public async Task<IActionResult> CriarClientes([FromBody] CriarClienteRequest request)
    {
        try
        {
            var result = await _criarClienteUseCase.Execute(request.ToInput());
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
}