using Microsoft.AspNetCore.Mvc;
using WebUploadServer.Helpers;
using WebUploadServer.Models;

namespace WebUploadServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContentController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<ContentController> _logger;

        public ContentController(ILogger<ContentController> logger)
        {
            _logger = logger;
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> Upload([FromBody] UploadDTO model)
        {
            var image = await ImageWorker.SaveImageAsync(model.Photo);

            return Ok(new UploadResponseDTO
            {
                Image = "/images/"+image,
            });
        }
    }
}
