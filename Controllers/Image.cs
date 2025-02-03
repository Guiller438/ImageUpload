using ImageUploadMS.Interfaces;
using ImageUploadMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace ImageUploadMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class Image : Controller
    {
        private readonly IImageUpload _imageUpload;

        private readonly string _imageFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "profilepictures");


        public Image(IImageUpload imageUpload)
        {
            _imageUpload = imageUpload;
        }

        [HttpPost("uploadprofile")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadImageProfile([FromForm] ImageUploadModels image)
        {
            if (image == null || image.File == null || image.File.Length == 0)
            {
                return BadRequest("No se proporcionó una imagen válida.");
            }

            var fileName = await _imageUpload.UploadImageProfileAsync(image.File);

            // 📌 Nueva URL con `wwwroot/profilepictures/`
            var fullUrl = $"{Request.Scheme}://{Request.Host}/profilepictures/{fileName}";

            return Ok(new { url = fullUrl });
        }

        [HttpPost("uploadevidence")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadImageEvidence([FromForm] ImageUploadModels image)
        {
            if (image == null || image.File == null || image.File.Length == 0)
            {
                return BadRequest("No se proporcionó una imagen válida.");
            }

            var fileName = await _imageUpload.UploadImageEvidenceAsync(image.File);

            // 📌 Nueva URL con `wwwroot/profilepictures/`
            var fullUrl = $"{Request.Scheme}://{Request.Host}/evidenceimages/{fileName}";

            return Ok(new { url = fullUrl });
        }

        [HttpGet("all")]
        public IActionResult GetAllImages()
        {
            if (!Directory.Exists(_imageFolder))
            {
                return NotFound("La carpeta de imágenes no existe.");
            }

            var imageFiles = Directory.GetFiles(_imageFolder)
                .Select(file => new
                {
                    FileName = Path.GetFileName(file),
                    Url = $"{Request.Scheme}://{Request.Host}/profilepictures/{Path.GetFileName(file)}"
                })
                .ToList();

            return Ok(imageFiles);
        }
    }
}
