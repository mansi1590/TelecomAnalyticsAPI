using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TelecomAnalyticsAPI.Data;

namespace TelecomAnalyticsAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AnalyticsController : ControllerBase
{
    private readonly AppDbContext _context;

    public AnalyticsController(AppDbContext context)
    {
        _context = context;
    }

    // Total Calls
    [HttpGet("total-calls")]
    public async Task<IActionResult> TotalCalls()
    {
        var totalCalls = await _context.CdrRecords.CountAsync();

        return Ok(new
        {
            TotalCalls = totalCalls
        });
    }

    // Total Duration
    [HttpGet("total-duration")]
    public async Task<IActionResult> TotalDuration()
    {
        var totalDuration = await _context.CdrRecords
            .SumAsync(x => x.CallDuration);

        return Ok(new
        {
            TotalDuration = totalDuration
        });
    }

    // Top Callers
    [HttpGet("top-callers")]
    public async Task<IActionResult> TopCallers()
    {
        var topCallers = await _context.CdrRecords
            .GroupBy(x => x.CallerNumber)
            .Select(g => new
            {
                CallerNumber = g.Key,
                TotalCalls = g.Count()
            })
            .OrderByDescending(x => x.TotalCalls)
            .Take(5)
            .ToListAsync();

        return Ok(topCallers);
    }

    // Call Direction Distribution
    [HttpGet("call-distribution")]
    public async Task<IActionResult> CallDistribution()
    {
        var distribution = await _context.CdrRecords
            .GroupBy(x => x.calldirection)
            .Select(g => new
            {
                CallDirection = g.Key,
                Count = g.Count()
            })
            .ToListAsync();

        return Ok(distribution);
    }
}