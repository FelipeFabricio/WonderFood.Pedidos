using NSubstitute;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Application.Produtos.Commands.InserirProduto;
using WonderFood.Application.UnitTests.Produto.Factory;

namespace WonderFood.Application.UnitTests.Produto;

public class InserirProdutoCommandHandlerTests
{
    private readonly InserirProdutoCommandHandler _sut;
    private readonly IProdutoRepository _produtoRepository = Substitute.For<IProdutoRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    
    public InserirProdutoCommandHandlerTests()
    {
        _sut = new InserirProdutoCommandHandler(_produtoRepository, _unitOfWork);
    }
    
    [Fact]
    [Trait("Application", "Produtos")]
    public async Task Handle_DeveInserirProduto_QuandoDadosProdutosValidos()
    {
        // Arrange
        var command = new InserirProdutoCommand(ProdutoFactory.CriarInserirProdutoInputDto());
        _produtoRepository.Inserir(Arg.Any<Domain.Entities.Produto>()).Returns(Task.CompletedTask);
        _unitOfWork.CommitChangesAsync().Returns(Task.CompletedTask);
        
        // Act
        await _sut.Handle(command, CancellationToken.None);
        
        // Assert
        await _produtoRepository.Received(1).Inserir(Arg.Any<Domain.Entities.Produto>());
        await _unitOfWork.Received(1).CommitChangesAsync();
    }
}