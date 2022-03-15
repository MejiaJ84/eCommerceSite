using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace eCommerceSite.Controllers
{
    public class FiguresController : Controller
    {

        private readonly WarhammerFigureContext _context;

        public FiguresController(WarhammerFigureContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id)
        {
            const int NumFiguresToDisplayPerPage = 3;
            const int PageOffset = 1; // Need a page offset to use the current page and figure out the number of figures to skip.
            
            // Set current page to id if it has a value, else set to 1.
            int currPage = id ?? 1; // null-coalescing operator

            int totalNumOfProducts = await _context.Figures.CountAsync();
            double maxNumPages = Math.Ceiling((double)totalNumOfProducts / NumFiguresToDisplayPerPage);
            int lastPage = Convert.ToInt32(maxNumPages); // Round pages up to next whole number
            
            // Get all figures from database
            List<Figure> figures = await _context.Figures
                                                 .Skip(NumFiguresToDisplayPerPage * (currPage - PageOffset))
                                                 .Take(NumFiguresToDisplayPerPage)
                                                 .ToListAsync(); // method syntax

            FigureCatalogViewModel catalogModel = new(figures, lastPage, currPage);
            return View(catalogModel);
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

        // int id is the id passed in the link at the bottom of the index.cshtml
        // page in the three edit, details, and delete links
        public async Task<IActionResult> Edit(int id)
        {
            Figure figureToEdit = await _context.Figures.FindAsync(id);
            if (figureToEdit == null)
            {
                return NotFound();
            }
            return View(figureToEdit);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(Figure figureModel)
        {
            if (ModelState.IsValid)
            {
                _context.Figures.Update(figureModel);
                await _context.SaveChangesAsync();

                TempData["Message"] = $"{figureModel.Legion} {figureModel.Type} was updated successfully!";
                return RedirectToAction("Index"); // sends back to index page if successful
            }
            return View(figureModel); // returns to edit page with figure  if it didn't update successfully
        }

        public async Task<IActionResult> Delete(int id)
        {
            Figure figureToDelete = await _context.Figures.FindAsync(id);

            if (figureToDelete == null)
            {
                return NotFound();
            }

            return View(figureToDelete);
        }

        // so c# recognizes this method as Delete() even though they have different names.
        // The Get and Post versions are supposed to have the same names, but also can't 
        // have the exact same signature(same name and parameters), ActionName("Delete")
        // solves this problem.
        [HttpPost, ActionName("Delete")] 
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Figure figureToDelete = await _context.Figures.FindAsync(id);

            if (figureToDelete != null)
            {
                _context.Figures.Remove(figureToDelete);
                await _context.SaveChangesAsync();
                TempData["Message"] = $"{figureToDelete.Legion} {figureToDelete.Type} was deleted successfully!";
                return RedirectToAction("Index");
            }

            TempData["Message"] = $"This figure was already deleted or not in the database.";
            return RedirectToAction("Index");


        }

        public async Task<IActionResult> Details(int id)
        {
            Figure figureDetails = await _context.Figures.FindAsync(id);

            if (figureDetails == null)
            {
                return NotFound();
            }
            return View(figureDetails);
        }

    }
}
