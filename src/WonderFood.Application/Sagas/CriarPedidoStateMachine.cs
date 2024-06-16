using MassTransit;
using MediatR;
using WonderFood.Application.Pedidos.Commands.AlterarStatus;
using WonderFood.Application.Pedidos.Commands.EnviarParaProducao;
using WonderFood.Application.Pedidos.Commands.EnviarSolicitacaoReembolso;
using WonderFood.Application.Pedidos.Commands.ProcessarPagamento;
using WonderFood.Application.Sagas.Messages;
using WonderFood.Domain.Entities.Enums;
using WonderFood.Models.Enums;
using WonderFood.Models.Events;

namespace WonderFood.Application.Sagas;

public class CriarPedidoStateMachine : MassTransitStateMachine<CriarPedidoSagaState>
{
    private readonly ISender _mediator;

    #region States

    public State AguardandoConfirmacaoPagamento { get; set; }
    public State AguardandoInicioPreparoPedido { get; set; }
    public State AguardandoTerminoPreparoPedido { get; set; }
    public State AguardandoRetiradaPedido { get; set; }
    public State AguardandoReembolsoPagamento { get; set; }
    public State PedidoConcluido { get; set; }
    public State PedidoCancelado { get; set; }
    public State PedidoCanceladoComReembolso { get; set; }
    public State PedidoCanceladoSemReembolso { get; set; }

    #endregion

    #region Events

    public Event<Events.PedidoIniciadoEvent> PedidoIniciado { get; set; }
    public Event<Events.PagamentoConfirmadoEvent> PagamentoConfirmado { get; set; }
    public Event<Events.PagamentoRecusadoEvent> PagamentoRecusado { get; set; }
    public Event<Events.ProducaoPedidoIniciadaEvent> ProducaoPedidoIniciada { get; set; }
    public Event<Events.ProducaoPedidoConcluidaEvent> ProducaoPedidoConcluida { get; set; }
    public Event<Events.PedidoRetiradoEvent> PedidoRetirado { get; set; }
    public Event<Events.PedidoCanceladoEvent> PedidoCanceladoEvent { get; set; }
    public Event<Events.ReembolsoEfetuadoEvent> ReembolsoAceito { get; set; }
    public Event<Events.ReembolsoRecusadoEvent> ReembolsoRecusado { get; set; }

    #endregion

    public CriarPedidoStateMachine(ISender mediator)
    {
        _mediator = mediator;

        InstanceState(x => x.CurrentState);

        Event(() => PedidoIniciado, e => e.CorrelateById(m => m.Message.PedidoId));
        Event(() => PagamentoConfirmado, e => e.CorrelateById(m => m.Message.PedidoId));
        Event(() => ProducaoPedidoIniciada, e => e.CorrelateById(m => m.Message.PedidoId));
        Event(() => ProducaoPedidoConcluida, e => e.CorrelateById(m => m.Message.PedidoId));
        Event(() => PedidoRetirado, e => e.CorrelateById(m => m.Message.PedidoId));
        Event(() => PagamentoRecusado, e => e.CorrelateById(m => m.Message.PedidoId));
        Event(() => PedidoCanceladoEvent, e => e.CorrelateById(m => m.Message.PedidoId));
        Event(() => ReembolsoAceito, e => e.CorrelateById(m => m.Message.PedidoId));
        Event(() => ReembolsoRecusado, e => e.CorrelateById(m => m.Message.PedidoId));

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
                .Then(context =>
                {
                    var command = new ProcessarPagamentoPedidoCommand(context.Message.PedidoId,
                        StatusPagamento.PagamentoAprovado);
                    _mediator.Send(command);
                })
                .TransitionTo(AguardandoInicioPreparoPedido),
            When(PagamentoRecusado)
                .Then(context =>
                {
                    context.Saga.MotivoCancelamento = "Pagamento recusado às " + DateTime.Now;
                    var command = new ProcessarPagamentoPedidoCommand(context.Message.PedidoId,
                        StatusPagamento.PagamentoRecusado);
                    _mediator.Send(command);
                })
                .TransitionTo(PedidoCancelado));

        During(AguardandoInicioPreparoPedido,
            When(ProducaoPedidoIniciada)
                .Then(context =>
                {
                    var command =
                        new AlterarStatusPedidoCommand(context.Message.PedidoId, StatusPedido.PreparoIniciado);
                    _mediator.Send(command);
                })
                .TransitionTo(AguardandoTerminoPreparoPedido),
            When(PedidoCanceladoEvent)
                .Then(context =>
                {
                    context.Saga.MotivoCancelamento = context.Message.MotivoCancelamento;

                    var command = new EnviarSolicitacaoReembolsoCommand(context.Message.PedidoId);
                    _mediator.Send(command);
                })
                .TransitionTo(AguardandoReembolsoPagamento));

        During(AguardandoTerminoPreparoPedido,
            When(ProducaoPedidoConcluida)
                .Then(context =>
                {
                    var command =
                        new AlterarStatusPedidoCommand(context.Message.PedidoId, StatusPedido.ProntoParaRetirada);
                    _mediator.Send(command);
                })
                .TransitionTo(AguardandoRetiradaPedido),
            When(PedidoCanceladoEvent)
                .Then(context =>
                {
                    context.Saga.MotivoCancelamento = context.Message.MotivoCancelamento;
                    var command = new EnviarSolicitacaoReembolsoCommand(context.Message.PedidoId);
                    _mediator.Send(command);
                })
                .TransitionTo(AguardandoReembolsoPagamento));

        During(AguardandoRetiradaPedido,
            When(PedidoRetirado)
                .Then(context =>
                {
                    var command = new AlterarStatusPedidoCommand(context.Message.PedidoId, StatusPedido.PedidoRetirado);
                    _mediator.Send(command);
                })
                .TransitionTo(PedidoConcluido));
        
        During(AguardandoReembolsoPagamento,
            When(ReembolsoAceito)
                .Then(context =>
                {
                    var command = new AlterarStatusPedidoCommand(context.Message.PedidoId, StatusPedido.CanceladoComReembolso);
                    _mediator.Send(command);
                })
                .TransitionTo(PedidoCanceladoComReembolso),
            When(ReembolsoRecusado)
                .Then(context =>
                {
                    var command = new AlterarStatusPedidoCommand(context.Message.PedidoId, StatusPedido.CanceladoSemReembolso);
                    _mediator.Send(command);
                })
                .TransitionTo(PedidoCanceladoSemReembolso));
    }
}