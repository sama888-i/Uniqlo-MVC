using System.ComponentModel.DataAnnotations;

namespace Uniqlo2.ViewModels.Product
{
    public class ProductUpdateVM
    {

        [MaxLength (32),Required]
        public string Name { get; set; } = null!;
        [MaxLength(64), Required]
        public string Description { get; set; } = null!;
        [Required]
        public decimal CostPrice { get; set; }
        [Required]
        public decimal SellPrice { get; set; }
        [Required]
        public int Quantity { get; set; }

        public int Discount { get; set; }
        public IFormFile CoverFile { get; set; }
        public IEnumerable<string> OtherFileUrls { get; set; }
        public IEnumerable<IFormFile>? OtherFiles { get; set; }
        public int? CategoryId { get; set; }
        public string CoverFileUrl { get; set; } = null!;


    }
}
