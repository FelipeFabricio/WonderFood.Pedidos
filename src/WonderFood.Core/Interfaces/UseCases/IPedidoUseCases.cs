using WonderFood.Core.Dtos;

namespace WonderFood.Core.Interfaces;

public interface IPedidoUseCases
{
    IEnumerable<PedidosOutputDto> ObterPedidosEmAberto();
    int Inserir(InserirPedidoInputDto pedido);
    StatusPedidoOutputDto ConsultarStatusPedido(int numeroPedido);
}