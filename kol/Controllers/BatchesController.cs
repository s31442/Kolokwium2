using kol.Models.DTOs;
using kol.Services;
using Microsoft.AspNetCore.Mvc;

namespace kol.Controllers;



[ApiController]
[Route("api/[controller]")]

public class BatchesController : ControllerBase
{
    private readonly INurseriesService _nurseriesService;

    public BatchesController(INurseriesService nurseriesService)
    {
        _nurseriesService = nurseriesService;
    }
    
    
    [HttpPost]
    public async Task<IActionResult> InsertNewBatchAsync(RequestBatchDTO request, CancellationToken token)
    {
        await _nurseriesService.InsertNewBatchAsync(request, token);
        return Created();
    }
    
    
}