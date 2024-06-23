using WonderFood.Domain.Entities.Enums;

namespace WonderFood.Domain.Entities;

public class Produto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Valor { get; set; }
    public CategoriaProduto Categoria { get; set; }

    public Produto(string nome, decimal valor, CategoriaProduto categoria, string descricao, Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        Categoria = categoria;
        ValidarDescricao(descricao);
        ValidarNome(nome);
        ValidarValor(valor);
    }

    private void ValidarDescricao(string descricao)
    {
        if(!string.IsNullOrWhiteSpace(descricao) && 
           System.Text.RegularExpressions.Regex.IsMatch(descricao, @"[^a-zA-Z0-9\s\.\!\u00C0-\u00FF]"))
            throw new ArgumentException("Descrição do produto não pode conter caracteres especiais");

        Descricao = descricao;
    }

    private void ValidarValor(decimal valor)
    {
        if(valor <= 0)
            throw new ArgumentException("Valor do produto deve ser maior que zero");

        Valor = valor;
    }

    private void ValidarNome(string nome)
    {
        if(string.IsNullOrWhiteSpace(nome) || nome.Length < 3)
            throw new ArgumentException("Nome do produto precisa ter no mínimo 3 caracteres");
        
        if (System.Text.RegularExpressions.Regex.IsMatch(nome, @"[^a-zA-Z0-9\s\.\!\-\u00C0-\u00FF]"))
            throw new ArgumentException("Nome do produto não pode conter caracteres especiais");

        Nome = nome;
    }

    private Produto()
    {
        
    }
}