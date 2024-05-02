using WonderFood.Models.Events;

namespace WonderFood.Application.Common.Interfaces;

public interface IWonderFoodPagamentoExternal
{
    void SolicitarPagamento(PagamentoSolicitadoEvent pagamento);
}
