using AutoMapper;
using FIAP.TechChallenge.LambdaCliente.Application.Models.Input;
using FIAP.TechChallenge.LambdaCliente.Application.Models.Response;
using FIAP.TechChallenge.LambdaCliente.Application.UseCases;
using FIAP.TechChallenge.LambdaCliente.Domain.Entities;
using FIAP.TechChallenge.LambdaCliente.Domain.Repositories;
using Moq;
using TechTalk.SpecFlow;
using Xunit;

namespace FIAP.TechChallenge.LambdaCliente.FunctionalTests.Steps
{
    [Binding]
    public class CriarClienteSteps
    {
        private CriarClienteInput _input;
        private Cliente _clienteSalvo;
        private ClienteResponse _response;
        private readonly Mock<IClienteRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private CriarClienteUseCase _useCase;

        public CriarClienteSteps()
        {
            _mockRepository = new Mock<IClienteRepository>();
            _mockMapper = new Mock<IMapper>();
        }

        [Given(@"que eu tenha um ""(.*)"", ""(.*)"" e ""(.*)"" válidos")]
        public void DadoQueEuTenhaUmCPFNomeEEmailValidos(string cpf, string nome, string email)
        {
            _input = new CriarClienteInput
            {
                CPF = cpf,
                Nome = nome,
                Email = email
            };

            _clienteSalvo = new Cliente
            {
                Id = Guid.NewGuid(),
                CPF = cpf,
                Nome = nome,
                Email = email
            };

            _mockRepository.Setup(x => x.Post(It.IsAny<Cliente>())).ReturnsAsync(_clienteSalvo);
            _mockMapper.Setup(m => m.Map<ClienteResponse>(_clienteSalvo))
                       .Returns(new ClienteResponse { Nome = nome, CPF = cpf, Email = email });

            _useCase = new CriarClienteUseCase(_mockRepository.Object, _mockMapper.Object);
        }

        [When(@"eu executar o CriarClienteUseCase")]
        public async Task QuandoEuExecutarOCriarClienteUseCase()
        {
            _response = await _useCase.Execute(_input);
        }

        [Then(@"o cliente deve ser salvo e retornar cliente com o nome ""(.*)""")]
        public void EntaoOClienteDeveSerSalvoERetornarUmClienteResponse(string nome)
        {
            Assert.NotNull(_response);
            Assert.Equal(nome, _response.Nome);
        }
    }
}
