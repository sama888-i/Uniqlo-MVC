using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Uniqlo2.DataAccsess;
using Uniqlo2.Models;
using Uniqlo2.ViewModels.Slider;

namespace Uniqlo2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController(UniqloDbContext _context) : Controller
    {
        public async Task< IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create( Category data)
        {
            if (!ModelState.IsValid) return View();
            await _context.Categories.AddAsync(data);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult > Delete(int? id)
        {  if (!id.HasValue) return BadRequest();
           if( await _context .Categories.AnyAsync(x => x.Id == id))
           {
                _context.Categories.Remove(new Category { Id = id.Value });
                await _context.SaveChangesAsync();
           }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult >Update(int?id)
        {
            if (!id.HasValue) return BadRequest();
            var data = await _context.Categories.FindAsync(id.Value);
            if (data is null) return NotFound();
            return View(data);

        }
        [HttpPost ]
        public async Task <IActionResult>Update(Category data,int? id)
        {
            if (!id.HasValue) return BadRequest();
            var entity = await _context.Categories.FindAsync(id.Value);
            if (entity is null) return NotFound();
            entity.Name = data.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
