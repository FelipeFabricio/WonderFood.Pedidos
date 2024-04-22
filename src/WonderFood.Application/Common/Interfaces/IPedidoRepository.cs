using WonderFood.Domain.Entities;
using WonderFood.Domain.Entities.Enums;

namespace WonderFood.Application.Common.Interfaces;

public interface IPedidoRepository
{
    Task<Pedido?> ObterPorNumeroPedido(int numeroPedido);
    Task Inserir(Pedido pedido);
    Task AtualizarStatusPedido(Guid idPedido, StatusPedido statusPedido);
}