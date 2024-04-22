using AutoMapper;
using MediatR;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Domain.Dtos.Produto;

namespace WonderFood.Application.Produtos.Queries.ObterTodosProdutos;

public class ObterTodosProdutosQueryHandler : IRequestHandler<ObterTodosProdutosQuery, IEnumerable<ProdutoOutputDto>>
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IMapper _mappper;

    public ObterTodosProdutosQueryHandler(IProdutoRepository produtoRepository, IMapper mappper)
    {
        _produtoRepository = produtoRepository;
        _mappper = mappper;
    }

    public async Task<IEnumerable<ProdutoOutputDto>> Handle(ObterTodosProdutosQuery request, CancellationToken cancellationToken)
    {
        var produtos = await _produtoRepository.ObterTodosProdutos();
        return _mappper.Map<IEnumerable<ProdutoOutputDto>>(produtos);
    }
}