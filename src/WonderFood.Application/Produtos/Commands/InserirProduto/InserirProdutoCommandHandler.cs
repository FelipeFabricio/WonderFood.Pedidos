using AutoMapper;
using MediatR;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Domain.Entities;

namespace WonderFood.Application.Produtos.Commands.InserirProduto;

public class InserirProdutoCommandHandler : IRequestHandler<InserirProdutoCommand, Unit>
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IMapper _mappper;
    private readonly IUnitOfWork _unitOfWork;

    public InserirProdutoCommandHandler(IProdutoRepository produtoRepository, IMapper mappper, IUnitOfWork unitOfWork)
    {
        _produtoRepository = produtoRepository;
        _mappper = mappper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(InserirProdutoCommand request, CancellationToken cancellationToken)
    {
        var produtoEntity = _mappper.Map<Produto>(request.Produto);
        
        await _produtoRepository.Inserir(produtoEntity);
        await _unitOfWork.CommitChangesAsync();
        
        return Unit.Value;
    }
}