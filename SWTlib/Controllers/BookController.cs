using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryData;
using LibraryData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            var viewModel = new BookViewModel
            {
                BookList = _context.Books
                .Include(i => i.BookAuthors)
                    .ThenInclude(i => i.Author)
                .Include(i => i.BookCategories)
                    .ThenInclude(i => i.Category)
                .Include(i => i.BookKeywords)
                    .ThenInclude(i => i.Keyword)
                .AsNoTracking()
                .OrderBy(i => i.Title)
                .ToList(),

                BookmarkList = _context.Bookmarks
                .ToList()
            };

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

            var book = await _context.Books
                .Include(i => i.Location)
                .Include(i => i.BookAuthors)
                    .ThenInclude(i => i.Author)
                .Include(i => i.BookCategories)
                    .ThenInclude(i => i.Category)
                .Include(i => i.BookKeywords)
                    .ThenInclude(i => i.Keyword)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (book == null)
            {
                return NotFound();
            }
            CheckRating();

            return View(book);
        }

        public void CheckRating()
        {
            var allRatings = _context.Ratings.ToList();

            ViewBag.Ratings = allRatings;
        }

        private void LocationDropDownList(object selectedLocation = null)
        {
            var locationsQuery = from l in _context.Locations
                                 orderby l.LocationName
                                 select l;
            ViewBag.Id = new SelectList(locationsQuery.AsNoTracking(), "Id", "LocationName", selectedLocation);
        }

        private void PopulateJoinTableData(Book book)
        {
            var allAuthors = _context.Authors;
            var allCategories = _context.Categories;
            var allKeywords = _context.Keywords;

            var bAuthors = new HashSet<int>(book.BookAuthors.Select(a => a.AuthorId));
            var bCategories = new HashSet<int>(book.BookCategories.Select(a => a.CategoryId));
            var bKeywords = new HashSet<int>(book.BookKeywords.Select(a => a.KeywordId));

            var authorViewModel = new List<JoinTableDataViewModel>();
            var categoryViewModel = new List<JoinTableDataViewModel>();
            var keywordViewModel = new List<JoinTableDataViewModel>();        

            //Authors
            foreach (var author in allAuthors)
            {
                authorViewModel.Add(new JoinTableDataViewModel
                {
                    AuthorId = author.Id,
                    AuthorName = author.AuthorName,
                    AAssigned = bAuthors.Contains(author.Id)
                });
            }
            //Categories
            foreach (var category in allCategories)
            {
                categoryViewModel.Add(new JoinTableDataViewModel
                {
                    CategoryId = category.Id,
                    CategoryName = category.CategoryName,
                    CAssigned = bCategories.Contains(category.Id)
                });
            }
            //Keywords
            foreach (var keyword in allKeywords)
            {
                keywordViewModel.Add(new JoinTableDataViewModel
                {
                    KeywordId = keyword.Id,
                    KeywordName = keyword.KeywordName,
                    KAssigned = bKeywords.Contains(keyword.Id)
                });
            }

            ViewData["Authors"] = authorViewModel;
            ViewData["Categories"] = categoryViewModel;
            ViewData["Keywords"] = keywordViewModel;

            
        }
              

        // GET: Book/Create
        public IActionResult Create()
        {
            var book = new Book
            {
                BookAuthors = new List<BookAuthor>(),
                BookCategories = new List<BookCategory>(),
                BookKeywords = new List<BookKeyword>()
            };

            LocationDropDownList();
            
            var authors = _context.Authors.Select(c => new {
                AuthorId = c.Id,
                c.AuthorName
            }).ToList();
            ViewBag.Authors = new MultiSelectList(authors, "AuthorId", "AuthorName");

            var categories = _context.Categories.Select(c => new
            {
                CategoryId = c.Id,
                c.CategoryName
            }).ToList();
            ViewBag.Categories = new MultiSelectList(categories, "CategoryId", "CategoryName");
            
            var keywrd = _context.Keywords.Select(c => new
            {
                KeywordId = c.Id,
                c.KeywordName
            }).ToList();
            ViewBag.Keywords = new MultiSelectList(keywrd, "KeywordId", "KeywordName");/**/

            return View();            
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Title,ISBN,Publisher,Year,Description,Language,LocationId,")] Book book, string[] selectedAuthors, string[] selectedCategories, string[] selectedKeywords)
        {
            
            if (selectedAuthors != null)
            {
                //Create a new List<int> to store the AuthorId's.
                var authorsList = new List<int>();

                foreach (var item in selectedAuthors)
                {
                    bool isInt = int.TryParse(item, out int n);     //Try to parse the string item from selectedAuthors to int and return a bool.
                    if (isInt == true)
                    {
                        authorsList.Add(int.Parse(item));           //If success: add item to authorsList.
                    }

                    if (isInt == false)                             //If false: create a new author and add it to the database.
                    {
                        var newAuthor = new Author { AuthorName = item };
                        _context.Authors.Add(newAuthor);
                        await _context.SaveChangesAsync();

                        var newAuthorId = _context.Authors.Find(newAuthor.Id);      //Get the Id of the created author and add it to authorsList.
                        authorsList.Add(newAuthorId.Id);
                    }
                }

                book.BookAuthors = new List<BookAuthor>();
                //Add Book and Author to Join-Table
                foreach (var author in authorsList)
                {
                    var authorToAdd = new BookAuthor { BookId = book.Id, AuthorId = author };
                    book.BookAuthors.Add(authorToAdd);
                }           
            }

            if (selectedCategories != null)
            {
                book.BookCategories = new List<BookCategory>();
                //Add Book and Category to Join-Table
                foreach (var category in selectedCategories)
                {
                    var categoryToAdd = new BookCategory { BookId = book.Id, CategoryId = int.Parse(category) };
                    book.BookCategories.Add(categoryToAdd);
                }
            }

            if (selectedKeywords != null)
            {
                //Create a new List<int> to store the KeywordId's.
                var keywordsList = new List<int>();

                foreach (var item in selectedKeywords)
                {
                    bool isInt = int.TryParse(item, out int n);     //Try to parse the string item from selectedKeywords to int and return a bool.
                    if (isInt == true)
                    {
                        keywordsList.Add(int.Parse(item));           //If success: add item to keywordsList.
                    }

                    if (isInt == false)                             //If false: create a new keyword and add it to the database.
                    {
                        var newKeyword = new Keyword { KeywordName = item };
                        _context.Keywords.Add(newKeyword);
                        await _context.SaveChangesAsync();

                        var newKeywordId = _context.Keywords.Find(newKeyword.Id);      //Get the Id of the created keyword and add it to keywordsList.
                        keywordsList.Add(newKeywordId.Id);
                    }
                }

                book.BookKeywords = new List<BookKeyword>();
                //Add Book and Keyword to Join-Table
                foreach (var keyword in keywordsList)
                {
                    var keywordToAdd = new BookKeyword { BookId = book.Id, KeywordId = keyword };
                    book.BookKeywords.Add(keywordToAdd);
                }
            }


            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            LocationDropDownList(book.LocationId);

            return View(book);
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int id) => View();

        // POST: Book/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            if (collection == null)
            {
                throw new System.ArgumentNullException(nameof(collection));
            }

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
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }           

            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            if (collection == null)
            {
                throw new System.ArgumentNullException(nameof(collection));
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));                
            }
            catch
            {
                return View();
            }
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddDown(int userid, int bookid)
        {
            var book = _context.Books.FirstOrDefault(s => s.Id == bookid);
            var user = _context.Users.FirstOrDefault(u => u.Id == userid);
            var rating = new Rating
            {
                BookId = book.Id,
                UserId = user.Id
            };

            try
            {
                _context.Ratings.Add(rating);
                book.RatingDown += 1;
                _context.SaveChanges();
                return RedirectToAction("Details", "Book", new { Id = bookid });
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, " +
                    "see your system administrator.");
                throw;
            }
        }

        // Rating Logic
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddUp(int? id)
        {  
            var book = _context.Books.FirstOrDefault(s => s.Id == id);
            
                try
                {
                    book.RatingUp += 1;
                    _context.SaveChanges();
                return RedirectToAction("Details", "Book", new { Id = id });
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                throw;
                }
            }            
        }
}