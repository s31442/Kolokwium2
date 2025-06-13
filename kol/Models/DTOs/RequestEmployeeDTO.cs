using System.ComponentModel.DataAnnotations;

namespace kol.Models.DTOs;

public class RequestEmployeeDTO
{
    
    [Required]
    public int EmployeeId { get; set; }
    [Required]
    [MaxLength(100)]
    public string Role { get; set; }
    
}