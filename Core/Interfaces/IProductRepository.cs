using Core.Entities;

namespace Core.Interfaces;

//Interface will be implemented in Infrastructure/Data/ProductRepository.cs
//and will be injected in Controllers/ProductsController.cs
public interface IProductRepository
{
    Task<Product> GetProductByIdAsync(int id);
    
    //IReadOnlyList is a list that cannot be modified,
    //it is read only (More specific than IEnumerable)
    Task<IReadOnlyList<Product>> GetProductsAsync();
    
    Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
    
    Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
    
}