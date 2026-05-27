using CitizenPortal.DTOs;

namespace CitizenPortal.Services;

public interface INotificationService
{
    Task<IEnumerable<NotificationResponseDto>> GetByUserIdAsync(int userId);
    Task MarkAsReadAsync(int id);
    Task MarkAllAsReadAsync(int userId);
}
