using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Uniqlo2.Enums;

namespace Uniqlo2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =nameof(Roles.Admin))]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
