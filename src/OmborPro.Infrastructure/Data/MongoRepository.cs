using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using OmborPro.Application.Common.Interfaces;
using OmborPro.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OmborPro.Infrastructure.Data;

public class MongoRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly IMongoCollection<T> _collection;

    public MongoRepository(IOptions<MongoDbSettings> settings)
    {
        // Register Guid serialization convention if not already registered
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);
        
        // Collection name is usually the class name pluralized or just the class name
        _collection = database.GetCollection<T>(typeof(T).Name.ToLower() + "s");
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _collection.Find(x => x.Id == id && x.DeletedAt == null).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _collection.Find(x => x.DeletedAt == null).ToListAsync();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        // Combine predicate with soft-delete filter
        var filter = Builders<T>.Filter.And(
            Builders<T>.Filter.Where(predicate),
            Builders<T>.Filter.Eq(x => x.DeletedAt, null)
        );
        return await _collection.Find(filter).ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
    }

    public async Task DeleteAsync(T entity)
    {
        // Soft delete
        entity.DeletedAt = DateTime.UtcNow;
        await UpdateAsync(entity);
    }
}
