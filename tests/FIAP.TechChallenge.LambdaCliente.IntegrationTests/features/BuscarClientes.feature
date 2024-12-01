#language: pt-br

Funcionalidade: Buscar cliente

@success
Cenário: 01 - Buscar cliente por cpf
	Dado que um cliente com CPF "12345678900" está cadastrado no DynamoDB
	Quando eu faço uma requisição GET para "/cliente/12345678900"
	Entao o status da resposta deve ser 200
	#E o corpo da resposta deve conter "nome" e "João Silva"