using AutoMapper;
using Core.Entities;
using e_commerce_app.Dtos;

namespace e_commerce_app.Helpers;

//Profile
//Is an AutoMapper class that we can use to configure our mappings
public class MappingProfiles : Profile
{
    //Constructor
    public MappingProfiles()
    {
        //As long as the property names match, AutoMapper will map them automatically
        //Add it to the program.cs as a service.
        //We need to tell AutoMapper how to map the ProductType and ProductBrand
        CreateMap<Product, ProductToReturnDto>()
            .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
            .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
            .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
    }
    
}