using CitizenPortal.DTOs;

namespace CitizenPortal.Services;

public interface IUserService
{
    Task<IEnumerable<UserResponseDto>> GetAllAsync();
    Task<UserResponseDto?> GetByIdAsync(int id);
    Task<UserResponseDto> UpdateAsync(int id, UpdateUserDto dto);
    Task DeleteAsync(int id);
}
