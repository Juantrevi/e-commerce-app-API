using System.Linq.Expressions;

namespace Core.Specifications;

public interface ISpecification<T>
{
    //This is the criteria that we will use to filter our entities (Like a Where clause)
    Expression<Func<T, bool>> Criteria { get; }
    
    //This is the criteria that we will use to include things in our entities (Like include clause)
    List<Expression<Func<T, object>>> Includes { get; }
}