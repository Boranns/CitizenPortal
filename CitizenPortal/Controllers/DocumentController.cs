using CitizenPortal.Models;
using CitizenPortal.Repositories;
using CitizenPortal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CitizenPortal.Controllers;

[ApiController]
[Route("api/applications/{applicationId}/documents")]
[Authorize]
public class DocumentController : ControllerBase
{
    private readonly IBlobService _blobService;
    private readonly IApplicationRepository _applicationRepository;

    public DocumentController(IBlobService blobService, IApplicationRepository applicationRepository)
    {
        _blobService = blobService;
        _applicationRepository = applicationRepository;
    }

    [HttpPost]
    [Authorize(Roles = "Borger")]
    public async Task<IActionResult> Upload(int applicationId, IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest(new { message = "Ingen fil valgt" });

        var application = await _applicationRepository.GetByIdAsync(applicationId);
        if (application == null)
            return NotFound(new { message = "Ansøgning ikke fundet" });

        using var stream = file.OpenReadStream();
        var blobUrl = await _blobService.UploadAsync(stream, file.FileName, file.ContentType);

        var document = new Document
        {
            ApplicationId = applicationId,
            FileName = file.FileName,
            BlobUrl = blobUrl,
            UploadedAt = DateTime.UtcNow
        };

        application.Documents.Add(document);
        await _applicationRepository.UpdateAsync(application);

        return Ok(new { fileName = document.FileName, blobUrl = document.BlobUrl });
    }
}
