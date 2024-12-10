using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Uniqlo2.DataAccsess;
using Uniqlo2.Helpers;
using Uniqlo2.Models;
using Uniqlo2.ViewModels.Slider;

namespace Uniqlo2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =RoleConstants.Slider )]
    public class SliderController(UniqloDbContext _context,IWebHostEnvironment _env) : Controller
    {
        public async Task<IActionResult> Index()
        {
           
            return View(await _context.Sliders.ToListAsync());
        }
        
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> Create(SliderCreateVM vm)
        {
            if (vm.File != null)
            {
                if (!vm.File.ContentType.StartsWith("image"))
                {
                    ModelState.AddModelError("File", "File type must be image");
                }
                if (vm.File.Length > 800 * 1024)
                   ModelState.AddModelError("File", "File length must be lees than 800kb ");
                
            }
               
            if (!ModelState.IsValid) return View();
            string newFileName= Path.GetRandomFileName() + Path.GetExtension(vm.File.FileName);
            using (Stream stream = System.IO.File.Create(Path.Combine (_env.WebRootPath,"imgs","sliders",newFileName )))
            {
                await vm.File.CopyToAsync(stream);
            }
            Slider slider = new Slider
            {
                ImageUrl = newFileName,
                Link = vm.Link,
                Subtitle = vm.Subtitle,
                Title = vm.Title,

            };
            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult > Delete(int? id)
        {
            if (!id.HasValue) return BadRequest();
            if(await _context.Sliders.AnyAsync(x => x.Id == id))
            {
                
                _context.Sliders.Remove(new Slider { Id = id.Value });
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));


        }
        public async Task<IActionResult> Update(int? id)
        {   if (!id.HasValue) return BadRequest();
            var data= await _context.Sliders.FindAsync(id.Value);
            if (data  is null) return NotFound();

            var viewModel = new SliderCreateVM
            {
                Title = data.Title,
                Subtitle = data.Subtitle,
                Link = data.Link,
                
            };


            return View(viewModel);

        }
        [HttpPost ]
        public async Task<IActionResult> Update(int? id,SliderCreateVM vm)
        {if (!id.HasValue) return BadRequest();
            var entity = await _context.Sliders.FindAsync(id.Value );
            if(entity is null)return NotFound();

            if (vm.File != null)
            {
                if (!vm.File.ContentType.StartsWith("image"))
                {
                    ModelState.AddModelError("File", "File type must be image");
                }
                if (vm.File.Length > 800 * 1024)
                    ModelState.AddModelError("File", "File length must be lees than 800kb ");

            }
            if (!ModelState.IsValid) return View();
            string newFileName = Path.GetRandomFileName() + Path.GetExtension(vm.File.FileName);
            using (Stream stream = System.IO.File.Create(Path.Combine(_env.WebRootPath, "imgs", "sliders", newFileName)))
            {
                await vm.File.CopyToAsync(stream);
            }


            entity.ImageUrl = newFileName;
            entity.Link = vm.Link;
            entity.Subtitle = vm.Subtitle;
            entity.Title = vm.Title;

            
           
            await _context.SaveChangesAsync();




            return RedirectToAction(nameof(Index));

        }
        
        public async Task<IActionResult> Hide(int? id)
        {
            if (!id.HasValue) return BadRequest();
             var data= await _context.Sliders.FindAsync(id.Value);
            if (id is null) return NotFound();
            

             data.IsDeleted = true;
             await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
            
        }
        public async Task<IActionResult> Show(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var data = await _context.Sliders.FindAsync(id.Value);
            if (id is null) return NotFound();


            data.IsDeleted = false;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }


    }
}
