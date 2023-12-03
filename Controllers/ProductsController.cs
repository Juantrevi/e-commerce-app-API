using e_commerce_app.Data;
using e_commerce_app.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_app.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly StoreContext _context;

    public ProductsController(StoreContext context)
    {
        _context = context;
        
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
        var products = await _context.Products.ToListAsync();
        
        return products;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        
        return product;
    }
}
