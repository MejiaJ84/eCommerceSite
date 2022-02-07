using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;


namespace eCommerceSite.Controllers
{
    public class FiguresController : Controller
    {

        private readonly WarhammerFigureContext _context;

        public FiguresController(WarhammerFigureContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Figure f)
        {
            if (ModelState.IsValid)
            {
                _context.Figures.Add(f); // Prepares insert
                _context.SaveChanges(); // Executes pending insert

                ViewData["Message"] = $"{f.Legion} {f.Type} was added successfully!";
                return View();
            }

            return View(f);
        }

    }
}
