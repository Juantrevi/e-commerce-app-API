namespace e_commerce_app.Helpers;
/*
 Class
 Pagination, we make it generic so we can use it for any type of entity
 */
public class Pagination<T> where T : class
{
    public Pagination(int pageIndex, int pageSize, int count, IReadOnlyList<T> data)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        Count = count;
        Data = data;
    }

    public int PageIndex { get; set; }
    
    public int PageSize { get; set; }
    
    //This is the total number of items in our query, but the user will give us the page size
    public int Count { get; set; }
    
    public IReadOnlyList<T> Data { get; set; }
}