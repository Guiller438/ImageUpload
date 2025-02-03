using ImageUploadMS.Interfaces;

namespace ImageUploadMS.Services
{
    public class ImageUploadServices : IImageUpload
    {
        private readonly string _imageFolderProfileImages = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "profilepictures");

        private readonly string _imageFolderEvidenceImages = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "evidenceimages");

        public ImageUploadServices()
        {
            if (!Directory.Exists(_imageFolderProfileImages))
            {
                Directory.CreateDirectory(_imageFolderProfileImages);
                Directory.CreateDirectory(_imageFolderEvidenceImages);
            }
        }

        public async Task<string> UploadImageProfileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("El archivo es inválido.");

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(_imageFolderProfileImages, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }

        public async Task<string> UploadImageEvidenceAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("El archivo es inválido.");

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(_imageFolderEvidenceImages, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }
    }
}
