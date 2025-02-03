using ImageUploadMS.Interfaces;

namespace ImageUploadMS.Services
{
    public class ImageUploadServices : IImageUpload
    {
        private readonly string _imageFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "profilepictures");
    

        public ImageUploadServices()
        {
            if (!Directory.Exists(_imageFolder))
            {
                Directory.CreateDirectory(_imageFolder);
            }
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("El archivo es inválido.");

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(_imageFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }
    }
}
