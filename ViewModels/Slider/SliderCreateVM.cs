using System.ComponentModel.DataAnnotations;

namespace Uniqlo2.ViewModels.Slider
{
    public class SliderCreateVM
    {
        [MaxLength(32, ErrorMessage = "Title length must be lees than 32 "), Required(ErrorMessage = "Başlığı qeyd etməmisiniz")]
        public string Title { get; set; }
        [MaxLength(64), Required]
        public string Subtitle { get; set; }
        public string? Link { get; set; }
        [Required]
        public IFormFile File { get; set; }

    }
}
