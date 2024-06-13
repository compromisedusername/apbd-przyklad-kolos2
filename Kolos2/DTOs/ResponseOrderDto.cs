using System.Collections;
using System.ComponentModel.DataAnnotations;
using Kolos2.Models;

namespace Kolos2.DTOs;

public class GetOrderDto
{
    [Required]

    public int Id { get; set; }
    
    [Required]

    public DateTime AcceptedAt { get; set; }
    [Required]
    
    public DateTime? FulfilledAt { get; set; }
    [Required]
    
    public string? Comments { get; set; }
    [Required]

    public ICollection<PastryDto> Pastries { get; set; } = null!;

    

}

