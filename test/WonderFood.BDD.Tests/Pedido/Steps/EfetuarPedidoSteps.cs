using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using TechTalk.SpecFlow;
using WonderFood.Application.Common.Interfaces;
using WonderFood.Application.Pedidos.Commands.Inserir;
using WonderFood.BDD.Tests.Common;
using WonderFood.Domain.Dtos.Pedido;
using WonderFood.Domain.Dtos.Produto;
using WonderFood.Domain.Entities;
using WonderFood.Domain.Entities.Enums;
using WonderFood.Infra.Sql.Clientes;
using WonderFood.Infra.Sql.Context;
using WonderFood.Infra.Sql.Pedidos;
using WonderFood.Infra.Sql.Produtos;
using Xunit;

namespace WonderFood.BDD.Tests.Pedido.Steps;

[Binding]
public class EfetuarPedidoSteps
{
    private Cliente _cliente;
    private InserirPedidoInputDto _pedidoInput;
    private readonly IUnitOfWork _unitOfWork;
    private PedidosOutputDto _pedidoOutput;
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IProdutoRepository _produtoRepository;
    private readonly IMapper _mapper;
    private readonly WonderFoodContext _context;
    private readonly InMemoryDbContextFactory _dbContextFactory;
    private readonly ServiceProvider _serviceProvider;
    private readonly InserirPedidoCommandHandler _inserirPedidoCommandHandler;
    private readonly IWonderFoodPagamentoExternal _pagamentosExternal = Substitute.For<IWonderFoodPagamentoExternal>();

    public EfetuarPedidoSteps()
    {
        var serviceCollection = new ServiceCollection();
        _dbContextFactory = new InMemoryDbContextFactory();
        _context = _dbContextFactory.CreateContext();

        serviceCollection.AddSingleton(_context);
        serviceCollection.AddScoped<IPedidoRepository, PedidoRepository>();
        serviceCollection.AddScoped<IClienteRepository, ClienteRepository>();
        serviceCollection.AddScoped<IProdutoRepository, ProdutoRepository>();
        serviceCollection.AddScoped<IUnitOfWork>(serviceProvider =>
            serviceProvider.GetRequiredService<WonderFoodContext>());

        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Domain.Entities.Pedido, PedidosOutputDto>().ReverseMap();
            cfg.CreateMap<ProdutosPedido, ProdutosPedidoOutputDto>()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Produto.Nome))
                .ForMember(dest => dest.Valor, opt => opt.MapFrom(src => src.Produto.Valor))
                .ForMember(dest => dest.Categoria, opt => opt.MapFrom(src => src.Produto.Categoria))
                .ForMember(dest => dest.ProdutoId, opt => opt.MapFrom(src => src.ProdutoId))
                .ForMember(dest => dest.Quantidade, opt => opt.MapFrom(src => src.Quantidade));
        });
        IMapper mapper = configuration.CreateMapper();
        serviceCollection.AddSingleton(mapper);
        
        _serviceProvider = serviceCollection.BuildServiceProvider();
        _pedidoRepository = _serviceProvider.GetRequiredService<IPedidoRepository>();
        _clienteRepository = _serviceProvider.GetRequiredService<IClienteRepository>();
        _produtoRepository = _serviceProvider.GetRequiredService<IProdutoRepository>();
        _unitOfWork = _serviceProvider.GetRequiredService<WonderFoodContext>();
        _mapper = _serviceProvider.GetRequiredService<IMapper>();

        _inserirPedidoCommandHandler = new InserirPedidoCommandHandler(_pedidoRepository, _unitOfWork,
            _clienteRepository, _produtoRepository, _pagamentosExternal, _mapper);
    }


    [Given(@"que o cliente possui um cadastro válido no restaurante")]
    public async Task GivenQueOClientePossuiUmCadastroValidoNoRestaurante()
    {
        var idCliente = Guid.Parse("945f7aea-ae9e-43d0-9f8f-737c2a56f710");
        _cliente = await _clienteRepository.ObterClientePorId(idCliente);

        Assert.NotNull(_cliente);
    }

    [Given(@"o Pedido não possui produtos")]
    public void GivenOPedidoNaoPossuiProdutos()
    {
        _pedidoInput = new InserirPedidoInputDto
        {
            ClienteId = _cliente.Id,
            FormaPagamento = FormaPagamento.CartaoCredito,
            Observacao = "Pedido sem produtos",
            Produtos = new List<InserirProdutosPedidoInputDto>()
        };
    }


    [Given(@"o Pedido possui pelo menos um produto")]
    public void GivenOPedidoPossuiPeloMenosUmProduto()
    {
        _pedidoInput = new InserirPedidoInputDto
        {
            ClienteId = _cliente.Id,
            FormaPagamento = FormaPagamento.CartaoCredito,
            Observacao = "Pedido com produtos",
            Produtos = new List<InserirProdutosPedidoInputDto>
            {
                new()
                {
                    ProdutoId = Guid.Parse("e3547b5e-f97e-4e6b-a64b-54858baf7874"),
                    Quantidade = 1
                }
            }
        };
    }


    [When(@"o cliente efetuar o pedido")]
    public async Task WhenOClienteEfetuarOPedido()
    {
        var command = new InserirPedidoCommand(_pedidoInput);
        _pedidoOutput = await _inserirPedidoCommandHandler.Handle(command, new CancellationToken());
    }
    
    [Then(@"o cliente deverá ter seu pedido registrado no sistema com sucesso")]
    public void ThenOClienteDeveraTerSeuPedidoRegistradoNoSistemaComSucesso()
    {
        var pedido = _pedidoRepository.ObterPorId(_pedidoOutput.Id);
        Assert.NotNull(pedido);
    }

    [Then(@"o cliente deverá ser informado que o pedido está aguardando pagamento")]
    public void ThenOClienteDeveraSerInformadoQueOPedidoEstaAguardandoPagamento()
    {
        Assert.Equal(StatusPedido.AguardandoPagamento.ToString(), _pedidoOutput.Status);
    }
}