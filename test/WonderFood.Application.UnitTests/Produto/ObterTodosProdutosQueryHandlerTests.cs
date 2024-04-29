using AutoMapper;
using FluentAssertions;
using NSubstitute;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Application.Produtos.Queries.ObterTodosProdutos;
using WonderFood.Application.UnitTests.Produto.Factory;
using WonderFood.Domain.Dtos.Produto;

namespace WonderFood.Application.UnitTests.Produto;

public class ObterTodosProdutosQueryHandlerTests
{
    private readonly ObterTodosProdutosQueryHandler _sut;
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IProdutoRepository _produtoRepository = Substitute.For<IProdutoRepository>();

    public ObterTodosProdutosQueryHandlerTests()
    {
        _sut = new ObterTodosProdutosQueryHandler(_produtoRepository, _mapper);
    }

    [Fact]
    [Trait("Application", "Produtos")]
    public async Task Handle_DeveRetornarListaProdutos_QuandoHouveremProdutosCadastradosNaBase()
    {
        //Arrange
        var query = new ObterTodosProdutosQuery();
        var produtos = ProdutoFactory.CriarListaProdutosEntity();
        _produtoRepository.ObterTodosProdutos().Returns(produtos);
        _mapper.Map<IEnumerable<ProdutoOutputDto>>(produtos).Returns(ProdutoFactory.CriarListaProdutosOutputDto());
        
        //Act
        var result = await _sut.Handle(query, CancellationToken.None);
        
        //Assert
        result.Should().NotBeNullOrEmpty();
        result.Should().BeAssignableTo<IEnumerable<ProdutoOutputDto>>();
        result.Should().HaveCount(3);
    }
    
}