using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryData;
using LibraryData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWTlib.Models;
using SWTlib.Models.ViewModels;

namespace SWTlib.Controllers
{
    public class RentalController : Controller
    {
        private LibraryContext _context;
        public RentalController(LibraryContext context)
        {
            _context = context;
        }
        // GET: Rental
        public ActionResult Index(int? id, int? BookId )
        {
            var viewModel = new RentalViewModel();
            viewModel.RentalList = _context.Rentals
                .Include(i => i.Book)
                .Include(i => i.Book.BookAuthors)
                    .ThenInclude(i => i.Author)
                .Include(i => i.Book.BookCategories)
                    .ThenInclude(i => i.Category)
                .Include(i => i.Book.BookKeywords)
                    .ThenInclude(i => i.Keyword)
                .AsNoTracking()
                .OrderBy(i => i.ReturnDate)
                .ToList();

            if (id != null)
            {
                ViewData["RentalId"] = id.Value;
                Rental rental = viewModel.RentalList.Where(
                    i => i.Id == id.Value).Single();
                viewModel.AuthorList = rental.Book.BookAuthors.Select(s => s.Author);
                viewModel.CategoryList = rental.Book.BookCategories.Select(s => s.Category);
                viewModel.KeywordList = rental.Book.BookKeywords.Select(s => s.Keyword);

            }

            return View(viewModel);

            
        }


        // GET: Rental/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Rental/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _context.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            var returnDate = DateTime.Today;
            returnDate.AddDays(14);

            var createRental = new Rental { BookId = book.Id, RentalDate = DateTime.Now, ReturnDate = returnDate, Book = book };
            //ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name");
            return View(createRental);
        }

        // POST: Rental/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("RentalId,BookId,RentalDate,ReturnDate")] Rental createRental)
        {
            if (ModelState.IsValid)
            {
                _context.Rentals.Add(createRental);
                _context.SaveChanges();
                return RedirectToAction("Index", "Rental");
            }

            //ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name", borrowHistory.CustomerId);
            createRental.Book = _context.Books.Find(createRental.BookId);
            return View(createRental);
        }

        // GET: Rental/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals
                .Include(s => s.Book)
                    .ThenInclude(e => e.BookAuthors)
                        .ThenInclude(s => s.Author)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // POST: Rental/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var rentalToUpdate = await _context.Rentals.FirstOrDefaultAsync(s => s.Id == id);
            if (await TryUpdateModelAsync<Rental>(
                rentalToUpdate,
                "",
                s => s.ReturnDate))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(rentalToUpdate);
        }

        // GET: Rental/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if(id == null)
            {
                return NotFound();
            }

            var rental = _context.Rentals
                .Include(i => i.Book)
                .AsNoTracking()
                .FirstOrDefault(m => m.Id == id);
            if (rental == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(rental);
        }

        // POST: Rental/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Rentals.Remove(rental);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }
    }
}