
using CampusPlacementManagement.DTOs.Colleges;
using CampusPlacementManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CampusPlacementManagement.Controllers;

[Route("api/colleges")]
[ApiController]
public class CollegeController : ControllerBase
{
    private readonly ICollegeService _collegeService;

    public CollegeController(ICollegeService collegeService)
    {
        _collegeService = collegeService;
    }

    // 1. Register a new college
    [HttpPost("register")]
    public async Task<IActionResult> RegisterCollege(
        [FromBody] CreateCollegeDto dto)
    {
        try
        {
            var college = await _collegeService.CreateCollegeAsync(dto);

            return CreatedAtAction(
                nameof(GetCollegeById),
                new { collegeId = college.CollegeId },
                college
            );
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    // 2. Get all colleges
    [HttpGet]
    public async Task<IActionResult> GetAllColleges()
    {
        var colleges = await _collegeService.GetAllCollegesAsync();

        return Ok(colleges);
    }

    // 3. Get pending colleges
    [HttpGet("pending")]
    public async Task<IActionResult> GetPendingColleges()
    {
        var colleges =
            await _collegeService.GetCollegesByStatusAsync("Pending");

        return Ok(colleges);
    }

    // 4. Get college by ID
    [HttpGet("{collegeId:int}")]
    public async Task<IActionResult> GetCollegeById(int collegeId)
    {
        if (collegeId <= 0)
            return BadRequest(new { message = "Invalid college ID." });

        var college =
            await _collegeService.GetCollegeByIdAsync(collegeId);

        if (college == null)
            return NotFound(new { message = "College not found." });

        return Ok(college);
    }

    // 5. Approve college
    [HttpPut("{collegeId:int}/approve")]
    public async Task<IActionResult> ApproveCollege(int collegeId)
    {
        if (collegeId <= 0)
            return BadRequest(new { message = "Invalid college ID." });

        var college =
            await _collegeService.ApproveCollegeAsync(collegeId);

        if (college == null)
            return NotFound(new { message = "College not found." });

        return Ok(college);
    }

    // 6. Reject college
    [HttpPut("{collegeId:int}/reject")]
    public async Task<IActionResult> RejectCollege(int collegeId)
    {
        if (collegeId <= 0)
            return BadRequest(new { message = "Invalid college ID." });

        var college =
            await _collegeService.RejectCollegeAsync(collegeId);

        if (college == null)
            return NotFound(new { message = "College not found." });

        return Ok(college);
    }

    // 7. Suspend college
    [HttpPut("{collegeId:int}/suspend")]
    public async Task<IActionResult> SuspendCollege(int collegeId)
    {
        if (collegeId <= 0)
            return BadRequest(new { message = "Invalid college ID." });

        var college =
            await _collegeService.SuspendCollegeAsync(collegeId);

        if (college == null)
            return NotFound(new { message = "College not found." });

        return Ok(college);
    }

    // 8. Update college
    [HttpPut("{collegeId:int}")]
    public async Task<IActionResult> UpdateCollege(
        int collegeId,
        [FromBody] UpdateCollegeDto dto)
    {
        if (collegeId <= 0)
            return BadRequest(new { message = "Invalid college ID." });

        try
        {
            var college =
                await _collegeService.UpdateCollegeAsync(collegeId, dto);

            if (college == null)
                return NotFound(new { message = "College not found." });

            return Ok(college);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }
}
