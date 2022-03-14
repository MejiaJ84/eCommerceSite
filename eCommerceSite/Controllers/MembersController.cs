using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceSite.Controllers
{
    public class MembersController : Controller
    {

        private readonly WarhammerFigureContext _context;

        public MembersController(WarhammerFigureContext context)
        {
            _context = context;
        }



        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel regModel)
        {
            if (ModelState.IsValid)
            {
                // map RegisterViewModel data to Member object
                Member newMember = new()
                {
                    Email = regModel.Email,
                    Password = regModel.Password
                };

                _context.Members.Add(newMember);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");

            }
            return View(regModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}
