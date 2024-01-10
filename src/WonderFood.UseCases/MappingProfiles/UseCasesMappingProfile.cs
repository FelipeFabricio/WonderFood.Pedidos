using AutoMapper;
using WonderFood.Core.Dtos;
using WonderFood.Core.Entities;

namespace WonderFood.UseCases.MappingProfiles;

public class UseCasesMappingProfile : Profile
{
    public UseCasesMappingProfile()
    {
        CreateMap<Core.Entities.Cliente, ClienteOutputDto>().ReverseMap();
        CreateMap<Core.Entities.Cliente, InserirClienteInputDto>().ReverseMap();
        CreateMap<Core.Entities.Cliente, AtualizarClienteInputDto>().ReverseMap();
        CreateMap<Produto, ProdutoOutputDto>().ReverseMap();
        CreateMap<Produto, InserirProdutoInputDto>().ReverseMap();
        CreateMap<Pedido, StatusPedidoOutputDto>().ReverseMap();
        CreateMap<Pedido, PedidosOutputDto>().ReverseMap();
        CreateMap<ProdutosPedido, ProdutosPedidoOutputDto>()
            .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Produto.Nome))
            .ForMember(dest => dest.Valor, opt => opt.MapFrom(src => src.Produto.Valor))
            .ForMember(dest => dest.Quantidade, opt => opt.MapFrom(src => src.Quantidade));
    }
}