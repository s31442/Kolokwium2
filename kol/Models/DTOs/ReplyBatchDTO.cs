namespace kol.Models.DTOs;

public class ReplyBatchDTO
{
    public int BatchId { get; set; }
    public int Quantity { get; set; }
    public DateTime SownDate { get; set; }
    public DateTime? ReadyDate { get; set; }
    public ReplySpeciesDTO ReplySpecies { get; set; }
    public List<ReplyResponsibleDTO> Responsible { get; set; }
}