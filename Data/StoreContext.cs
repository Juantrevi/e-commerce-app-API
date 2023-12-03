using e_commerce_app.Entities;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_app.Data;

public class StoreContext : DbContext
{
    public StoreContext(DbContextOptions options) : base(options)
    {
        
        
    }
    
    public DbSet<Product> Products { get; set; }
}