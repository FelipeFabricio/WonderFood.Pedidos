DECLARE @RowCount INT;

SELECT @RowCount = COUNT(*) FROM Clientes;
IF @RowCount = 0
BEGIN
INSERT INTO Clientes (id, nome, cpf, email) VALUES
('250cc130-f258-4e32-abdd-5a457888c513', 'João da Silva', '93880743002', 'joao@gmail.com'),
('ef1db1e1-f913-44be-b850-ad0b08b990b0', 'Maria dos Santos', '76545678809', 'maria@hotmail.com');
END

SELECT @RowCount = COUNT(*) FROM Produtos;
IF @RowCount = 0
BEGIN
INSERT INTO Produtos (id, nome, descricao, categoria, valor) VALUES
('99d8b405-378f-449a-8636-c431fa38debb', 'X-Bacon', 'Pão, Hambúrguer Bovino e Bacon duplo', 0, 29.90),
('d12dc279-a731-4ae3-8713-5f655bfe966e', 'Suco de Laranja', 'Suco da fruta!!', 1, 10.90),
('9e40d3cc-7325-46c1-a5a2-8d770bdd22a0', 'Pudim', 'O lendário Pudim de leite', 2, 9.99),
('be484c18-8f83-4a76-a5c4-f1d2f98cce94', 'Batata Frita Média', 'Batata frita clássica', 3, 8.50);
END;

SELECT @RowCount = COUNT(*) FROM Pedidos;
IF @RowCount = 0
BEGIN
INSERT INTO Pedidos (id, clienteId, valorTotal, status, DataPedido, observacao) VALUES
('1a367798-fee1-49cd-a686-fc3e04577e2e', '250cc130-f258-4e32-abdd-5a457888c513', 59.29, 3, '06/26/2023', 'Pedido efetuado'),
('adf4e9b1-4b8b-434d-8790-112f1d9b2bcf', 'ef1db1e1-f913-44be-b850-ad0b08b990b0', 40.80, 0, '06/30/2023', 'Remover cebola'),
('17584894-b0ca-4338-b77e-994f104bebea', '250cc130-f258-4e32-abdd-5a457888c513', 59.29, 1, '01/12/2023', 'Pedido confirmmado'),
('d4c48fd0-56ce-4896-916e-d8749d2b2067', 'ef1db1e1-f913-44be-b850-ad0b08b990b0', 50.79, 2, '10/10/2023', 'Carne mal passada');
END;

SELECT @RowCount = COUNT(*) FROM ProdutosPedido;
IF @RowCount = 0
BEGIN
INSERT INTO ProdutosPedido (pedidoId, produtoId, quantidade) VALUES
('1a367798-fee1-49cd-a686-fc3e04577e2e', '99d8b405-378f-449a-8636-c431fa38debb', 1),
('1a367798-fee1-49cd-a686-fc3e04577e2e', 'd12dc279-a731-4ae3-8713-5f655bfe966e', 1),
('1a367798-fee1-49cd-a686-fc3e04577e2e', '9e40d3cc-7325-46c1-a5a2-8d770bdd22a0', 1),
('1a367798-fee1-49cd-a686-fc3e04577e2e', 'be484c18-8f83-4a76-a5c4-f1d2f98cce94', 1),
('adf4e9b1-4b8b-434d-8790-112f1d9b2bcf', '99d8b405-378f-449a-8636-c431fa38debb', 1),
('adf4e9b1-4b8b-434d-8790-112f1d9b2bcf', 'd12dc279-a731-4ae3-8713-5f655bfe966e', 1),
('17584894-b0ca-4338-b77e-994f104bebea', '99d8b405-378f-449a-8636-c431fa38debb', 1),
('17584894-b0ca-4338-b77e-994f104bebea', 'd12dc279-a731-4ae3-8713-5f655bfe966e', 1),
('17584894-b0ca-4338-b77e-994f104bebea', '9e40d3cc-7325-46c1-a5a2-8d770bdd22a0', 1),
('17584894-b0ca-4338-b77e-994f104bebea', 'be484c18-8f83-4a76-a5c4-f1d2f98cce94', 1),
('d4c48fd0-56ce-4896-916e-d8749d2b2067', '99d8b405-378f-449a-8636-c431fa38debb', 1),
('d4c48fd0-56ce-4896-916e-d8749d2b2067', 'd12dc279-a731-4ae3-8713-5f655bfe966e', 1),
('d4c48fd0-56ce-4896-916e-d8749d2b2067', '9e40d3cc-7325-46c1-a5a2-8d770bdd22a0', 1);
END;