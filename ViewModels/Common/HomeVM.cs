using Uniqlo2.ViewModels.Product;
using Uniqlo2.ViewModels.Slider;

namespace Uniqlo2.ViewModels.Common
{
    public class HomeVM
    {
        public IEnumerable<SliderItemVM> Sliders { get; set; }
        public IEnumerable<ProductItemVM> Products { get; set; }

    }
}
