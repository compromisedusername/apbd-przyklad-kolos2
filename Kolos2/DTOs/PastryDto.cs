using System.ComponentModel.DataAnnotations;

namespace Kolos2.DTOs;
public class PastryDto
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    [Range(0,double.MaxValue)]
    public decimal Price { get; set; } 
    [Required]
    [Range(1,int.MaxValue)]
    public int Amount { get; set; } 
}