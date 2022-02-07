using Microsoft.AspNetCore.Mvc;

namespace eCommerceSite.Controllers
{
    public class FiguresController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}
