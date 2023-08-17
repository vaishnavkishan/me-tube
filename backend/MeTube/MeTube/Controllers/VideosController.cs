using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace MeTube.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VideosController : ControllerBase
{
    private readonly string _videosPath = Path.Combine(Directory.GetCurrentDirectory(), "Videos");

    [HttpGet]
    public IActionResult StreamRandomVideo()
    {
        var videoFiles = Directory.GetFiles(_videosPath, "*.mp4");
        if (videoFiles.Length == 0)
        {
            return NotFound("No videos found.");
        }

        var random = new Random();
        var randomVideoPath = videoFiles[random.Next(videoFiles.Length)];

        var videoStream = new FileStream(randomVideoPath, FileMode.Open, FileAccess.Read);

        try
        {
            return new FileStreamResult(videoStream, "video/mp4");
        }
        catch
        {
            videoStream.Dispose(); // Ensure the stream is properly disposed in case of exception
            throw;
        }
    }
}