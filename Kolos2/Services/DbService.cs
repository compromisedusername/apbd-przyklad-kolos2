using Microsoft.EntityFrameworkCore;
using Kolos2.Data;
using Kolos2.DTOs;
using Kolos2.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Kolos2.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _dbContext;
    
    public DbService(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<ICollection<GetOrderDto>> GetOrdersData(string? clientLastName)
    {
        var orders = await _dbContext.Orders
           .Include(e => e.Client)
            .Include(e => e.OrderPastries)
            .ThenInclude(e => e.Pastry)
            .Where(e => clientLastName == null || e.Client.LastName == clientLastName)
            .ToListAsync();


        return orders.Select(e => new GetOrderDto()
        {
            Id = e.Id,
            AcceptedAt = e.AcceptedAt,
            FulfilledAt = e.FulfilledAt,
            Comments = e.Comments,
            Pastries = e.OrderPastries.Select(p => new PastryDto()
            {
                Name = p.Pastry.Name,
                Price = p.Pastry.Price,
                Amount = p.Amount
            }).ToList()
        }).ToList();

    }

    public async Task<bool> DoesPastryExist(List<PastryDto> orderDtoPastries)
    {
        foreach (var pastry in orderDtoPastries)
        {
            if (! await _dbContext.Pastries.AnyAsync(e => e.Name.Equals(pastry.Name)))
            {
                return false;
            }
        }

        return true;
    }

    public async Task<Pastry> GetPastryByName(PastryDto pastryDto)
    {
        var res = await _dbContext.Pastries.FirstOrDefaultAsync(e => e.Name.Equals(pastryDto.Name));
        if (res is null)
        {
            throw new Exception("Pastry doesn't exists!");
        }

        return res;
    }

    public async Task<bool> DoesClientExist(int idClient)
    {
        return await _dbContext.Clients.AnyAsync(e => e.Id == idClient);

    }

    public async Task<bool> DoesEmployeeExist(int orderDtoEmployeeId)
    {
        return await _dbContext.Employees.AnyAsync(e => e.Id == orderDtoEmployeeId);
    }

    public async Task CreateOrder(int idClient, RequestOrderDto orderDto)
    {

        var order = new Order()
        {
            AcceptedAt = orderDto.AcceptedAt,
            Comments = orderDto.Comments,
            EmployeeId = orderDto.EmployeeId,
            ClientId = idClient,
        };
        var pastries = new List<OrderPastry>();
        foreach (var pastry in orderDto.Pastries)
        {
            var dbpastry = await GetPastryByName(pastry);
            pastries.Add(new OrderPastry()
            {
                PastryId = dbpastry.Id,
                Amount = pastry.Amount,
                Comment = orderDto.Comments,
                Order = order
            });
        }

        await _dbContext.AddAsync(order);
        await _dbContext.AddRangeAsync(pastries);

        await _dbContext.SaveChangesAsync();

    }
}