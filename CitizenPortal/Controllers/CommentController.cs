using CitizenPortal.DTOs;
using CitizenPortal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CitizenPortal.Controllers;

[ApiController]
[Route("api/applications/{applicationId}/comments")]
[Authorize]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetByApplicationId(int applicationId)
    {
        var comments = await _commentService.GetByApplicationIdAsync(applicationId);
        return Ok(comments);
    }

    [HttpPost]
    public async Task<IActionResult> Create(int applicationId, CreateCommentDto dto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _commentService.CreateAsync(userId, applicationId, dto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _commentService.DeleteAsync(id);
        return NoContent();
    }
}