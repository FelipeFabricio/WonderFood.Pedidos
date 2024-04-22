using MediatR;
using WonderFood.Domain.Dtos.Produto;

namespace WonderFood.Application.Produtos.Commands.InserirProduto;

public record InserirProdutoCommand(InserirProdutoInputDto Produto) : IRequest<Unit>;