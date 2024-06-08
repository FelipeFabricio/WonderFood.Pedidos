using MassTransit;
using WonderFood.Application.Sagas.Messages;
using WonderFood.Models.Events;

namespace WonderFood.Application.Sagas;

public class CriarPedidoStateMachine : MassTransitStateMachine<CriarPedidoSagaState>
{
    #region States

    public State AguardandoConfirmacaoPagamento { get; set; }
    public State AguardandoInicioPreparoPedido { get; set; }
    public State AguardandoTerminoPreparoPedido { get; set; }
    public State AguardandoRetiradaPedido { get; set; }
    public State PedidoConcluido { get; set; }

    #endregion

    #region Events

    public Event<Events.PedidoIniciadoEvent> PedidoIniciado { get; set; }
    public Event<Events.PagamentoConfirmadoEvent> PagamentoConfirmado { get; set; }
    public Event<Events.ProducaoPedidoIniciadaEvent> ProducaoPedidoIniciada { get; set; }
    public Event<Events.ProducaoPedidoConcluidaEvent> ProducaoPedidoConcluida { get; set; }
    public Event<Events.PedidoRetiradoEvent> PedidoRetirado { get; set; }

    #endregion

    public CriarPedidoStateMachine()
    {
        InstanceState(x => x.CurrentState);

        Event(() => PedidoIniciado, e => e.CorrelateById(m => m.Message.PedidoId));
        Event(() => PagamentoConfirmado, e => e.CorrelateById(m => m.Message.PedidoId));
        Event(() => ProducaoPedidoIniciada, e => e.CorrelateById(m => m.Message.PedidoId));
        Event(() => ProducaoPedidoConcluida, e => e.CorrelateById(m => m.Message.PedidoId));
        Event(() => PedidoRetirado, e => e.CorrelateById(m => m.Message.PedidoId));

        Initially(
                When(PedidoIniciado)
                    .Then(context =>
                    {
                        context.Saga.PedidoId = context.Message.PedidoId;
                        context.Saga.ClienteId = context.Message.ClienteId;
                        context.Saga.NumeroPedido = context.Message.NumeroPedido;
                    })
                    .Publish(context => new PagamentoSolicitadoEvent
                    {
                        IdPedido = context.Message.PedidoId,
                        ValorTotal = context.Message.ValorTotal,
                        FormaPagamento = context.Message.FormaPagamento,
                        IdCliente = context.Message.ClienteId,
                        DataConfirmacaoPedido = DateTime.Now
                    })
            .TransitionTo(AguardandoConfirmacaoPagamento));

        During(AguardandoConfirmacaoPagamento,
            When(PagamentoConfirmado)
                //envia para produção
                .TransitionTo(AguardandoInicioPreparoPedido));

        During(AguardandoInicioPreparoPedido,
            When(ProducaoPedidoIniciada)
                //faz nada
                .TransitionTo(AguardandoTerminoPreparoPedido));

        During(AguardandoTerminoPreparoPedido,
            When(ProducaoPedidoConcluida)
                //faz nada
                .TransitionTo(AguardandoRetiradaPedido));

        During(AguardandoRetiradaPedido,
            When(PedidoRetirado)
                //faz nada
                .TransitionTo(PedidoConcluido)
                .Finalize());
    }
}