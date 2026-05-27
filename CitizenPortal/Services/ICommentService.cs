using CitizenPortal.DTOs;

namespace CitizenPortal.Services;

public interface ICommentService
{
    Task<IEnumerable<CommentResponseDto>> GetByApplicationIdAsync(int applicationId);
    Task<CommentResponseDto> CreateAsync(int userId, int applicationId, CreateCommentDto dto);
    Task DeleteAsync(int id);
}