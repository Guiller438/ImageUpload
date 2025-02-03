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

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadImage([FromForm] ImageUploadModels image)
        {
            if (image == null || image.File == null || image.File.Length == 0)
            {
                return BadRequest("No se proporcionó una imagen válida.");
            }

            var fileName = await _imageUpload.UploadImageAsync(image.File);

            // 📌 Nueva URL con `wwwroot/profilepictures/`
            var fullUrl = $"{Request.Scheme}://{Request.Host}/profilepictures/{fileName}";

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
