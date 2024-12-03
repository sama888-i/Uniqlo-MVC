using System.ComponentModel.DataAnnotations;

namespace Uniqlo2.ViewModels.Product
{
    public class ProductCreateVM
    {
        [MaxLength (32, ErrorMessage = "Title length must be lees than 32 "), Required(ErrorMessage = "Başlığı qeyd etməmisiniz")]
        public string Name { get; set; } = null!;
        [MaxLength (64),Required ]
        public string Description { get; set; } = null!;
        [Required ]
        public decimal CostPrice { get; set; }
        [Required ]
        public decimal SellPrice { get; set; }
        [Required ]
        public int Quantity { get; set; }

        public int Discount { get; set; }
        public IFormFile  CoverFile { get; set; } 
   
        public IEnumerable<IFormFile> OtherFiles{ get; set; } 
        public int? CategoryId { get; set; }
       
    }
}
