using MediatR;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Domain.Entities;

namespace WonderFood.Application.Produtos.Commands.InserirProduto;

public class InserirProdutoCommandHandler : IRequestHandler<InserirProdutoCommand, Unit>
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public InserirProdutoCommandHandler(IProdutoRepository produtoRepository,IUnitOfWork unitOfWork)
    {
        _produtoRepository = produtoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(InserirProdutoCommand request, CancellationToken cancellationToken)
    {
        var produto = new Produto(request.Produto.Nome, request.Produto.Valor, 
            request.Produto.Categoria, request.Produto.Descricao);
        
        await _produtoRepository.Inserir(produto);
        await _unitOfWork.CommitChangesAsync();
        
        return Unit.Value;
    }
}