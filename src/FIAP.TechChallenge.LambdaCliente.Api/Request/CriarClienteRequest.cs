using FIAP.TechChallenge.LambdaCliente.Application.Models.Input;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace FIAP.TechChallenge.LambdaCliente.Api.Request
{
    [ExcludeFromCodeCoverage]
    public class CriarClienteRequest
    {
        [Required(ErrorMessage = "É obrigatório informar o nome.")]
        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("cpf")]
        public string CPF { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        public CriarClienteInput ToInput()
        {
            return new CriarClienteInput
            {
                Nome = Nome,
                CPF = CPF,
                Email = Email
            };
        }
    }
}
