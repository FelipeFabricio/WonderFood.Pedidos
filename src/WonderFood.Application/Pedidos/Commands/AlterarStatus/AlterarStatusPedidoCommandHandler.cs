using MediatR;

namespace WonderFood.Application.Pedidos.Commands.AlterarStatus;

public class AlterarStatusPedidoCommandHandler :  IRequestHandler<AlterarStatusPedidoCommand, Unit>
{
    public Task<Unit> Handle(AlterarStatusPedidoCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}