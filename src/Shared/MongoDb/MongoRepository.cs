using Shared.Interfaces;
using System.Linq.Expressions;

namespace Shared.MongoDb;

public class MongoRepository<T> : IRepository<T> where T : IEntity
{
    private readonly IMongoCollection<T> _dbCollection;
    private readonly FilterDefinitionBuilder<T> _filterBuilder = Builders<T>.Filter;

    public MongoRepository(IMongoDatabase database, string collectionName)
    {
        _dbCollection = database.GetCollection<T>(collectionName);
    }

    public async Task CreateAsync(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity)); // should be a better way with c# 12
        }
        await _dbCollection.InsertOneAsync(entity);
    }

    public Task DeleteAsync(Guid Id) 
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyCollection<T>> GetAllAsync()
    {
        return await _dbCollection.Find(_filterBuilder.Empty).ToListAsync();
    }
    

    public async Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter)
    {
        return await _dbCollection.Find(filter).ToListAsync();

    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
    {
        return await _dbCollection.Find(filter).FirstOrDefaultAsync();
    }


    public async Task<T> GetAsync(Guid Id)
    {
        FilterDefinition<T> filter = _filterBuilder.Eq(entity => entity.Id, Id);
        return await _dbCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        if(entity == null)
        {
            throw new ArgumentNullException(nameof(entity)); // should be a better way with c# 12
        }

        FilterDefinition<T> filter = _filterBuilder.Eq(exisitingEntity => exisitingEntity.Id, entity.Id);
        await _dbCollection.ReplaceOneAsync(filter, entity);
    }

    // needs docker add in doc
}
