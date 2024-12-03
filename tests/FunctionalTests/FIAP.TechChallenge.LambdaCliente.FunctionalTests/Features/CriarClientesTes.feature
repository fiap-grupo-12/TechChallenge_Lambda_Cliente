#language: pt-br

Funcionalidade: Criar cliente

@success
Cenário: 01 - Criar novo cliente
	Dado que eu tenha um cliente com os dados de cpf "10226748979", nome "Joao" e email "teste" válidos
    Quando eu executar a api de criacao de clientes
    Então o cliente deve ser salvo e retornar cliente com o nome "Joao"