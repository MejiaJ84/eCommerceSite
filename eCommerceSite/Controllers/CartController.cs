using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceSite.Controllers
{
    public class CartController : Controller
    {

        private readonly WarhammerFigureContext _context;

        public CartController(WarhammerFigureContext context)
        {
            _context = context;
        }

        public IActionResult Add(int id)
        {

            Figure? figureToBuy = _context.Figures.Where(f => f.FigureId == id).SingleOrDefault();

            if (figureToBuy == null)
            {
                // Figure with the specified id no longer exists
                TempData["Message"] = "Sorry! That figure no longer exists.";
                return RedirectToAction("Index", "Figures");
            }
            // TODO: Add item to cart cookie
            TempData["Message"] = "Item added to cart.";
            return RedirectToAction("Index", "Figures");
        }
    }
}
