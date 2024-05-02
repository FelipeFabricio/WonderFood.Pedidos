using WonderFood.Models.Events;

namespace WonderFood.Application.Common.Interfaces;

public interface IWonderFoodProducaoExternal
{
    void SolicitarProducao(PagamentoProcessadoEvent producao);
}