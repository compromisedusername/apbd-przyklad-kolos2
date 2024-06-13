using System.Transactions;
using Kolos2.DTOs;
using Kolos2.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Kolos2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{

    private readonly IDbService _dbContext;

    public ClientsController(IDbService dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("{idClient:int}/orders")]
    public async Task<IActionResult> CreateOrder(int idClient, RequestOrderDto orderDto)
    {

        if (! await _dbContext.DoesPastryExist(orderDto.Pastries))
        {
            return BadRequest("Given pastry doesnt exist!");
        }

        if (! await _dbContext.DoesClientExist(idClient))
        {
            return BadRequest("Client " + idClient + " does not exist!");
        }
        
        if (! await _dbContext.DoesEmployeeExist(orderDto.EmployeeId))
        {
            return BadRequest("Employee " + orderDto.EmployeeId + " does not exist!");
        }

        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            await _dbContext.CreateOrder(idClient, orderDto);
            
            scope.Complete();
        }
        
        return CreatedAtAction(nameof(CreateOrder), new {idClient}, orderDto);
    }
    
}