#language: pt-br

Funcionalidade: Criar cliente

@success
Cenário: 01 - Criar novo cliente
	Dado que eu tenha um "10226748979", "Joao" e "teste" válidos
    Quando eu executar o CriarClienteUseCase
    Então o cliente deve ser salvo e retornar cliente com o nome "Joao"