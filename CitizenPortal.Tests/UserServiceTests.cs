using CitizenPortal.DTOs;
using CitizenPortal.Models;
using CitizenPortal.Repositories;
using CitizenPortal.Services;
using Moq;

namespace CitizenPortal.Tests;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userService = new UserService(_userRepositoryMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllUsers()
    {
        var users = new List<User>
        {
            new() { Id = 1, Name = "User 1", Email = "user1@test.com", Role = "Borger", IsActive = true },
            new() { Id = 2, Name = "User 2", Email = "user2@test.com", Role = "Admin", IsActive = true }
        };

        _userRepositoryMock.Setup(x => x.GetAllAsync())
            .ReturnsAsync(users);

        var result = await _userService.GetAllAsync();

        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ReturnsUser()
    {
        var user = new User { Id = 1, Name = "Test User", Email = "test@test.com", Role = "Borger", IsActive = true };

        _userRepositoryMock.Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(user);

        var result = await _userService.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("Test User", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ReturnsNull()
    {
        _userRepositoryMock.Setup(x => x.GetByIdAsync(99))
            .ReturnsAsync((User?)null);

        var result = await _userService.GetByIdAsync(99);

        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateAsync_WithValidId_UpdatesUser()
    {
        var user = new User { Id = 1, Name = "Old Name", Email = "test@test.com", Role = "Borger", IsActive = true };
        var dto = new UpdateUserDto { Name = "New Name", IsActive = false };

        _userRepositoryMock.Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(user);
        _userRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<User>()))
            .ReturnsAsync((User u) => u);

        var result = await _userService.UpdateAsync(1, dto);

        Assert.Equal("New Name", result.Name);
        Assert.False(result.IsActive);
    }

    [Fact]
    public async Task UpdateAsync_WithInvalidId_ThrowsException()
    {
        _userRepositoryMock.Setup(x => x.GetByIdAsync(99))
            .ReturnsAsync((User?)null);

        var dto = new UpdateUserDto { Name = "Test", IsActive = true };

        await Assert.ThrowsAsync<Exception>(() => _userService.UpdateAsync(99, dto));
    }

    [Fact]
    public async Task DeleteAsync_CallsRepository()
    {
        _userRepositoryMock.Setup(x => x.DeleteAsync(1))
            .Returns(Task.CompletedTask);

        await _userService.DeleteAsync(1);

        _userRepositoryMock.Verify(x => x.DeleteAsync(1), Times.Once);
    }
}