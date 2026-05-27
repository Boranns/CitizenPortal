using CitizenPortal.DTOs;
using CitizenPortal.Models;
using CitizenPortal.Repositories;
using CitizenPortal.Services;
using Microsoft.Extensions.Configuration;
using Moq;

namespace CitizenPortal.Tests;

public class AuthServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _configurationMock = new Mock<IConfiguration>();

        _configurationMock.Setup(x => x["Jwt:Key"])
            .Returns("CitizenPortalSuperSecretKey123!@@#SuperLong");
        _configurationMock.Setup(x => x["Jwt:Issuer"])
            .Returns("CitizenPortal");
        _configurationMock.Setup(x => x["Jwt:Audience"])
            .Returns("CitizenPortalUsers");
        _configurationMock.Setup(x => x["Jwt:ExpireMinutes"])
            .Returns("60");

        _authService = new AuthService(_userRepositoryMock.Object, _configurationMock.Object);
    }

    [Fact]
    public async Task Register_WithValidData_ReturnsAuthResponse()
    {
        var dto = new RegisterDto
        {
            Name = "Test User",
            Email = "test@test.com",
            Password = "Test123!",
            Role = "Borger"
        };

        _userRepositoryMock.Setup(x => x.EmailExistsAsync(dto.Email))
            .ReturnsAsync(false);
        _userRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<User>()))
            .ReturnsAsync((User u) => u);

        var result = await _authService.RegisterAsync(dto);

        Assert.NotNull(result);
        Assert.Equal(dto.Email, result.Email);
        Assert.Equal(dto.Name, result.Name);
        Assert.NotEmpty(result.Token);
    }

    [Fact]
    public async Task Register_WithExistingEmail_ThrowsException()
    {
        var dto = new RegisterDto { Email = "existing@test.com", Password = "Test123!" };

        _userRepositoryMock.Setup(x => x.EmailExistsAsync(dto.Email))
            .ReturnsAsync(true);

        await Assert.ThrowsAsync<Exception>(() => _authService.RegisterAsync(dto));
    }

    [Fact]
    public async Task Login_WithValidCredentials_ReturnsAuthResponse()
    {
        var dto = new LoginDto { Email = "test@test.com", Password = "Test123!" };
        var user = new User
        {
            Id = 1,
            Name = "Test User",
            Email = "test@test.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Test123!"),
            Role = "Borger"
        };

        _userRepositoryMock.Setup(x => x.GetByEmailAsync(dto.Email))
            .ReturnsAsync(user);

        var result = await _authService.LoginAsync(dto);

        Assert.NotNull(result);
        Assert.Equal(dto.Email, result.Email);
        Assert.NotEmpty(result.Token);
    }

    [Fact]
    public async Task Login_WithWrongPassword_ThrowsException()
    {
        var dto = new LoginDto { Email = "test@test.com", Password = "WrongPassword!" };
        var user = new User
        {
            Email = "test@test.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Test123!")
        };

        _userRepositoryMock.Setup(x => x.GetByEmailAsync(dto.Email))
            .ReturnsAsync(user);

        await Assert.ThrowsAsync<Exception>(() => _authService.LoginAsync(dto));
    }

    [Fact]
    public async Task Login_WithNonExistentEmail_ThrowsException()
    {
        var dto = new LoginDto { Email = "notexist@test.com", Password = "Test123!" };

        _userRepositoryMock.Setup(x => x.GetByEmailAsync(dto.Email))
            .ReturnsAsync((User?)null);
        await Assert.ThrowsAsync<Exception>(() => _authService.LoginAsync(dto));
    }
}
