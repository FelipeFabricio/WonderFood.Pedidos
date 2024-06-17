namespace WonderFood.Domain.Entities.Enums;

public enum StatusPedido
{
    AguardandoPagamento,
    PagamentoAprovado,
    PagamentoRecusado,
    AguardandoPreparo,
    PreparoIniciado,
    ProntoParaRetirada,
    PedidoRetirado,
    AguardandoReembolsoPagamento,
    CanceladoComReembolso,
    CanceladoSemReembolso,
    Cancelado
}