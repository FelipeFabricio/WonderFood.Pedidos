Funcionalidade: Pedido - Efetuar Pedido no sistema
    Como um cliente
    Eu desejo efetuar um pedido no sistema
    Para que eu possa receber o meu pedido
    
Cenário: Pedido registrado com sucesso
    Dado que o cliente possui um cadastro válido no restaurante
    E o cliente está logado
    E o Pedido possui pelo menos um produto
    Quando o cliente efetuar o pedido
    Então o cliente deverá ter seu pedido registrado no sistema com sucesso
    E o cliente deverá ser informado que o pedido foi registrado com sucesso
    E o cliente deverá ser informado que o pedido está aguardando pagamento
    
Cenário: Pedido sem produtos
    Dado que o cliente possui um cadastro válido no restaurante
    E o cliente está logado
    E o Pedido não possui produtos
    Quando o cliente efetuar o pedido
    Então o cliente deverá receber uma mensagem informando que o pedido não possui produtos
    E o cliente não deverá ter seu pedido registrado no sistema
