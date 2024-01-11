using WonderFood.Core.Entities;
using WonderFood.Core.Entities.Enums;

namespace WonderFood.Core.Interfaces.Repository;

public interface IPedidoRepository
{
    Pedido ObterPorNumeroPedido(int numeroPedido);
    IEnumerable<Pedido> ObterPedidosEmAberto();
    void Inserir(Pedido pedido);
    void AtualizarStatusPedido(int numeroPedido, StatusPedido novoSStatus);
    void Delete(int numeroPedido);
}