using MassTransit;

namespace WonderFood.Application.Sagas;

public class CriarPedidoSagaState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; }
    public Guid ClienteId { get; set; }
    public Guid PedidoId { get; set; }
    public int NumeroPedido { get; set; }
    public string MotivoCancelamento { get; set; }
}