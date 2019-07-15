using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryData;
using LibraryData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWTlib.Models.ViewModels;

namespace SWTlib.Controllers
{
    public class BookmarkController : Controller
    {
        private readonly LibraryContext _context;
        public BookmarkController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Bookmark
        public ActionResult Index(int? id)
        {
            var viewModel = new BookmarkViewModel
            {
                BookmarkList = _context.Bookmarks
                .Include(i => i.Book)
                .Include(i => i.Book.BookAuthors)
                .ThenInclude(i => i.Author)
                .Include(i => i.Book.BookCategories)
                    .ThenInclude(i => i.Category)
                .Include(i => i.Book.BookKeywords)
                    .ThenInclude(i => i.Keyword)
                .AsNoTracking()
                .OrderBy(i => i.Book.Title)
                .ToList()
            };

            if (id != null)
            {
                ViewData["BookmarkId"] = id.Value;
                Bookmark bookmark = viewModel.BookmarkList.Where(
                    i => i.UserId == id.Value).Single();
                viewModel.AuthorList = bookmark.Book.BookAuthors.Select(s => s.Author);
                viewModel.CategoryList = bookmark.Book.BookCategories.Select(s => s.Category);
                viewModel.KeywordList = bookmark.Book.BookKeywords.Select(s => s.Keyword);

            }

            return View(viewModel);
        }


        // POST: Bookmark/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SetBookmark(int UserId, int BookId)
        {
            var bm = new Bookmark
            {
                UserId = UserId,
                BookId = BookId
            };

            try
            {                
                _context.Bookmarks.Add(bm);
                _context.SaveChanges();
                return RedirectToAction("Index", "Book");
            }
            catch
            {
                return View();
            }
        }

        // POST: Bookmark/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteBookmark(int UserId, int BookId)
        {
            var bookmark = _context.Bookmarks.Find(UserId, BookId);

            if (bookmark == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Bookmarks.Remove(bookmark);
                _context.SaveChanges();
                return RedirectToAction("Index", "Book");
            }
            catch
            {
                return View();
            }
        }
    }
}