using System.ComponentModel.DataAnnotations;

namespace ImageUploadMS.Models
{
    public class ImageUploadModels
    {
        [Required]
        public IFormFile File { get; set; }
    }
}
