using WonderFood.Domain.Entities;
using WonderFood.Domain.Entities.Enums;

namespace WonderFood.Application.Common.Interfaces;

public interface IPedidoRepository
{
    Task<Pedido?> ObterPorNumeroPedido(int numeroPedido);
    Task<Pedido?> ObterPorId(Guid id);
    Task Inserir(Pedido pedido);
    Task AtualizarStatus(Pedido pedido);
}