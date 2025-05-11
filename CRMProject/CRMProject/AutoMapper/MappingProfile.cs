using AutoMapper;
using DataAcccess.DBContext;
using DataAcccess.DTO;

namespace CRMProject.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();
        }
    }
}
