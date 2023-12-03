using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

//Remember to add it as a service
//This repository is responsible for getting data from the database, 
//and it will be used by the ProductController.
//What it does it is abstracting the data access layer from the controller.
public class ProductRepository : IProductRepository
{
    //Constructor
    //We need to inject the StoreContext, so we can access the database.
    private readonly StoreContext _context;
    public ProductRepository(StoreContext context)
    {
        _context = context;
    }

    
    public async Task<Product> GetProductByIdAsync(int id)
    {
        //include()
        //This method is used to include related entities in the query results.
        return await _context.Products
            .Include(p => p.ProductType)
            .Include(p => p.ProductBrand)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    
    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        //include()
        //This method is used to include related entities in the query results.
        return await _context.Products
            .Include(p => p.ProductType)
            .Include(p => p.ProductBrand)
            .ToListAsync();
    }

    
    public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
    {
        return await _context.ProductBrands.ToListAsync();
    }

    public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
    {
        return await _context.ProductTypes.ToListAsync();
    }
}