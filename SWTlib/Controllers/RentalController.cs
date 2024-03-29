﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LibraryData;
using LibraryData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SWTlib.Models;
using SWTlib.Models.Helpers;
using SWTlib.Models.ViewModels;

namespace SWTlib.Controllers
{
    public class RentalController : Controller
    {
        private readonly LibraryContext _context;
        public RentalController(LibraryContext context)
        {
            _context = context;
        }

        private void ReturnDateDropDown(object selectedDate = null)
        {
            var returnDates = new List<ReturnDateHelper>
            {
                new ReturnDateHelper
                {
                    Duration = "1 Week",
                    ReturnDate = DateTime.Now.AddDays(7)
                },
                new ReturnDateHelper
                {
                    Duration = "2 Weeks",
                    ReturnDate = DateTime.Now.AddDays(14)
                },
                new ReturnDateHelper
                {
                    Duration = "3 Weeks",
                    ReturnDate = DateTime.Now.AddDays(21)
                },
                new ReturnDateHelper
                {
                    Duration = "4 Weeks",
                    ReturnDate = DateTime.Now.AddMonths(1)
                }
            };


            var dateQuery = from d in returnDates
                            orderby d.Duration
                            select d;
            ViewBag.ReturnDate = new SelectList(dateQuery, "ReturnDate", "Duration", selectedDate);
        }

        private void ExtendReturnDateDropDown(int? RentalId, object selectedDate = null)
        {
            Rental rental = _context.Rentals.Where(r => r.Id == RentalId.Value).Single();
            var lastDate = rental.ReturnDate;

            var returnDates = new List<ReturnDateHelper>
            {
                new ReturnDateHelper
                {
                    Duration = "1 Week",
                    ReturnDate = lastDate.AddDays(7)
                },
                new ReturnDateHelper
                {
                    Duration = "2 Weeks",
                    ReturnDate = lastDate.AddDays(14)
                },
                new ReturnDateHelper
                {
                    Duration = "3 Weeks",
                    ReturnDate = lastDate.AddDays(21)
                },
                new ReturnDateHelper
                {
                    Duration = "4 Weeks",
                    ReturnDate = lastDate.AddMonths(1)
                }
            };


            var dateQuery = from d in returnDates
                            orderby d.Duration
                            select d;
            ViewBag.ReturnDate = new SelectList(dateQuery, "ReturnDate", "Duration", selectedDate);
        }

        // GET: Rental
        public ActionResult Index(int? id)
        {
            var sessionId = HttpContext.Session.GetInt32("_Id");
            var user = _context.Users.FirstOrDefault(i => i.Id == sessionId);

            var viewModel = new RentalViewModel
            {
                RentalList = _context.Rentals.Where(u => u.UserId == user.Id)
                .Include(i => i.Book)
                .Include(i => i.Book.BookAuthors)
                    .ThenInclude(i => i.Author)
                .Include(i => i.Book.BookCategories)
                    .ThenInclude(i => i.Category)
                .Include(i => i.Book.BookKeywords)
                    .ThenInclude(i => i.Keyword)
                .AsNoTracking()
                .OrderBy(i => i.ReturnDate)
                .ToList()
            };

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
        public ActionResult Details()
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
            var sessionId = HttpContext.Session.GetInt32("_Id");
            var user = _context.Users.FirstOrDefault(i => i.Id == sessionId);

            if (book == null)
            {
                return NotFound();
            }

            var createRental = new Rental
            {
                BookId = book.Id,
                Book = book,
                UserId = user.Id,
                User = user
            };

          
            ReturnDateDropDown();
            return View(createRental);
        }

        // POST: Rental/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("RentalId,BookId,RentalDate,ReturnDate")] Rental createRental)
        {
            var sessionId = HttpContext.Session.GetInt32("_Id");
            var user = _context.Users.FirstOrDefault(i => i.Id == sessionId);

            if (ModelState.IsValid)
            {
                createRental.User = user;
                createRental.UserId = user.Id;
                _context.Rentals.Add(createRental);
                _context.SaveChanges();

                var book = _context.Books.Find(createRental.BookId);
                book.Status = true;
                _context.SaveChanges();

                return RedirectToAction("Index", "Rental");
            }
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

            ExtendReturnDateDropDown(id);
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
                    rentalToUpdate.ExtendedRental = true;
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

                var book = _context.Books.Find(rental.BookId);
                book.Status = false;
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id, saveChangesError = true });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddReminder(int userid, int bookid)
        {
            var book = _context.Books.Find(bookid);
            var user = _context.Users.Find(userid);



            var newReminder = new Reminder
            {
                UserId = userid,
                BookId = bookid,
                Book = book,
                User = user                
            };

            try
            {
                _context.Reminders.Add(newReminder);
                _context.SaveChanges();

                return RedirectToAction("Index", "Book");
            }
            catch
            {
                return RedirectToAction("Details", "Book", new { Id = bookid });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteReminder(int userid, int bookid)
        {
            var reminder = _context.Reminders.Find(userid, bookid);

            if (reminder == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Reminders.Remove(reminder);
                _context.SaveChanges();
                return RedirectToAction("Index", "Book");
            }
            catch
            {
                return View();
            }
        }

        [Route("component/[action]")]
        public IActionResult Reminder() => ViewComponent("Reminder");

        [Route("component/[action]")]
        public IActionResult MyRentalsExpire() => ViewComponent("MyRentalsExpire");
    }
}