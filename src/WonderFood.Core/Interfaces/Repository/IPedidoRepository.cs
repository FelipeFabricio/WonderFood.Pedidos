using WonderFood.Core.Entities;
using WonderFood.Core.Entities.Enums;

namespace WonderFood.Core.Interfaces.Repository;

public interface IPedidoRepository
{
    Pedido ObterPorNumeroPedido(int numeroPedido);
    void Inserir(Pedido pedido);
}