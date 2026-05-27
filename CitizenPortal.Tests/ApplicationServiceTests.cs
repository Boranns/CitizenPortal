using CitizenPortal.DTOs;
using CitizenPortal.Models;
using CitizenPortal.Repositories;
using CitizenPortal.Services;
using Moq;

namespace CitizenPortal.Tests;

public class ApplicationServiceTests
{
    private readonly Mock<IApplicationRepository> _applicationRepositoryMock;
    private readonly ApplicationService _applicationService;

    public ApplicationServiceTests()
    {
        _applicationRepositoryMock = new Mock<IApplicationRepository>();
        _applicationService = new ApplicationService(_applicationRepositoryMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllApplications()
    {
        var applications = new List<Application>
        {
            new() { Id = 1, Title = "Test 1", Status = "Afventer", User = new User { Name = "User 1" } },
            new() { Id = 2, Title = "Test 2", Status = "Afventer", User = new User { Name = "User 2" } }
        };

        _applicationRepositoryMock.Setup(x => x.GetAllAsync())
            .ReturnsAsync(applications);

        var result = await _applicationService.GetAllAsync();

        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ReturnsApplication()
    {
        var application = new Application
        {
            Id = 1,
            Title = "Test",
            Status = "Afventer",
            User = new User { Name = "Test User" }
        };

        _applicationRepositoryMock.Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(application);

        var result = await _applicationService.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("Test", result.Title);
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ReturnsNull()
    {
        _applicationRepositoryMock.Setup(x => x.GetByIdAsync(99))
            .ReturnsAsync((Application?)null);

        var result = await _applicationService.GetByIdAsync(99);

        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_WithValidData_ReturnsApplication()
    {
        var dto = new CreateApplicationDto
        {
            Title = "New Application",
            Description = "Test description"
        };

        _applicationRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<Application>()))
            .ReturnsAsync((Application a) => a);

        var result = await _applicationService.CreateAsync(1, dto);

        Assert.NotNull(result);
        Assert.Equal(dto.Title, result.Title);
        Assert.Equal("Afventer", result.Status);
    }

    [Fact]
    public async Task UpdateStatusAsync_WithValidId_UpdatesStatus()
    {
        var application = new Application
        {
            Id = 1,
            Title = "Test",
            Status = "Afventer",
            User = new User { Name = "Test User" }
        };

        var dto = new UpdateApplicationStatusDto { Status = "Godkendt" };

        _applicationRepositoryMock.Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(application);
        _applicationRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Application>()))
            .ReturnsAsync((Application a) => a);

        var result = await _applicationService.UpdateStatusAsync(1, dto);

        Assert.Equal("Godkendt", result.Status);
    }

    [Fact]
    public async Task UpdateStatusAsync_WithInvalidId_ThrowsException()
    {
        _applicationRepositoryMock.Setup(x => x.GetByIdAsync(99))
            .ReturnsAsync((Application?)null);

        var dto = new UpdateApplicationStatusDto { Status = "Godkendt" };

        await Assert.ThrowsAsync<Exception>(() =>
            _applicationService.UpdateStatusAsync(99, dto));
    }

    [Fact]
    public async Task GetByUserIdAsync_ReturnsUserApplications()
    {
        var applications = new List<Application>
        {
            new() { Id = 1, UserId = 1, Title = "Test", Status = "Afventer", User = new User { Name = "User" } }
        };

        _applicationRepositoryMock.Setup(x => x.GetByUserIdAsync(1))
            .ReturnsAsync(applications);

        var result = await _applicationService.GetByUserIdAsync(1);

        Assert.Single(result);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsEmptyList_WhenNoApplications()
    {
        _applicationRepositoryMock.Setup(x => x.GetAllAsync())
            .ReturnsAsync(new List<Application>());

        var result = await _applicationService.GetAllAsync();

        Assert.Empty(result);
    }

    [Fact]
    public async Task CreateAsync_SetsStatusToAfventer()
    {
        var dto = new CreateApplicationDto
        {
            Title = "Test",
            Description = "Test description"
        };

        _applicationRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<Application>()))
            .ReturnsAsync((Application a) => a);

        var result = await _applicationService.CreateAsync(1, dto);

        Assert.Equal("Afventer", result.Status);
    }
}