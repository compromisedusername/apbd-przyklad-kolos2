using System.ComponentModel.DataAnnotations;

namespace Kolos2.Models;
public class Employee
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public string FirstName { get; set; }
    [MaxLength(120)]
    public string LastName { get; set; }

    public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
}