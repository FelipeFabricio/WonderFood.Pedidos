using Microsoft.Extensions.Options;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Models.Events;

namespace WonderFood.ExternalServices.Services;

public class WonderFoodProducaoExternal : ExternalServiceBase, IWonderFoodProducaoExternal
{
    private readonly HttpClient _httpClient;
    private readonly ExternalServicesSettings _settings;

    public WonderFoodProducaoExternal(HttpClient httpClient, IOptions<ExternalServicesSettings> settings)
    {
        _httpClient = httpClient;
        _settings = settings.Value;
    }

    public async Task EnviarParaProducao(IniciarProducaoCommand producaoPedido)
    {
        var url = string.Concat(_settings.WonderfoodProducao.BaseUrl, _settings.WonderfoodProducao.IniciarProducao);
        using var response = await _httpClient.PostAsync(url, CreateStringContent(producaoPedido));
    }
}