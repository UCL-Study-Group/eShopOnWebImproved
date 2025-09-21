using MongoDB.Driver;

namespace CatalogService.Infrastructure.Context;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext()
    {
        var jsonConfig = Path.Combine(Directory.GetCurrentDirectory(), "Properties", "config.json");

        if (!File.Exists(jsonConfig))
            throw new FileNotFoundException("Found no configuration file");
    }
}