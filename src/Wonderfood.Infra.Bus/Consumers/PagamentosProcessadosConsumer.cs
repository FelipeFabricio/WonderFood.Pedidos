using MassTransit;
using WonderFood.Core.Interfaces.UseCases;
using Wonderfood.Models.Events;

namespace Wonderfood.Infra.Bus.Consumers;

public class PagamentosProcessadosConsumer : IConsumer<PagamentoProcessadoEvent>
{
    private readonly IPedidoUseCases _pedidoUseCases;

    public PagamentosProcessadosConsumer(IPedidoUseCases pedidoUseCases)
    {
        _pedidoUseCases = pedidoUseCases;
    }

    public async Task Consume(ConsumeContext<PagamentoProcessadoEvent> context)
    {
        await _pedidoUseCases.EnviarPedidoParaProducao(context.Message);
    }
}