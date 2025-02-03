namespace ImageUploadMS.Interfaces
{
    public interface IImageUpload
    {
        Task<string> UploadImageAsync(IFormFile file);
    }
}
