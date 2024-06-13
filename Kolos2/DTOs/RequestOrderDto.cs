using System.ComponentModel.DataAnnotations;
using Kolos2.Models;

namespace Kolos2.DTOs;

public class RequestOrderDto
{
    [Required]
    public int EmployeeId { get; set; }
    [Required]
    public DateTime AcceptedAt { get; set; }
    [Required]
    public string Comments { get; set; }
    public List<PastryDto> Pastries { get; set; }
}