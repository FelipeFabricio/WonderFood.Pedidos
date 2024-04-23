using AutoMapper;
using MediatR;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Domain.Dtos.Produto;

namespace WonderFood.Application.Produtos.Queries.ObterTodosProdutos;

public class ObterTodosProdutosQueryHandler(IProdutoRepository produtoRepository, IMapper mappper)
    : IRequestHandler<ObterTodosProdutosQuery, IEnumerable<ProdutoOutputDto>>
{
    public async Task<IEnumerable<ProdutoOutputDto>> Handle(ObterTodosProdutosQuery request, CancellationToken cancellationToken)
    {
        var produtos = await produtoRepository.ObterTodosProdutos();
        return mappper.Map<IEnumerable<ProdutoOutputDto>>(produtos);
    }
}