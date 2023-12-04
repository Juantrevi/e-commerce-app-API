namespace e_commerce_app.Dtos;

//DTO is a container to move data between layers
//DTOs are commonly used by services to expose data to consumers
//They dont contain any business logic
public class ProductToReturnDto
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public decimal Price { get; set; }
    
    public string PictureUrl { get; set; }
    
    public string ProductType { get; set; }
    
    public string ProductBrand { get; set; }
    
}