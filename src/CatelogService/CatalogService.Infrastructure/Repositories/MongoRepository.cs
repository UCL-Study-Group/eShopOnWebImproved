using CatalogService.Infrastructure.Context;
using CatalogService.Infrastructure.Interfaces;
using CatalogService.Infrastructure.Models;
using FluentResults;
using MongoDB.Driver;

namespace CatalogService.Infrastructure.Repositories;

public class MongoRepository<T> : MongoDbContext, IRepository<T> where T : CatalogItem
{
    private readonly IMongoCollection<T> _collection;

    public MongoRepository()
    {
        _collection = Database.GetCollection<T>(typeof(T).Name);
    }
    
    /// <summary>
    /// Retrieves all entities of the provided type from the database
    /// </summary>
    /// <returns></returns>
    public async Task<Result<IEnumerable<T>>> GetAllAsync()
    {
        try
        {
            var items = await _collection.Find(_ => true).ToListAsync();

            return Result.Ok(items.AsEnumerable());
        }
        catch (Exception)
        {
            return Result.Fail("Could not retrieve collection");
        }
    }

    /// <summary>
    /// Retrieves the first entity of the provided type with matching id
    /// </summary>
    /// <param name="id">The id of the type</param>
    /// <returns></returns>
    public async Task<Result<T>> GetByIdAsync(int id)
    {
        try
        {
            var query = await _collection.FindAsync(e => e.Id == id);

            return Result.Ok(query.FirstOrDefault());
        }
        catch (Exception)
        {
            return Result.Fail("Couldn't find an item with the specified id");
        }
    }

    /// <summary>
    /// Creates a new entity of the provided type and saves it
    /// </summary>
    /// <param name="entity">The entity that is to be created</param>
    /// <returns></returns>
    public async Task<Result> CreateAsync(T entity)
    {
        try
        {
            await _collection.InsertOneAsync(entity);

            return Result.Ok();
        }
        catch (Exception)
        {
            return Result.Fail("Couldn't create provided item");
        }
    }

    /// <summary>
    /// Updates an already existing entity with provided one
    /// </summary>
    /// <param name="entity">The entity which should be updated</param>
    /// <returns></returns>
    public async Task<Result> UpdateAsync(T entity)
    {
        try
        {
            var filter = Builders<T>.Filter.Eq(e => e.Id, entity.Id);
            var options = new ReplaceOptions { IsUpsert = true };
            
            
            await _collection.ReplaceOneAsync(filter, entity, options);
            
            return Result.Ok();
        } catch (Exception)
        {
            return Result.Fail("Couldn't update provided item");
        }
    }

    /// <summary>
    /// Removes the entity matching the id provided 
    /// </summary>
    /// <param name="id">The id of the entity to be deleted</param>
    /// <returns></returns>
    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            await _collection.DeleteOneAsync(e => e.Id == id);
            
            return Result.Ok();
        }
        catch (Exception)
        {
            return Result.Fail("Failed to delete provided id");
        }
    }
}