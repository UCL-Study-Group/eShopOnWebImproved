using CatalogService.Infrastructure.Models;
using CatalogService.Infrastructure.Repositories;
using FluentResults;

namespace CatalogService.Application.Services;

public class CatalogAppService
{
    private readonly MongoRepository<CatalogItem> _repository;
    
    public CatalogAppService(MongoRepository<CatalogItem> repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<CatalogItem>>> GetAllAsync()
    {
        var result = await _repository.GetAllAsync();

        return result.IsFailed ? Result.Fail(result.Errors) : Result.Ok(result.Value);
    }

    public async Task<Result<CatalogItem>> GetByIdAsync(int id)
    {
        var result = await _repository.GetByIdAsync(id);
        
        return result.IsFailed ? Result.Fail<CatalogItem>(result.Errors) : Result.Ok(result.Value);
    }

    public async Task<Result> CreateAsync(CatalogItem catalogItem)
    {
        var result = await _repository.CreateAsync(catalogItem);
        
        return result.IsFailed ? Result.Fail(result.Errors) : Result.Ok();
    }

    public async Task<Result> UpdateAsync(CatalogItem catalogItem)
    {
        var result = await _repository.UpdateAsync(catalogItem);
        
        return result.IsFailed ? Result.Fail(result.Errors) : Result.Ok();
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var result = await _repository.DeleteAsync(id);
        
        return result.IsFailed ? Result.Fail(result.Errors) : Result.Ok();
    }
}