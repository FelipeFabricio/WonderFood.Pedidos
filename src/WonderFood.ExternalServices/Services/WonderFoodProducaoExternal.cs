using WonderFood.Application.Common.Interfaces;
using WonderFood.Models.Events;

namespace WonderFood.ExternalServices.Services;

public class WonderFoodProducaoExternal : IWonderFoodProducaoExternal
{
    public void SolicitarProducao(PagamentoProcessadoEvent producao)
    {
        throw new NotImplementedException();
    }
}