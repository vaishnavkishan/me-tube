using Microsoft.AspNetCore.Mvc;

namespace MeTube.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ThumbnailsController: ControllerBase
{
    private readonly string _videosPath = Path.Combine(Directory.GetCurrentDirectory(), "Videos");
    private readonly string _thumbnailsPath = Path.Combine(Directory.GetCurrentDirectory(), "Thumbnails");

    [HttpGet("")]
    public IActionResult GetThumbnailList()
    {
        var thumbnailUrls = new List<string>();

        foreach (var videoFile in Directory.GetFiles(_videosPath, "*.mp4"))
        {
            var videoTitle = Path.GetFileNameWithoutExtension(videoFile);
            var thumbnailFileName = $"{videoTitle}.jpg";
            var thumbnailPath = Path.Combine(_thumbnailsPath, thumbnailFileName);

            if (System.IO.File.Exists(thumbnailPath))
            {
                var thumbnailUrl = GenerateThumbnailUrl(videoTitle);
                thumbnailUrls.Add(thumbnailUrl);
            }
        }

        return Ok(thumbnailUrls);
    }
    
    [HttpGet("{fileName}")]
    public IActionResult StreamThumbnail(string fileName)
    {
        var thumbnailPath = Path.Combine(_thumbnailsPath, $"{fileName}.jpg");

        if (System.IO.File.Exists(thumbnailPath))
        {
            var contentType = "image/jpeg"; // Set the appropriate content type

            var stream = new FileStream(thumbnailPath, FileMode.Open, FileAccess.Read);

            try
            {
                return new FileStreamResult(stream, "image/jpeg");
            }
            catch
            {
                stream.Dispose(); // Ensure the stream is properly disposed in case of exception
                throw;
            }
        }

        return NotFound();
    }


    private string GenerateThumbnailUrl(string thumbnailFileName)
    {
        // Assuming your API is hosted locally on port 5001
        // Example: https://localhost:5001/api/videos/thumbnails/your-thumbnail-filename
        return Url.Action("StreamThumbnail", "Thumbnails", new { fileName = thumbnailFileName }, Request.Scheme);
    }
}