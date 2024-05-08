using Microsoft.Extensions.Options;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Models.Events;

namespace WonderFood.ExternalServices.Services;

public class WonderFoodPagamentoExternal : ExternalServiceBase, IWonderFoodPagamentoExternal
{
    private readonly HttpClient _httpClient;
    private readonly ExternalServicesSettings _settings;

    public WonderFoodPagamentoExternal(HttpClient httpClient, IOptions<ExternalServicesSettings> settings)
    {
        _httpClient = httpClient;
        _settings = settings.Value;
    }
    
    public async Task EnviarSolicitacaoPagamento(PagamentoSolicitadoEvent pagamento)
    {
        var url = string.Concat(_settings.WonderfoodPagamentos.BaseUrl, _settings.WonderfoodPagamentos.PagamentoSolicitado);
        using var response = await _httpClient.PostAsync(url, CreateStringContent(pagamento));
    }
}