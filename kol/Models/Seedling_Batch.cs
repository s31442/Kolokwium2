using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kol.Models;

public class Seedling_Batch
{
    [Key]
    public int BatchId { get; set; }
    
    public int NurseryId { get; set; }
    
    public int SpeciesId {get; set;}
    
    public int Quantity { get; set; }
    
    public DateTime SownDate { get; set; }
    
    public DateTime? ReadyDate { get; set; }
    
    [ForeignKey("NurseryId")]
    public Nursery Nursery { get; set; }
    
    [ForeignKey("SpeciesId")]
    public Tree_Species TreeSpecies { get; set; }
    
    public ICollection<Responsible> Responsibles { get; set; }
    
}