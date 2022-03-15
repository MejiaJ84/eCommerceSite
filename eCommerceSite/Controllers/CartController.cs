using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eCommerceSite.Controllers
{
    public class CartController : Controller
    {

        private readonly WarhammerFigureContext _context;
        private const string Cart = "ShoppingCart";

        public CartController(WarhammerFigureContext context)
        {
            _context = context;
        }

        public IActionResult Add(int id)
        {
            // pulls figure from database matching the id
            Figure? figureToBuy = _context.Figures.Where(f => f.FigureId == id).SingleOrDefault();

            if (figureToBuy == null)
            {
                // Figure with the specified id no longer exists
                TempData["Message"] = "Sorry! That figure no longer exists.";
                return RedirectToAction("Index", "Figures");
            }

            // fills the ViewModel object with data from the figures database
            CartFigureViewModel cartFigure = new()
            {
                FigureId = figureToBuy.FigureId,
                Legion = figureToBuy.Legion,
                Type = figureToBuy.Type,
                Price = figureToBuy.Price
            };

            // enables multi-item shopping cart
            List<CartFigureViewModel> cartFigures = GetExistingCartData();
            cartFigures.Add(cartFigure);
            
            // converts the figure object to a string
            string cookieData = JsonConvert.SerializeObject(cartFigures);

            // Serialization JSON
            HttpContext.Response.Cookies.Append(Cart, cookieData, new CookieOptions()
            {
                Expires = DateTime.Now.AddYears(1) // cookie expires one year from now
            });

            // TODO: Add item to cart cookie
            TempData["Message"] = "Item added to cart.";
            return RedirectToAction("Index", "Figures");
        }

        /// <summary>
        /// Return the current list of figures in the users shopping 
        /// cart cookie. If there is no cookie, an empty list will be returned.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private List<CartFigureViewModel> GetExistingCartData()
        {
            string? cookie = HttpContext.Request.Cookies[Cart];
            if (string.IsNullOrWhiteSpace(cookie))
            {
                return new List<CartFigureViewModel>();
            }
            // converts the figure object from a string back to an object
            return JsonConvert.DeserializeObject<List<CartFigureViewModel>>(cookie);
        }

        public IActionResult Summary()
        {
            List<CartFigureViewModel> cartFigures = GetExistingCartData();
            return View(cartFigures);
        }
    }
}
