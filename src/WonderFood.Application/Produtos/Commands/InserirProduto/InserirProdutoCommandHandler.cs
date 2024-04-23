using MediatR;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Domain.Entities;

namespace WonderFood.Application.Produtos.Commands.InserirProduto;

public class InserirProdutoCommandHandler(IProdutoRepository produtoRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<InserirProdutoCommand, Unit>
{
    public async Task<Unit> Handle(InserirProdutoCommand request, CancellationToken cancellationToken)
    {
        var produto = new Produto(request.Produto.Nome, request.Produto.Valor, 
            request.Produto.Categoria, request.Produto.Descricao);
        
        await produtoRepository.Inserir(produto);
        await unitOfWork.CommitChangesAsync();
        
        return Unit.Value;
    }
}