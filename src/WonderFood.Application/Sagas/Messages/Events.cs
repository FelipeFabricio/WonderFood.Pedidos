using MassTransit.NewIdProviders;
using WonderFood.Models.Enums;

namespace WonderFood.Application.Sagas.Messages;

public class Events
{
    public class PedidoIniciadoEvent
    {
        public Guid PedidoId { get; set; }
        public Guid ClienteId { get; set; }
        public int NumeroPedido { get; set; }
        public decimal ValorTotal { get; set; }
        public FormaPagamento FormaPagamento { get; set; }
        public DateTime DataConfirmacaoPedido { get; set; }
    }
    
    public class PagamentoConfirmadoEvent
    {
        public Guid PedidoId { get; set; }
    }    
    
    public class PagamentoRecusadoEvent
    {
        public Guid PedidoId { get; set; }
    }    
    
    public class ProducaoPedidoIniciadaEvent
    {
        public Guid PedidoId { get; set; }
    }
    
    public class PedidoRetiradoEvent
    {
        public Guid PedidoId { get; set; }
    }
    
    public class ProducaoPedidoConcluidaEvent
    {
        public Guid PedidoId { get; set; }
    }
    
    public class PedidoCanceladoEvent
    {
        public Guid PedidoId { get; set; }
        public string MotivoCancelamento { get; set; }
    }
}