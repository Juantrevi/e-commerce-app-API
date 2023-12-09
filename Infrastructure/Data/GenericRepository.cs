using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly StoreContext _context;

    public GenericRepository(StoreContext context)
    {
        _context = context;
    }
    
    public async Task<T> GetByIdAsync(int id)
    {
        /*
        .Set<T>()
        method we can access the entities of type T 
        */
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        /*
        With the .Set<T>()
        method we can access the entities of type T
        But we need the .include() method to include related entities
        But here we can't use it, because we don't know the type of T
        that's why we need to use IQueryable<T> as specification class
        which will return the type of T 
        */
        return await _context.Set<T>().ToListAsync();
    }

    /*
    Implementation 
    of the specification methods, using the ApplySpecification method
    */
    public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }
    
    //This method allows to apply specification to the query
    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
    }
    
}