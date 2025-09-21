namespace CatalogService.Infrastructure.Models;

public class DbCredentials
{
    public required string ConnectionString { get; set; }
    public required string DatabaseName { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}