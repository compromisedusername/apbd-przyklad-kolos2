using Kolos2.Models;
using Kolos2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kolos2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{

    private readonly IDbService _dbService;

    public OrdersController(IDbService dbService)
    {
        _dbService = dbService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetOrdersData(string? clientLastName = null)
    {
        var orders = await _dbService.GetOrdersData(clientLastName);
        return Ok(orders);
    }
    
    
}