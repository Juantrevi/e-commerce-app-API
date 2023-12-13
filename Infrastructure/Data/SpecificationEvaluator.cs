using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;
/*
This class 
is used to evaluate our specification
and return an IQueryable of our entity.
We constraint this to use only with our entity classes
*/
public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
{
    
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
    {
        var query = inputQuery;
        
        /*
         ORDER
         Is important, if we are filtering our results, we dont
         want to page our results before knowing which ones we are returning.
         So the paging is the last thing we do, after filtering and sorting
         */
        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria); // example: p => p.ProductTypeId == id
        }
        
        if (spec.OrderBy != null)
        {
            query = query.OrderBy(spec.OrderBy); 
        }
        
        if (spec.OrderByDescending != null)
        {
            query = query.OrderByDescending(spec.OrderByDescending); 
        }
        
        if (spec.IsPagingEnabled)
        {
            query = query.Skip(spec.Skip).Take(spec.Take);
        }
        
        /*
        Aggregate
        We are combining all out includes into one query
        */
        query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
        
        Console.WriteLine(query);
        return query;
    }
    
}