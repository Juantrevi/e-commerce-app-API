using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

//This class is used to configure our entities
//different than the default configuration on 
//how Entity Framework Core will create our Migrations
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    //This method is used to configure our entities
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        //Not necessary, because id is already required by default
        builder.Property(p => p.Id).IsRequired();
        
        //But name is not required by default, so we need to add it
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Description).IsRequired().HasMaxLength(180);
        
        //Configuring the decimal itself and the precision of the number of decimal places
        builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
        
        builder.Property(p => p.PictureUrl).IsRequired();
        
        builder.HasOne(b => b.ProductBrand).WithMany()
            .HasForeignKey(p => p.ProductBrandId);
        
        builder.HasOne(t => t.ProductType).WithMany()
            .HasForeignKey(p => p.ProductTypeId);
    }
    
}