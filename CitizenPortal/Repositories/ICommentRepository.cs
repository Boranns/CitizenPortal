using CitizenPortal.Models;

namespace CitizenPortal.Repositories;

public interface ICommentRepository
{
    Task<Comment?> GetByIdAsync(int id);
    Task<IEnumerable<Comment>> GetByApplicationIdAsync(int applicationId);
    Task<Comment> CreateAsync(Comment comment);
    Task DeleteAsync(int id);
}
