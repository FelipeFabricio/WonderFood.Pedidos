using WonderFood.Core.Entities.Enums;

namespace WonderFood.Core.Entities
{
    public class Pedido
    {
        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        public StatusPedido Status { get; set; }
        public int NumeroPedido { get; set; }
        public string Observacao { get; set; }
        public DateTime DataPedido { get; private set; }
        public decimal ValorTotal { get; set; }
        public IEnumerable<ProdutosPedido> Produtos { get; set; }
        public Cliente Cliente { get; set; }
        public FormaPagamento FormaPagamento { get; set; }
        public void PreencherDataPedido() => DataPedido = DateTime.Now;
    }
}