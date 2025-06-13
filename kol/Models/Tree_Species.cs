using System.ComponentModel.DataAnnotations;

namespace kol.Models;

public class Tree_Species
{
    [Key]
    public int SpeciesId { get; set; }
    
    [MaxLength(100)]
    public string LatinName { get; set; }
    
    public int GrowthTimeInYears { get; set; }
    
    ICollection<Seedling_Batch> Seedling_Batches { get; set; }
}