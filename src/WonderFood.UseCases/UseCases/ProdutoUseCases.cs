using AutoMapper;
using WonderFood.Core.Dtos.Produto;
using WonderFood.Core.Entities;
using WonderFood.Core.Entities.Enums;
using WonderFood.Core.Interfaces.Repository;
using WonderFood.Core.Interfaces.UseCases;

namespace WonderFood.UseCases.UseCases;

public class ProdutoUseCases : IProdutoUseCases
{
    private readonly IProdutoRepository _repository;
    private readonly IMapper _mappper;

    public ProdutoUseCases(IProdutoRepository repository, IMapper mappper)
    {
        _repository = repository;
        _mappper = mappper;
    }

    public IEnumerable<ProdutoOutputDto> ObterTodosProdutos()
    {
        var produtos = _repository.ObterTodosProdutos();
        return _mappper.Map<IEnumerable<ProdutoOutputDto>>(produtos);
    }

    public IEnumerable<ProdutoOutputDto> ObterProdutoPorCategoria(int categoria)
    {
        if (Enum.IsDefined(typeof(CategoriaProduto), categoria))
        {
            CategoriaProduto categoriaEnum = (CategoriaProduto)Enum.ToObject(typeof(CategoriaProduto), categoria);

            var produtos = _repository.ObterProdutosPorCategoria(categoriaEnum);
            return _mappper.Map<IEnumerable<ProdutoOutputDto>>(produtos);
        }
        throw new ArgumentException("Categoria do produto inválida!");
    }

    public void InserirProduto(InserirProdutoInputDto produto)
    {
        var produtoEntity = _mappper.Map<Produto>(produto);
        _repository.Inserir(produtoEntity);
    }
}