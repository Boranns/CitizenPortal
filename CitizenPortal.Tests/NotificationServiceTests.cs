using CitizenPortal.Models;
using CitizenPortal.Repositories;
using CitizenPortal.Services;
using Moq;

namespace CitizenPortal.Tests;

public class NotificationServiceTests
{
    private readonly Mock<INotificationRepository> _notificationRepositoryMock;
    private readonly NotificationService _notificationService;

    public NotificationServiceTests()
    {
        _notificationRepositoryMock = new Mock<INotificationRepository>();
        _notificationService = new NotificationService(_notificationRepositoryMock.Object);
    }

    [Fact]
    public async Task GetByUserIdAsync_ReturnsNotifications()
    {
        var notifications = new List<Notification>
        {
            new() { Id = 1, Message = "Test 1", IsRead = false, CreatedAt = DateTime.UtcNow },
            new() { Id = 2, Message = "Test 2", IsRead = true, CreatedAt = DateTime.UtcNow }
        };

        _notificationRepositoryMock.Setup(x => x.GetByUserIdAsync(1))
            .ReturnsAsync(notifications);

        var result = await _notificationService.GetByUserIdAsync(1);

        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task MarkAsReadAsync_CallsRepository()
    {
        _notificationRepositoryMock.Setup(x => x.MarkAsReadAsync(1))
            .Returns(Task.CompletedTask);

        await _notificationService.MarkAsReadAsync(1);

        _notificationRepositoryMock.Verify(x => x.MarkAsReadAsync(1), Times.Once);
    }

    [Fact]
    public async Task MarkAllAsReadAsync_CallsRepository()
    {
        _notificationRepositoryMock.Setup(x => x.MarkAllAsReadAsync(1))
            .Returns(Task.CompletedTask);

        await _notificationService.MarkAllAsReadAsync(1);

        _notificationRepositoryMock.Verify(x => x.MarkAllAsReadAsync(1), Times.Once);
    }
}