using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Uniqlo2.DataAccsess;
using Uniqlo2.ViewModels.Common;
using Uniqlo2.ViewModels.Product;
using Uniqlo2.ViewModels.Slider;

namespace Uniqlo2.Controllers
{
    public class HomeController (UniqloDbContext _context) : Controller
    {
        public async Task< IActionResult> Index()
        {
            HomeVM vm = new();
            vm.Sliders  = await _context.Sliders
                .Where (x=>!x.IsDeleted)
                .Select(x => new SliderItemVM
            {
                ImageUrl = x.ImageUrl,
                Link = x.Link,
                Title = x.Title,
                Subtitle = x.Subtitle
            }).ToListAsync();
            vm.Products = await _context.Products
                .Where(x => !x.IsDeleted)
                .Select(x => new ProductItemVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImageUrl = x.CoverImage,
                    IsInStock = x.Quantity > 0,
                    Discount = x.Discount,
                    Price = x.SellPrice

                }).ToListAsync(); 
            return View(vm);
        }
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}
