﻿using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications;

//Method
//to include related entities
public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
{
    //Using constructor WITH PARAMS
    //with parameters from BaseSpecification using criteria
    public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
    {
        AddInclude(x => x.ProductType);
        AddInclude(x => x.ProductBrand);
    }

    //Using constructor WITH NO PARAMS
    //with no parameters from BaseSpecification
    public ProductsWithTypesAndBrandsSpecification()
    {
        AddInclude(x => x.ProductType);
        AddInclude(x => x.ProductBrand);
    }
}