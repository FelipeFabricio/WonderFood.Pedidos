using WonderFood.Models.Events;

namespace WonderFood.Application.Common.Interfaces;

public interface IWonderFoodPagamentoExternal
{
    Task EnviarSolicitacaoPagamento(PagamentoSolicitadoEvent pagamento);
}
