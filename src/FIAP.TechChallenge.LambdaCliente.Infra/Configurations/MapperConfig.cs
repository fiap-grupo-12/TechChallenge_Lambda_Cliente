using AutoMapper;
using FIAP.TechChallenge.LambdaCliente.Application.Models.Input;
using FIAP.TechChallenge.LambdaCliente.Application.Models.Response;
using FIAP.TechChallenge.LambdaCliente.Domain.Entities;

namespace FIAP.TechChallenge.LambdaCliente.Infra.Configurations;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<CriarClienteInput, Cliente>().ReverseMap();
        CreateMap<ClienteResponse, Cliente>().ReverseMap();
    }
}
