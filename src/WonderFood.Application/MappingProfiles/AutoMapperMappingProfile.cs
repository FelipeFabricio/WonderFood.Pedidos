using AutoMapper;
using WonderFood.Domain.Dtos.Cliente;
using WonderFood.Domain.Dtos.Pedido;
using WonderFood.Domain.Dtos.Produto;
using WonderFood.Domain.Entities;

namespace WonderFood.Application.MappingProfiles;

public class AutoMapperMappingProfile : Profile
{
    public AutoMapperMappingProfile()
    {
        CreateMap<Cliente, ClienteOutputDto>().ReverseMap();
        CreateMap<Cliente, InserirClienteInputDto>().ReverseMap();
        CreateMap<Cliente, AtualizarClienteInputDto>().ReverseMap();
        CreateMap<InserirClienteInputDto, ClienteOutputDto>().ReverseMap();
        
        CreateMap<Produto, ProdutoOutputDto>().ReverseMap();
        CreateMap<Produto, InserirProdutoInputDto>().ReverseMap();
        
        CreateMap<Pedido, StatusPedidoOutputDto>().ReverseMap();
        CreateMap<Pedido, PedidosOutputDto>().ReverseMap();
        CreateMap<Pedido, InserirPedidoInputDto>().ReverseMap();
        CreateMap<Pedido, InserirPedidoOutputDto>().ReverseMap();
        
        CreateMap<ProdutosPedido,InserirProdutosPedidoInputDto>().ReverseMap();
        CreateMap<ProdutosPedido, ProdutosPedidoOutputDto>()
            .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Produto.Nome))
            .ForMember(dest => dest.Valor, opt => opt.MapFrom(src => src.Produto.Valor))
            .ForMember(dest => dest.Categoria, opt => opt.MapFrom(src => src.Produto.Categoria))
            .ForMember(dest => dest.ProdutoId, opt => opt.MapFrom(src => src.ProdutoId))
            .ForMember(dest => dest.Quantidade, opt => opt.MapFrom(src => src.Quantidade));
    }
}