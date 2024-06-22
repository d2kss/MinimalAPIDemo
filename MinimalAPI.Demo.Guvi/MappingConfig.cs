using AutoMapper;
using MinimalAPI.Demo.Guvi.Models.DTO;

namespace MinimalAPI.Demo.Guvi
{
    public class MappingConfig:Profile
    {
        public MappingConfig() {
            CreateMap<Product, ProductCreateDTO>()
            // .ForMember(d=>d.DisplayOrder1,o=>o.MapFrom(s=>s.DisplayOrder))
            .ReverseMap();

            CreateMap<Product, ProductDTO>()
            // .ForMember(d=>d.DisplayOrder1,o=>o.MapFrom(s=>s.DisplayOrder))
            .ReverseMap();

        }
    }
}
