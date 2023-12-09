using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

/*
Creating Migrations:
dotnet ef database drop -p Infrastructure
dotnet ef migrations remove -p Infrastructure
dotnet ef migrations add InitialCreate -p Infrastructure -o Data/Migrations
*/
public class StoreContext : DbContext
{
    public StoreContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductBrand> ProductBrands { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }


    /*
    This method is used to configure our entities as we did it in the Config folder 
    ProductConfiguration class 
    */
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}