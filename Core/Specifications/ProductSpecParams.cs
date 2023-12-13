namespace Core.Specifications;
/*
 Class
 This class is to setup the parameters that we are going to take for
 our ProductController.cs when we get a list of products and we want to filter them
 or paging them.
 The idea is to not take individual strings or parameters in our
    ProductController.cs, but to take a class that has all the parameters
 */
public class ProductSpecParams
{
    private const int MaxPageSize = 50;
    
    public int PageIndex { get; set; } = 1;
    
    //This is for the client to be able to set the page size (From 1 to 50)
    private int _pageSize = 6;
    
    /*
     This is to be more specific
     */
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
    
    public int? BrandId { get; set; }
    
    public int? TypeId { get; set; }
    
    public string Sort { get; set; }
}