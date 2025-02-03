namespace ImageUploadMS.Interfaces
{
    public interface IImageUpload
    {
        Task<string> UploadImageProfileAsync(IFormFile file);

        Task<string> UploadImageEvidenceAsync(IFormFile file);

    }
}
