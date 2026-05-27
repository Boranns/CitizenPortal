using CitizenPortal.Services;
using Moq;

namespace CitizenPortal.Tests;

public class BlobServiceTests
{
    private readonly Mock<IBlobService> _blobServiceMock;

    public BlobServiceTests()
    {
        _blobServiceMock = new Mock<IBlobService>();
    }

    [Fact]
    public async Task UploadAsync_ReturnsUrl()
    {
        var stream = new MemoryStream();
        var expectedUrl = "https://storage.blob.core.windows.net/documents/test.pdf";

        _blobServiceMock.Setup(x => x.UploadAsync(stream, "test.pdf", "application/pdf"))
            .ReturnsAsync(expectedUrl);

        var result = await _blobServiceMock.Object.UploadAsync(stream, "test.pdf", "application/pdf");

        Assert.Equal(expectedUrl, result);
    }

    [Fact]
    public async Task DeleteAsync_CallsRepository()
    {
        var blobUrl = "https://storage.blob.core.windows.net/documents/test.pdf";

        _blobServiceMock.Setup(x => x.DeleteAsync(blobUrl))
            .Returns(Task.CompletedTask);

        await _blobServiceMock.Object.DeleteAsync(blobUrl);

        _blobServiceMock.Verify(x => x.DeleteAsync(blobUrl), Times.Once);
    }
}
