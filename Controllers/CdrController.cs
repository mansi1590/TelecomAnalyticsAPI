using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TelecomAnalyticsAPI.Data;

namespace TelecomAnalyticsAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CdrController : ControllerBase
{
    private readonly AppDbContext _context;

    public CdrController(AppDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll(
        int page = 1,
        int pageSize = 10,
        string? city = null,
        string? callerNumber = null)
    {
        var query = _context.CdrRecords.AsQueryable();

        // Filter by city
        if (!string.IsNullOrEmpty(city))
        {
            query = query.Where(x => x.City.Contains(city));
        }

        // Filter by caller number
        if (!string.IsNullOrEmpty(callerNumber))
        {
            query = query.Where(x => x.CallerNumber.Contains(callerNumber));
        }

        // Total count before pagination
        var totalRecords = await query.CountAsync();

        // Pagination
        var data = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return Ok(new
        {
            TotalRecords = totalRecords,
            Page = page,
            PageSize = pageSize,
            Data = data
        });
    }
}
