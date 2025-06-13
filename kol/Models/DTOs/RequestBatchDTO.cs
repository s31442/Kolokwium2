using System.ComponentModel.DataAnnotations;

namespace kol.Models.DTOs;

public class RequestBatchDTO
{
    [Required] 
    public int quantity { get; set; }
    [Required]
    [MaxLength(100)]
    public string Species { get; set; }
    [Required]
    [MaxLength(100)]
    public string Nursery { get; set; }
    [Required]
    public List<RequestEmployeeDTO> Responsible { get; set; }
}