using Uniqlo2.Models;
using Uniqlo2.ViewModels.Common;

namespace Uniqlo2.ViewModels.ProductDetails
{
    public class ProductDetailsVM
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal SellPrice { get; set; }
        public int Quantity { get; set; }
        public int Discount { get; set; }
        public string CoverImage { get; set; } = null!;
        
        public ICollection<ProductImage>? Images { get; set; }


    }
}
