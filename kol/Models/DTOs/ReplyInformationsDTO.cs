namespace kol.Models.DTOs;

public class ReplyInformationsDTO
{
    public int NurseryId { get; set; }
    public string Name { get; set; }
    public DateTime EstablishedDate { get; set; }
    public List<ReplyBatchDTO> Batches { get; set; }
}