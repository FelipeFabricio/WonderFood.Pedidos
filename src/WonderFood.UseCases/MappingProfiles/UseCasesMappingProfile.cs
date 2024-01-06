using AutoMapper;

namespace WonderFood.UseCases.MappingProfiles;

public class UseCasesMappingProfile : Profile
{
    public UseCasesMappingProfile()
    {
        CreateMap<Core.Entities.Cliente, Core.Dtos.ClienteDto>().ReverseMap();
        CreateMap<Core.Entities.Cliente, Core.Dtos.InserirClienteInputDto>().ReverseMap();
        CreateMap<Core.Entities.Cliente, Core.Dtos.AtualizarClienteInputDto>().ReverseMap();
    }
}