using Wonderfood.Models.Enums;

namespace Wonderfood.Models.Events;

public class PagamentoSolicitadoEvent
{
    public Guid IdPedido { get; set; }
    public decimal ValorTotal { get; set; }
    public FormaPagamento FormaPagamento { get; set; }
    public Guid IdCliente { get; set; }
    public DateTime DataConfirmacaoPedido { get; set; }
}