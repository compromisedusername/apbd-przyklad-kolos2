using Kolos2.DTOs;

namespace Kolos2.Services;

public interface IDbService
{
    Task<ICollection<GetOrderDto>?> GetOrdersData(string? clientLastName);
    Task<bool> DoesPastryExist(List<PastryDto> orderDtoPastries);
    Task<bool> DoesClientExist(int idClient);
    Task<bool> DoesEmployeeExist(int orderDtoEmployeeId);
    Task CreateOrder(int idClient, RequestOrderDto orderDto);
}