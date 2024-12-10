using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Uniqlo2.DataAccsess;
using Uniqlo2.Extensions;
using Uniqlo2.Helpers;
using Uniqlo2.Models;
using Uniqlo2.ViewModels.Common;
using Uniqlo2.ViewModels.Product;

namespace Uniqlo2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =RoleConstants.Product )]
    public class ProductController(UniqloDbContext _context, IWebHostEnvironment _env) : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.Include(x => x.Category).ToListAsync());
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories.Where(x => !x.IsDeleted).ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVM vm)
        {
            vm.OtherFiles.Select(x => x.Length / 1024).Sum();
            if (vm.OtherFiles != null && vm.OtherFiles.Any())
            {
                if (!vm.OtherFiles.All(x => x.IsValidType("image")))
                {
                    var fileNames = vm.OtherFiles.Where(x => !x.IsValidType("image")).Select(x => x.FileName);
                    ModelState.AddModelError("OtherFiles", string.Join(",", fileNames) + "are is not an image");
                }
                if (!vm.OtherFiles.All(x => x.IsValidSize(800)))
                {
                    var fileNames = vm.OtherFiles.Where(x => !x.IsValidSize(800))
                        .Select(x => x.FileName);
                    ModelState.AddModelError("OtherFiles", string.Join(",", fileNames) + "must be less than 800kb");

                }

            }
            if (vm.CoverFile != null)
            {
                if (!vm.CoverFile.IsValidType("image"))
                    ModelState.AddModelError("CoverFile", "File type must be an image");
                if (!vm.CoverFile.IsValidSize(800))
                    ModelState.AddModelError("CoverFile", "File length must be less than 800kb");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _context.Categories.Where(x => !x.IsDeleted).ToListAsync();
                return View();
            }
            Product product = new Product
            {
                CategoryId = vm.CategoryId,
                CostPrice = vm.CostPrice,
                Description = vm.Description,
                Discount = vm.Discount,
                Name = vm.Name,
                Quantity = vm.Quantity,
                SellPrice = vm.SellPrice,
                CoverImage = await vm.CoverFile!.UploadAsync(_env.WebRootPath, "imgs", "products")

            };
            List<ProductImage> list = [];
            foreach (var item in vm.OtherFiles)
            {
                string fileName = await item.UploadAsync(_env.WebRootPath, "imgs", "products");
                list.Add(new ProductImage
                {
                    FileUrl = fileName,
                    product = product
                });
            }
            await _context.ProductImages.AddRangeAsync(list);
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var product = await _context.Products
                .Where(x => x.Id == id.Value)
                .Select(x => new ProductUpdateVM
                {
                    Name = x.Name,
                    Description = x.Description,
                    Discount = x.Discount,
                    SellPrice = x.SellPrice,
                    CostPrice = x.CostPrice,
                    CategoryId = x.CategoryId,
                    Quantity = x.Quantity,
                    CoverFileUrl = x.CoverImage,
                    OtherFileUrls = x.Images.Select(y => new ImageUrlAndId
                    {
                        Url = y.FileUrl,
                        Id = y.Id

                    })
                }).FirstOrDefaultAsync();
            ViewBag.Categories = await _context.Categories.Where(x => !x.IsDeleted).ToListAsync();

            return View(product);


        }
        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateVM vm, int? id)
        {
            vm.OtherFiles.Select(x => x.Length / 1024).Sum();
            if (!id.HasValue) return BadRequest();
            var data = await _context.Products.FindAsync(id.Value);
            if (data is null) return NotFound();
            if (vm.CoverFile != null)
            {
                if (!vm.CoverFile.IsValidType("image"))
                    ModelState.AddModelError("CoverFile", "File type must be an image");
                if (!vm.CoverFile.IsValidSize(800))
                    ModelState.AddModelError("CoverFile", "File length must be less than 800kb");
                data.CoverImage = await vm.CoverFile!.UploadAsync(_env.WebRootPath, "imgs", "products");
            }
            if (vm.OtherFiles != null && vm.OtherFiles.Any())
            {
                if (!vm.OtherFiles.All(x => x.IsValidType("image")))
                {
                    var fileNames = vm.OtherFiles.Where(x => !x.IsValidType("image")).Select(x => x.FileName);
                    ModelState.AddModelError("OtherFiles", string.Join(",", fileNames) + "are is not an image");
                }
                if (!vm.OtherFiles.All(x => x.IsValidSize(800)))
                {
                    var fileNames = vm.OtherFiles.Where(x => !x.IsValidSize(800))
                        .Select(x => x.FileName);
                    ModelState.AddModelError("OtherFiles", string.Join(",", fileNames) + "must be less than 800kb");

                }

            }
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _context.Categories.Where(x => !x.IsDeleted).ToListAsync();
                return View(vm);
            }
            List<ProductImage> list = [];
            foreach (var item in vm.OtherFiles)
            {
                string fileName = await item.UploadAsync(_env.WebRootPath, "imgs", "products");
                list.Add(new ProductImage
                {
                    FileUrl = fileName,
                    product = data
                    
                });
            }  
            data.Name = vm.Name;
            data.Description = vm.Description;
            data.Discount = vm.Discount;
            data.CostPrice = vm.CostPrice;
            data.SellPrice = vm.SellPrice;
            data.CategoryId = vm.CategoryId;
            data.Quantity = vm.Quantity;
            


            await _context.ProductImages.AddRangeAsync(list);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> DeleteImage(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var img = await _context.ProductImages.FindAsync(id.Value);
            if (img == null) return NotFound();
            _context.ProductImages.Remove(img);
            string path = Path.Combine(_env.WebRootPath, "imgs", "products", img.FileUrl);
            await _context.SaveChangesAsync();
            if (Path.Exists(path))
                System.IO.File.Delete(path);
            return Ok();

        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue) return BadRequest();
            if (await _context.Products.AnyAsync(x => x.Id == id))
            {

                _context.Products.Remove(new Product { Id = id.Value });
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));

        }
    }
}
