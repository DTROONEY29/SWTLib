using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LibraryData;
using LibraryData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWTlib.Models;
using SWTlib.Models.ViewModels;

namespace SWTlib.Controllers
{
    public class HomeController : Controller
    {
        private readonly LibraryContext _context;
        public HomeController(LibraryContext context)
        {
            _context = context;
        }
        public ActionResult Index(int? id)
        {
            try
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
            catch
            {
                throw new Exception("There went something wrong");
            }

            
        }

        public async Task<IActionResult> Category(int? id)
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

        public IActionResult AddContent()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
