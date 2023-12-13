using System.Linq.Expressions;

namespace Core.Specifications;

public class BaseSpecification<T> : ISpecification<T>
{
    
    public BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    public BaseSpecification()
    {
    }

    public Expression<Func<T, bool>> Criteria { get; }
    
    public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

    public Expression<Func<T, object>> OrderBy { get; private set; }
    
    public Expression<Func<T, object>> OrderByDescending { get; private set; }
    
    public int Take { get; private set; }
    
    public int Skip { get; private set; }
    
    public bool IsPagingEnabled { get; private set; }


    //This method is used to add an include to our Includes list
    protected void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }
    
    //method to order by
    protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }
    
    //method to order by descending
    protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
    {
        OrderByDescending = orderByDescExpression;
    }
    
    //Pagination
    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }
    

    
}