using WonderFood.Domain.Entities.Enums;

namespace WonderFood.Domain.Entities;

public class Pedido
{
    public Guid Id { get; }
    public Guid ClienteId { get; }
    public int NumeroPedido { get; set; }
    public decimal ValorTotal { get; set; }
    public string? Observacao { get; init; }
    public DateTime DataPedido { get; init; }
    public StatusPedido Status { get; private set; }
    public IEnumerable<ProdutosPedido> Produtos { get; private set; }
    public Cliente Cliente { get; set; }
    public FormaPagamento FormaPagamento { get; init; }

    public Pedido(Guid clienteId, IEnumerable<ProdutosPedido> produtos, FormaPagamento formaPagamento,
        string? observacao = null, Guid? id = null)
    {
        ClienteId = clienteId;
        FormaPagamento = formaPagamento;
        Observacao = observacao;
        Id = id ?? Guid.NewGuid();
        Status = StatusPedido.AguardandoPagamento;
        DataPedido = DataPedido = DateTime.Now;
        PreencherListaProdutos(produtos);
        CalcularValorTotal();
    }

    private void PreencherListaProdutos(IEnumerable<ProdutosPedido> produtos)
    {
        var produtosPedidos = produtos.ToList();
        if (!produtosPedidos.Any())
            throw new Exception("A lista de produtos não pode ser vazia.");

        if (produtosPedidos.Any(p => p.Quantidade <= 0))
            throw new Exception("A quantidade de produtos não pode ser menor ou igual a zero.");
        
        foreach (var produto in produtosPedidos)
            produto.PedidoId = Id;
        
        Produtos = produtosPedidos;
    }

    private void CalcularValorTotal()
    {
        foreach (var produtosPedido in Produtos)
        {
            ValorTotal += produtosPedido.Produto.Valor * produtosPedido.Quantidade;
        }
    }

    public void AlterarStatusPedido(StatusPedido status)
    {
        if (Status == status)
            throw new Exception("Status atual é o mesmo que o status informado.");

        switch (Status)
        {
            case StatusPedido.AguardandoPagamento
                when (status != StatusPedido.PagamentoAprovado && status != StatusPedido.PagamentoRecusado &&
                      status != StatusPedido.Cancelado):
                throw new Exception(
                    "Não é possível alterar o status de 'AguardandoPagamento' para outro estado que não seja 'PagamentoRecusado', 'PagamentoAprovado' ou 'Cancelado'.");

            case StatusPedido.PagamentoAprovado
                when status != StatusPedido.AguardandoPreparo && status != StatusPedido.Cancelado:
                throw new Exception(
                    "Não é possível alterar o status de 'PagamentoAprovado' para outro estado que não seja 'AguardandoPreparo' ou 'Cancelado'.");

            case StatusPedido.PagamentoRecusado
                when status != StatusPedido.Cancelado:
                throw new Exception(
                    "Não é possível alterar o status de 'PagamentoRecusado' para outro estado que não seja 'Cancelado'.");

            case StatusPedido.AguardandoPreparo
                when status != StatusPedido.PreparoIniciado && status != StatusPedido.Cancelado:
                throw new Exception(
                    "Não é possível alterar o status de 'AguardandoPreparo' para outro estado que não seja 'PreparoIniciado' ou 'Cancelado'.");

            case StatusPedido.PreparoIniciado
                when status != StatusPedido.ProntoParaRetirada && status != StatusPedido.Cancelado:
                throw new Exception(
                    "Não é possível alterar o status de 'PreparoIniciado' para outro estado que não seja 'ProntoParaRetirada' ou 'Cancelado'.");

            case StatusPedido.ProntoParaRetirada
                when status != StatusPedido.PedidoRetirado && status != StatusPedido.Cancelado:
                throw new Exception(
                    "Não é possível alterar o  de 'ProntoParaRetirada' para outro estado que não seja 'PedidoRetirado' ou 'Cancelado'.");

            case StatusPedido.PedidoRetirado:
                throw new Exception("Não é possível alterar o status de 'PedidoRetirado' para outro estado.");

            case StatusPedido.Cancelado:
                throw new Exception("Não é possível alterar o status de 'Cancelado' para outro estado.");

            default:
                Status = status;
                break;
        }
    }

    //Para funcionando do EFcore
    private Pedido()
    {
    }
}