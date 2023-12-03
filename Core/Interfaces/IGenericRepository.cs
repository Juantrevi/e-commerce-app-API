using Core.Entities;

namespace Core.Interfaces;
//Generic repository
//This interface will be implemented as a generic repository
//for all our entities, it is one way to avoid code duplication
//and to make our code more maintainable.
//It will be implemented in Infrastructure/Data/GenericRepository.cs
public interface IGenericRepository<T> where T : BaseEntity
{
    
    Task<T> GetByIdAsync(int id);
    
    //IReadOnlyList is a list that cannot be modified,
    Task<IReadOnlyList<T>> ListAllAsync();
    
}