using WonderFood.Core.Dtos;
using WonderFood.Core.Entities.Enums;

namespace WonderFood.Core.Interfaces;

public interface IPedidoUseCases
{
    IEnumerable<PedidosOutputDto> ObterPedidosEmAberto();
    void Inserir(InserirPedidoInputDto pedidoInputDto);
    StatusPedidoOutputDto ConsultarStatusPedido(int numeroPedido);
}