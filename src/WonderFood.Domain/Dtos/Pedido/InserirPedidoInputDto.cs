using WonderFood.Domain.Dtos.Produto;
using WonderFood.Domain.Entities.Enums;

namespace WonderFood.Domain.Dtos.Pedido;

public class InserirPedidoInputDto
{
    public Guid ClienteId { get; set; }
    public string Observacao { get; set; }
    public FormaPagamento FormaPagamento { get; set; }
    public IEnumerable<InserirProdutosPedidoInputDto> Produtos { get; set; }
}