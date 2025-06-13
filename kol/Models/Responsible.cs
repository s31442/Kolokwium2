using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kol.Models;

public class Responsible
{
    public int BatchId { get; set; }
    
    public int EmployeeId { get; set; }
    
    [MaxLength(100)]
    public string Role { get; set; }
    
    
    [ForeignKey("BatchId")]
    public Seedling_Batch Seedling_Batch { get; set; }
    
    [ForeignKey("EmployeeId")]
    public Employee Employee { get; set; }
}