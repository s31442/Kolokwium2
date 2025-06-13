using System.ComponentModel.DataAnnotations;

namespace kol.Models;

public class Employee
{
    [Key]
    public int EmployeeId { get; set; }
    
    [MaxLength(100)]
    public string FirstName { get; set; }
    
    [MaxLength(100)]
    public string LastName { get; set; }
    
    public DateTime HireDate { get; set; }
    
    
    ICollection<Responsible> responsibles { get; set;}
}