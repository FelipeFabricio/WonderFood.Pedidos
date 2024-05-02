namespace WonderFood.ExternalServices;

public class ExternalServicesSettings
{
    public WonderFoodPagamento WonderFoodPagamentos { get; set; }
    public WonderFoodProducao WonderfoodProducao { get; set; }
}

public class WonderFoodPagamento
{
    public string BaseUrl { get; set; }
    public string PagamentoSolicitado { get; set; }
}

public class WonderFoodProducao
{
    public string BaseUrl { get; set; }
    public string PagamentoProcessado { get; set; }
}
