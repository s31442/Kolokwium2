using kol.Services;
using Microsoft.AspNetCore.Mvc;



namespace kol.Controllers;

[ApiController]
[Route("api/[controller]")]

public class NurseriesController : ControllerBase
{
    
    private readonly INurseriesService _nurseriesService;

    
    public NurseriesController(INurseriesService nurseriesService)
    {
        _nurseriesService = nurseriesService;
    }

    
    [HttpGet("{id}/batches")]
    public async Task<IActionResult> GetBatchesForIdAsync(int id, CancellationToken token)
    {
        var response = await _nurseriesService.GetBatchesForIdAsync(id, token);
        return Ok(response);
    }
    
}