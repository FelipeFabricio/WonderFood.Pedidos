using WonderFood.Core.Dtos.Pedido;
using WonderFood.Core.Entities.Enums;

namespace WonderFood.Core.Interfaces.UseCases;

public interface IPedidoUseCases
{
    IEnumerable<PedidosOutputDto> ObterPedidosEmAberto();
    void Inserir(InserirPedidoInputDto pedidoInputDto);
    StatusPedidoOutputDto ConsultarStatusPedido(int numeroPedido);
    void AtualizarStatusPedido(int numeroPedido, StatusPedido statusPedido);
    void Deletar(int numeroPedido);
}