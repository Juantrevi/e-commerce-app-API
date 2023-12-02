﻿using Microsoft.AspNetCore.Mvc;

namespace e_commerce_app.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public string GetProducts()
    {
        return "This will be a list of products";
    }
    
    [HttpGet("{id}")]
    public string GetProduct(int id)
    {
        return "This will be a product";
    }
}
