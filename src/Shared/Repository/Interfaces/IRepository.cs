using System.Linq.Expressions;

namespace Shared.Interfaces;

public interface IRepository<T> where T : IEntity
{
    Task CreateAsync(T model);

    Task<IReadOnlyCollection<T>> GetAllAsync();

    Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter);

    Task<T> GetAsync(Expression<Func<T, bool>> filter);

    Task<T> GetAsync(Guid Id);

    Task DeleteAsync(Guid Id);

    Task UpdateAsync(T entity);
}
