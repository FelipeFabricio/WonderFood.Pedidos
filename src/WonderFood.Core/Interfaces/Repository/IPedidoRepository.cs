using WonderFood.Core.Entities;

namespace WonderFood.Core.Interfaces;

public interface IPedidoRepository
{
    Pedido ObterPorNumeroPedido(int numeroPedido);
    IEnumerable<Pedido> ObterPedidosEmAberto();
    bool Inserir(Pedido pedido);
    bool AtualizarStatusPedido(Pedido pedido);
    bool Delete(int numeroPedido);
}