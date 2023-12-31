﻿using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using e_commerce_app.Dtos;
using e_commerce_app.Errors;
using e_commerce_app.Helpers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_app.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : BaseApiController
{
    private readonly IGenericRepository<Product> _productsRepo;
    private readonly IGenericRepository<ProductBrand> _productBrandsRepo;
    private readonly IGenericRepository<ProductType> _productTypesRepo;
    private readonly IMapper _mapper;

    public ProductsController(IGenericRepository<Product> productsRepo, 
        IGenericRepository<ProductBrand> productBrandsRepo, 
        IGenericRepository<ProductType> productTypesRepo,
        IMapper mapper)
        
    {
        _productsRepo = productsRepo;
        _productBrandsRepo = productBrandsRepo;
        _productTypesRepo = productTypesRepo;
        _mapper = mapper;
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
    public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams productParams)
    { 
        
        var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
        
        var countSpec = new ProductWithFiltersForCountSpecification(productParams);
        
        var totalItems = await _productsRepo.CountAsync(countSpec);
        
        var products = await _productsRepo.ListAsync(spec);
        
        var data = _mapper
            .Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
        
        return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize, totalItems, data));
        
    }
    
    
    [HttpGet("{id}")]
    //Swagger documentation
    //We are telling swagger that the response type is 200 or 404
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {
        //We call the constructor with params, and create a new instance
        var spec = new ProductsWithTypesAndBrandsSpecification(id);
        
        var product = await _productsRepo.GetEntityWithSpec(spec);

        if (product == null)
        {
            return NotFound(new ApiResponse(404));
        }
        
        return _mapper.Map<Product, ProductToReturnDto>(product);
        
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
