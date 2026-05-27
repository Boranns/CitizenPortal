using CitizenPortal.DTOs;
using CitizenPortal.Models;
using CitizenPortal.Repositories;

namespace CitizenPortal.Services;

public class ApplicationService : IApplicationService
{
    private readonly IApplicationRepository _applicationRepository;

    public ApplicationService(IApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }

    public async Task<IEnumerable<ApplicationResponseDto>> GetAllAsync()
    {
        var applications = await _applicationRepository.GetAllAsync();
        return applications.Select(MapToDto);
    }

    public async Task<IEnumerable<ApplicationResponseDto>> GetByUserIdAsync(int userId)
    {
        var applications = await _applicationRepository.GetByUserIdAsync(userId);
        return applications.Select(MapToDto);
    }

    public async Task<ApplicationResponseDto?> GetByIdAsync(int id)
    {
        var application = await _applicationRepository.GetByIdAsync(id);
        return application == null ? null : MapToDto(application);
    }

    public async Task<ApplicationResponseDto> CreateAsync(int userId, CreateApplicationDto dto)
    {
        var application = new Application
        {
            UserId = userId,
            Title = dto.Title,
            Description = dto.Description,
            Status = "Afventer",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _applicationRepository.CreateAsync(application);
        return MapToDto(application);
    }

    public async Task<ApplicationResponseDto> UpdateStatusAsync(int id, UpdateApplicationStatusDto dto)
    {
        var application = await _applicationRepository.GetByIdAsync(id)
            ?? throw new Exception("Application not found");

        application.Status = dto.Status;
        await _applicationRepository.UpdateAsync(application);
        return MapToDto(application);
    }

    public async Task DeleteAsync(int id)
    {
        await _applicationRepository.DeleteAsync(id);
    }

    private static ApplicationResponseDto MapToDto(Application application)
    {
        return new ApplicationResponseDto
        {
            Id = application.Id,
            Title = application.Title,
            Description = application.Description,
            Status = application.Status,
            UserName = application.User?.Name ?? string.Empty,
            CreatedAt = application.CreatedAt,
            UpdatedAt = application.UpdatedAt
        };
    }
}
