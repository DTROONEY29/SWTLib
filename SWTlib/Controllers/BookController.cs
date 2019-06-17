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
    public class BookController : Controller
    {
        private readonly LibraryContext _context;
        public BookController(LibraryContext context)
        {
            _context = context;
        }
        // GET: Book
        public ActionResult Index(int? id)
        {
            var viewModel = new BookAuthorViewModel();
            viewModel.BookList = _context.Books
                .Include(i => i.BookAuthors)
                    .ThenInclude(i => i.Author)
                .Include(i => i.BookCategories)
                    .ThenInclude(i => i.Category)
                .Include(i => i.BookKeywords)
                    .ThenInclude(i => i.Keyword)
                .AsNoTracking()
                .OrderBy(i => i.Title)
                .ToList();

            if (id != null)
            {
                ViewData["BookId"] = id.Value;
                Book book = viewModel.BookList.Where(
                    i => i.Id == id.Value).Single();
                viewModel.AuthorList = book.BookAuthors.Select(s => s.Author);
                viewModel.CategoryList = book.BookCategories.Select(s => s.Category);
                viewModel.KeywordList = book.BookKeywords.Select(s => s.Keyword);

            }

            return View(viewModel);
        }

        // GET: Book/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Books
                .Include(i => i.Location)
                .Include(i => i.BookAuthors)
                    .ThenInclude(i => i.Author)
                .Include(i => i.BookCategories)
                    .ThenInclude(i => i.Category)
                .Include(i => i.BookKeywords)
                    .ThenInclude(i => i.Keyword)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            var itemA = _context.BookAuthors.ToList();
            var itemC = _context.Categories.ToList();
            var itemK = _context.Keywords.ToList();

            BookAuthorViewModel bookAuthorViewModel = new BookAuthorViewModel();
            bookAuthorViewModel.AuthorList = itemA.Select(a => new Author()
            {
               // Id = a.Id,
              //  AuthorName = a.AuthorName
            }).ToList();

            bookAuthorViewModel.CategoryList = itemC.Select(a => new Category()
            {
                Id = a.Id,
                CategoryName = a.CategoryName
            }).ToList();

            bookAuthorViewModel.KeywordList = itemK.Select(a => new Keyword()
            {
                Id = a.Id,
                KeywordName = a.KeywordName
            }).ToList();

            return View(bookAuthorViewModel);
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel BAVM, Book books)
        {
            try
            {
                List<BookAuthor> ba = new List<BookAuthor>();
                List<BookCategory> bc = new List<BookCategory>();
                List<BookKeyword> bk = new List<BookKeyword>();

                books.Title = BAVM.Title;
                books.ISBN = BAVM.ISBN;
                books.Publisher = BAVM.Publisher;
                books.Year = BAVM.Year;
                books.Description = BAVM.Description;
                books.Language = BAVM.Language;
                books.Status = BAVM.Status;
                _context.Books.Add(books);
                _context.SaveChanges();
                int bookId = books.Id;

                //Authors
                foreach (var item in BAVM.AuthorList)
                {
                    ba.Add(new BookAuthor() { BookId = bookId, AuthorId = item.Id });                  
                }

                foreach (var item in ba)
                {
                    _context.BookAuthors.Add(item);
                }
                _context.SaveChanges();

                //Categories
                foreach (var item in BAVM.CategoryList)
                {
                    bc.Add(new BookCategory() { BookId = bookId, CategoryId = item.Id });
                }

                foreach (var item in bc)
                {
                    _context.BookCategories.Add(item);
                }
                _context.SaveChanges();

                //Keywords
                foreach (var item in BAVM.KeywordList)
                {
                    bk.Add(new BookKeyword() { BookId = bookId, KeywordId = item.Id });
                }

                foreach (var item in bk)
                {
                    _context.BookKeywords.Add(item);
                }
                _context.SaveChanges();

                return RedirectToAction("Index", "Book");
            }
            catch
            {
                return View();
            }
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Book/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Book/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}