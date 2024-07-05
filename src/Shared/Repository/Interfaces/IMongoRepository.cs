namespace Shared.Interfaces;

public interface IMongoRepository<T> : IRepository<T> where T : IEntity
{
}