using WonderFood.Core.Dtos.Produto;
using WonderFood.Core.Entities.Enums;

namespace WonderFood.Core.Dtos.Pedido;

public class InserirPedidoInputDto
{
    public Guid ClienteId { get; set; }
    public string Observacao { get; set; }
    public FormaPagamento FormaPagamento { get; set; }
    public IEnumerable<InserirProdutosPedidoInputDto> Produtos { get; set; }
}