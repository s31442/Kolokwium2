using kol.Models.DTOs;

namespace kol.Services;

public interface INurseriesService
{
    Task<ReplyInformationsDTO> GetBatchesForIdAsync(int id, CancellationToken token);
    Task InsertNewBatchAsync(RequestBatchDTO request, CancellationToken token);
}