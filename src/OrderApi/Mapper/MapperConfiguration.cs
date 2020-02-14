using AutoMapper;
using AutoMapperRegister;
using OrderApi.Models;

namespace OrderApi.Mapper
{
    [MapperConfigurator]
    public class MapperConfiguration : Profile
    {
        public MapperConfiguration()
        {
            CreateMap<Domain.EFCoreEntities.Order, OrderDetails>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
                .ForMember(dest => dest.Quantity, src => src.MapFrom(x => x.Quantity))
                .ReverseMap();
        }
    }
}
