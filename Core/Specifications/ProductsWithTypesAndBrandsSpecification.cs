﻿using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications;

//Method
//to include related entities
public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
{
    public ProductsWithTypesAndBrandsSpecification()
    {
        AddInclude(x => x.ProductType);
        AddInclude(x => x.ProductBrand);
    }
}