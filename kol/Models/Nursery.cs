using System.ComponentModel.DataAnnotations;

namespace kol.Models;

public class Nursery
{
    [Key]
    public int NurseryId { get; set; }
    
    [MaxLength(100)]
    public string Name { get; set; }
    
    public DateTime EstablishedDate { get; set; }
    
    public ICollection<Seedling_Batch> Seedling_Batches { get; set; }
}