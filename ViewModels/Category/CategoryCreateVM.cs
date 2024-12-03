using System.ComponentModel.DataAnnotations;

namespace Uniqlo2.ViewModels.Category
{
    public class CategoryCreateVM
    {
        [MaxLength(32, ErrorMessage = "Name length must be lees than 32 "), Required(ErrorMessage = "Mehsulun adini yazmamisiniz,Yazin!")]
        public string Name { get; set; }
    }
}
