using MediatR;
using WonderFood.Domain.Dtos.Produto;

namespace WonderFood.Application.Produtos.Queries.ObterTodosProdutos;

public record ObterTodosProdutosQuery() : IRequest<IEnumerable<ProdutoOutputDto>>;