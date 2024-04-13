using WonderFood.Core.Dtos.Pedido;
using WonderFood.Core.Entities.Enums;

namespace WonderFood.Core.Interfaces.UseCases;

public interface IPedidoUseCases
{
    void Inserir(InserirPedidoInputDto pedidoInputDto);
    StatusPedidoOutputDto ConsultarStatusPedido(int numeroPedido);
}