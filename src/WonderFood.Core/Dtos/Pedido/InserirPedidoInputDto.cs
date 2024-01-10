namespace WonderFood.Core.Dtos;

public class InserirPedidoInputDto
{
    public Guid ClienteId { get; set; }
    public string Observacao { get; set; }
    public IEnumerable<InserirProdutosPedidoInputDto> Produtos { get; set; }
}