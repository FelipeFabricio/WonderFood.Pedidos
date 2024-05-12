using WonderFood.Domain.Entities.Enums;

namespace WonderFood.Models.Events;

public class IniciarProducaoCommand
{
    public Guid IdPedido { get; set; }
    public int NumeroPedido { get; set; }
    public string? Observacao { get; set; }
    public StatusPedido Status { get; set; }
    public IEnumerable<ProdutosPedido> Produtos { get; set; }
}

public class ProdutosPedido
{
    public Guid ProdutoId { get; set; }
    public int Quantidade { get; set; }
}