using CitizenPortal.DTOs;
using CitizenPortal.Models;
using CitizenPortal.Repositories;
using CitizenPortal.Services;
using Moq;

namespace CitizenPortal.Tests;

public class CommentServiceTests
{
    private readonly Mock<ICommentRepository> _commentRepositoryMock;
    private readonly CommentService _commentService;

    public CommentServiceTests()
    {
        _commentRepositoryMock = new Mock<ICommentRepository>();
        _commentService = new CommentService(_commentRepositoryMock.Object);
    }

    [Fact]
    public async Task GetByApplicationIdAsync_ReturnsComments()
    {
        var comments = new List<Comment>
        {
            new() { Id = 1, Text = "Test comment", User = new User { Name = "User 1" }, CreatedAt = DateTime.UtcNow },
            new() { Id = 2, Text = "Test comment 2", User = new User { Name = "User 2" }, CreatedAt = DateTime.UtcNow }
        };

        _commentRepositoryMock.Setup(x => x.GetByApplicationIdAsync(1))
            .ReturnsAsync(comments);

        var result = await _commentService.GetByApplicationIdAsync(1);

        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task CreateAsync_WithValidData_ReturnsComment()
    {
        var dto = new CreateCommentDto { Text = "New comment" };

        _commentRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<Comment>()))
            .ReturnsAsync((Comment c) => c);

        var result = await _commentService.CreateAsync(1, 1, dto);

        Assert.NotNull(result);
        Assert.Equal(dto.Text, result.Text);
    }

    [Fact]
    public async Task DeleteAsync_CallsRepository()
    {
        _commentRepositoryMock.Setup(x => x.DeleteAsync(1))
            .Returns(Task.CompletedTask);

        await _commentService.DeleteAsync(1);

        _commentRepositoryMock.Verify(x => x.DeleteAsync(1), Times.Once);
    }
}
