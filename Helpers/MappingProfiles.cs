using AutoMapper;
using Core.Entities;
using e_commerce_app.Dtos;

namespace e_commerce_app.Helpers;

//Profile, is an AutoMapper class that we can use to configure our mappings
public class MappingProfiles : Profile
{
    //Constructor
    public MappingProfiles()
    {
        //As long as the property names match, AutoMapper will map them automatically
        //Add it to the program.cs as a service.
        CreateMap<Product, ProductToReturnDto>();
    }
    
}