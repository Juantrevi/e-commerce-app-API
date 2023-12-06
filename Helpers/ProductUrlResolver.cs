using AutoMapper;
using Core.Entities;
using e_commerce_app.Dtos;

namespace e_commerce_app.Helpers;
//Class
//This class is used to resolve the url of the product image
//and add the localhost url we placed in the appsettings.json file
public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
{
    private readonly IConfiguration _config;

    public ProductUrlResolver(IConfiguration config)
    {
        _config = config;
    }
    
    //After
    //We need to tell AutoMapper (MappingProfile) how to map.
    public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
    {
        if(!string.IsNullOrEmpty(source.PictureUrl))
        {
            return _config["ApiUrl"] + source.PictureUrl;
        }

        return null;

    }
}