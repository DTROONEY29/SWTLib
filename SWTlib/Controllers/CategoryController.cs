using LibraryData;
using LibraryData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWTlib.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWTlib.Controllers
{
    public class CategoryController : Controller
    {
        private readonly LibraryContext _context;
        public CategoryController(LibraryContext context)
        {
            _context = context;
        }

        public ActionResult Index(int? id)
        {
            var viewModel = new BookAuthorViewModel
            {
                CategoryList = _context.Categories
                .Include(i => i.BookCategories)
                    .ThenInclude(i => i.Book)
                .AsNoTracking()
                .ToList()
            };

            if (id != null)
            {
                ViewData["CategoryId"] = id.Value;
                Category cat = viewModel.CategoryList.Where(
                    i => i.Id == id.Value).Single();
                viewModel.BookList = cat.BookCategories.Select(s => s.Book);

            }

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cat = await _context.Categories
                .Include(i => i.BookCategories)
                    .ThenInclude(i => i.Book)
                    .ThenInclude(i => i.BookAuthors)
                    .ThenInclude(i => i.Author)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cat == null)
            {
                return NotFound();
            }

            return View(cat);
        }
    }
}
