﻿namespace WonderFood.Domain.Dtos.Produto;

public class InserirProdutosPedidoInputDto
{
    public Guid ProdutoId { get; set; }
    public int Quantidade { get; set; }
}