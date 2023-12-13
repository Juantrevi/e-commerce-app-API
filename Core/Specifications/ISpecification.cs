using System.Linq.Expressions;

namespace Core.Specifications;

public interface ISpecification<T>
{
    //This is the criteria that we will use to filter our entities (Like a Where clause)
    Expression<Func<T, bool>> Criteria { get; }
    
    //This is the criteria that we will use to include things in our entities (Like include clause)
    List<Expression<Func<T, object>>> Includes { get; }
    
    //Add support for additional expressions to order our entities (Like OrderBy clause)
    Expression<Func<T, object>> OrderBy { get; }
    
    Expression<Func<T, object>> OrderByDescending { get; }
    
    //Pagination
    int Take { get; }
    
    int Skip { get; }
    
    bool IsPagingEnabled { get; }
    
}