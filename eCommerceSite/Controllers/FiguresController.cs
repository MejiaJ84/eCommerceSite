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
        public async Task<IActionResult> Create(Figure f)
        {
            if (ModelState.IsValid)
            {
                _context.Figures.Add(f); // Prepares insert
                await _context.SaveChangesAsync(); // Executes pending insert
                // async allows multitasking while waiting for the execute above to complete
                // for async code tutorial 
                // https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro?view=aspnetcore-6.0#asynchronous-code
                ViewData["Message"] = $"{f.Legion} {f.Type} was added successfully!";
                return View();
            }

            return View(f);
        }

    }
}
