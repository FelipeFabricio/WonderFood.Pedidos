namespace WonderFood.ExternalServices;

public class ExternalServicesSettings
{
    public WonderfoodPagamentos WonderfoodPagamentos { get; set; }
    public WonderfoodProducao WonderfoodProducao { get; set; }
}

public class WonderfoodPagamentos
{
    public string BaseUrl { get; set; }
    public string PagamentoSolicitado { get; set; }
}

public class WonderfoodProducao
{
    public string BaseUrl { get; set; }
    public string IniciarProducao { get; set; }
}
