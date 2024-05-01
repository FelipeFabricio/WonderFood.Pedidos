using WonderFood.Domain.Dtos.Produto;
using WonderFood.Domain.Entities.Enums;

namespace WonderFood.Domain.Dtos.Pedido;

public class InserirPedidoOutputDto
{
    public int NumeroPedido { get; set; }
    public decimal ValorTotal { get; set; }
    public string Status { get; set; }
    public FormaPagamento FormaPagamento { get; set; }
    public string Observacao { get; set; }
    public IEnumerable<ProdutosPedidoOutputDto> Produtos { get; set; }
    
}