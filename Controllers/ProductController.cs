using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Claims;
using Uniqlo2.DataAccsess;

namespace Uniqlo2.Controllers
{
    public class ProductController(UniqloDbContext _context) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult>Details(int? id)
        {
            if (!id.HasValue)
                return BadRequest();
            var data = await _context.Products
                .Where(x => x.Id == id.Value && !x.IsDeleted)
                .Include(x => x.Images)
                .Include(x=>x.Ratings)
                .FirstOrDefaultAsync();
            if (data is null) return NotFound();
            ViewBag.Rating = 5;
            if(User.Identity?.IsAuthenticated ?? false)
            {
                string userId = User.Claims.FirstOrDefault(x=>x.Type==ClaimTypes.NameIdentifier)!.Value;
                int rating = await _context.ProductRatings.Where(x => x.UserId == userId && x.ProductId == id).Select(x => x.Rating).FirstOrDefaultAsync();
                ViewBag.Rating = rating == 0 ? 5 :rating;
            }
            return View(data);


        }
        public async Task<IActionResult> Rating(int productId,int rating)
        {
            string userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
            var data = await _context.ProductRatings.Where(x => x.UserId == userId && x.ProductId == productId).FirstOrDefaultAsync();
            if(data is null)
            {
                await _context.ProductRatings.AddAsync(new Models.ProductRating
                {
                    UserId = userId,
                    ProductId = productId,
                    Rating = rating
                });
            }
            else
            {
                data.Rating = rating;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details),new {Id=productId});
        }
        public async Task<IActionResult> Comment()
        {
            return View();
        }
    }
}
