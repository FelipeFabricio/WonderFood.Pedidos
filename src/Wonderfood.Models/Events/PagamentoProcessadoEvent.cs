using Wonderfood.Models.Enums;

namespace Wonderfood.Models.Events;

public class PagamentoProcessadoEvent
{
    public Guid IdPedido { get; set; }
    public SituacaoPagamento StatusPagamento { get; set; }
}