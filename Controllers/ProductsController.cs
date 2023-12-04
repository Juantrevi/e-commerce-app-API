using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_app.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IGenericRepository<Product> _productsRepo;
    private readonly IGenericRepository<ProductBrand> _productBrandsRepo;
    private readonly IGenericRepository<ProductType> _productTypesRepo;
    
    public ProductsController(IGenericRepository<Product> productsRepo, 
        IGenericRepository<ProductBrand> productBrandsRepo, 
        IGenericRepository<ProductType> productTypesRepo)
    {
        _productsRepo = productsRepo;
        _productBrandsRepo = productBrandsRepo;
        _productTypesRepo = productTypesRepo;
    }
    
    //Returns a list of products as JSON Synchronous code
        // [HttpGet]
        // public ActionResult<List<Product>> GetProducts()
        // {
        //     var products = _context.Products.ToList();
        //     
        //     return Ok(products);
        // }
        
        
    [HttpGet]   
    //Returns a list of products as JSON Asynchronous code
        //Better for performance and scalability
        //Each time a request is made, a new thread is created and consumed in our web
        // server, so if we have a lot of requests, we will have a lot of threads
        // and this will consume a lot of memory, so we need to use async code to handle this.
        //Aso creating the Task, what we are saying is "Go get me some data" and when you are done
        //Task goes away and deals with that, in the meantime, that thread can go and handle other requests
    public async Task<ActionResult<List<Product>>> GetProducts()
    { 
        var spec = new ProductsWithTypesAndBrandsSpecification();
        
        //This method is from our GenericRepository, and takes a specification
        var products = await _productsRepo.ListAsync(spec);
        
        return Ok(products);
    }
    
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        //We call the constructor with params, and create a new instance
        var spec = new ProductsWithTypesAndBrandsSpecification(id);
        
        var product = await _productsRepo.GetEntityWithSpec(spec);
        
        return product;
    }
    
    
    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
    {
        var productBrands = await _productBrandsRepo.ListAllAsync();
        
        return Ok(productBrands);
    }
    
    
    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
    {
        var productTypes = await _productTypesRepo.ListAllAsync();
        
        return Ok(productTypes);
    }
}
