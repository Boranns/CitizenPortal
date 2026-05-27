using CitizenPortal.DTOs;

namespace CitizenPortal.Services;

public interface IApplicationService
{
    Task<IEnumerable<ApplicationResponseDto>> GetAllAsync();
    Task<IEnumerable<ApplicationResponseDto>> GetByUserIdAsync(int userId);
    Task<ApplicationResponseDto?> GetByIdAsync(int id);
    Task<ApplicationResponseDto> CreateAsync(int userId, CreateApplicationDto dto);
    Task<ApplicationResponseDto> UpdateStatusAsync(int id, UpdateApplicationStatusDto dto);
    Task DeleteAsync(int id);
}