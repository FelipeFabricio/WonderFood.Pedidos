using WonderFood.Core.Dtos.Produto;

namespace WonderFood.Core.Dtos.Pedido;

public class InserirPedidoInputDto
{
    public Guid ClienteId { get; set; }
    public string Observacao { get; set; }
    public IEnumerable<InserirProdutosPedidoInputDto> Produtos { get; set; }
}