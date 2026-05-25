using CitizenPortal.Models;

namespace CitizenPortal.Repositories;

public interface IApplicationRepository
{
    Task<Application?> GetByIdAsync(int id);
    Task<IEnumerable<Application>> GetAllAsync();
    Task<IEnumerable<Application>> GetByUserIdAsync(int userId);
    Task<IEnumerable<Application>> GetByStatusAsync(string status);
    Task<Application> CreateAsync(Application application);
    Task<Application> UpdateAsync(Application application);
    Task DeleteAsync(int id);
}
