namespace CatalogService.Infrastructure.Models;

public class CatalogItem
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required decimal Price { get; init; }
    public required string ImageUrl { get; init; }
    public required CatalogType CatalogType { get; init; }
    public required CatalogBrand CatalogBrand { get; init; }
}