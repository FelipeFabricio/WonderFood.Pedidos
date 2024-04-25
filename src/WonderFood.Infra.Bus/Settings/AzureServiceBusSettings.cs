namespace WonderFood.Infra.Bus.Settings;

public class AzureServiceBusSettings
{
    public string ConnectionString { get; set; }
    public Queues Queues { get; set; }
}

public class Queues
{
    public string PagamentosSolicitados { get; set; }
    public string PagamentosProcessados { get; set; }
    public string IniciarProducaoPedido { get; set; }
    public string ProducaoPedidoConcluida { get; set; }
}
