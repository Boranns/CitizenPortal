using CitizenPortal.DTOs;
using CitizenPortal.Models;
using CitizenPortal.Repositories;

namespace CitizenPortal.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;

    public CommentService(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<IEnumerable<CommentResponseDto>> GetByApplicationIdAsync(int applicationId)
    {
        var comments = await _commentRepository.GetByApplicationIdAsync(applicationId);
        return comments.Select(c => new CommentResponseDto
        {
            Id = c.Id,
            Text = c.Text,
            UserName = c.User?.Name ?? string.Empty,
            CreatedAt = c.CreatedAt
        });
    }

    public async Task<CommentResponseDto> CreateAsync(int userId, int applicationId, CreateCommentDto dto)
    {
        var comment = new Comment
        {
            UserId = userId,
            ApplicationId = applicationId,
            Text = dto.Text,
            CreatedAt = DateTime.UtcNow
        };

        await _commentRepository.CreateAsync(comment);

        return new CommentResponseDto
        {
            Id = comment.Id,
            Text = comment.Text,
            UserName = string.Empty,
            CreatedAt = comment.CreatedAt
        };
    }

    public async Task DeleteAsync(int id)
    {
        await _commentRepository.DeleteAsync(id);
    }
}
