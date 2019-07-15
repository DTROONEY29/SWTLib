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
            var viewModel = new BookViewModel
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

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            try
            {
                var cat = new Category
                {
                    CategoryName = category.CategoryName
                };

                _context.Categories.Add(category);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Delete
        public ActionResult Delete()
        {
            var cat = _context.Categories.ToList();
            return View(cat);
        }

        // POST: Category/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var cat = _context.Categories.Find(id);
                _context.Categories.Remove(cat);
                _context.SaveChanges();

                return RedirectToAction("Delete", "Category");
            }
            catch
            {
                return View();
            }
        }

    }
}
