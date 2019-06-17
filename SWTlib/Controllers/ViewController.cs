using LibraryData;
using LibraryData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWTlib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWTlib.Controllers
{
    public class ViewController : Controller
    {
        private LibraryContext _context;
        public ViewController(LibraryContext context)
        {
            _context = context;
        }
        public ActionResult Index(int? id, int? AuthorId)
        {
            var viewModel = new ViewTestModel();
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
    }
}
