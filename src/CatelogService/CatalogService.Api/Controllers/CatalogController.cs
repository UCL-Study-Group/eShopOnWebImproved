using CatalogService.Application.Services;
using CatalogService.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Api.Controllers;

[ApiController, Route("api/v1/")]
public class CatalogController : Controller
{
    private readonly CatalogAppService _catalogService;

    public CatalogController(CatalogAppService catalogService)
    {
        _catalogService = catalogService;
    }
    
    [HttpGet("/Catalog")]
    public async Task<ActionResult<IEnumerable<CatalogItem>>> GetAllAsync()
    {
        var response = await _catalogService.GetAllAsync();
        
        if (response.IsFailed)
            return Problem(response.Errors[0].Message);
        
        if (!response.Value.Any())
            return NotFound();
        
        return Ok(response.Value);
    }

    [HttpPost("/Catalog")]
    public async Task<ActionResult> PostAsync([FromBody] CatalogItem item)
    {
        var response = await _catalogService.CreateAsync(item);

        return response.IsFailed ? Problem(response.Errors[0].Message) : Created();
    }
}