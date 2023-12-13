using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications;
/*
 Method
 to include related entities
*/
public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
{
    
    /*
Using constructor WITH NO PARAMS
with no parameters from BaseSpecification
*/
    /*
     NO LONGER NO PARAMS
        Now we have params from the query string, we use it to 
        filter the products, and we pass it to the base constructor
     */
    public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams) 
        : base(x => 
            (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId)
            && 
            (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId))
    {
        AddInclude(x => x.ProductType);
        AddInclude(x => x.ProductBrand);
        AddOrderBy(x => x.Name);
        ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);
        
        /*
         Sorting
         Sorting is done by the query string
         */
        if (!string.IsNullOrEmpty(productParams.Sort))
        {
            switch (productParams.Sort)
            {
                case "priceAsc":
                    AddOrderBy(p => p.Price);
                    break;
                case "priceDesc":
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    AddOrderBy(n => n.Name);
                    break;
            }
        }
    }
    
    
    /*
    Using constructor WITH PARAMS
    with parameters from BaseSpecification using criteria
    */ 
    public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
    {
        AddInclude(x => x.ProductType);
        AddInclude(x => x.ProductBrand);
    }


    
}