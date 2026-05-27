using CitizenPortal.DTOs;
using CitizenPortal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CitizenPortal.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ApplicationController : ControllerBase
{
    private readonly IApplicationService _applicationService;

    public ApplicationController(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    [HttpGet]
    [Authorize(Roles = "Sagsbehandler,Admin")]
    public async Task<IActionResult> GetAll()
    {
        var applications = await _applicationService.GetAllAsync();
        return Ok(applications);
    }

    [HttpGet("my")]
    [Authorize(Roles = "Borger")]
    public async Task<IActionResult> GetMy()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var applications = await _applicationService.GetByUserIdAsync(userId);
        return Ok(applications);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var application = await _applicationService.GetByIdAsync(id);
        if (application == null) return NotFound();
        return Ok(application);
    }

    [HttpPost]
    [Authorize(Roles = "Borger")]
    public async Task<IActionResult> Create(CreateApplicationDto dto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _applicationService.CreateAsync(userId, dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}/status")]
    [Authorize(Roles = "Sagsbehandler,Admin")]
    public async Task<IActionResult> UpdateStatus(int id, UpdateApplicationStatusDto dto)
    {
        try
        {
            var result = await _applicationService.UpdateStatusAsync(id, dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _applicationService.DeleteAsync(id);
        return NoContent();
    }
}
