using WonderFood.Models.Events;

namespace WonderFood.Application.Common.Interfaces;

public interface IWonderFoodProducaoExternal
{
    Task EnviarParaProducao(IniciarProducaoCommand producaoPedido);
}