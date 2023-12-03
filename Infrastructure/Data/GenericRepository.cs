using Core.Entities;
using Core.Interfaces;
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
        //With the .Set<T>() 
        //method we can access the entities of type T
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        //With the .Set<T>() 
        // method we can access the entities of type T
        //But we need the .include() method to include related entities
        //But here we can't use it, because we don't know the type of T
        //that's why we need to use
        return await _context.Set<T>().ToListAsync();
    }
}