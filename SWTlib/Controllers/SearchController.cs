using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryData;
using LibraryData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SWTlib.Models.ViewModels;

namespace SWTlib.Controllers
{
    public class SearchController : Controller
    {
        private readonly LibraryContext _context;
        public SearchController(LibraryContext context)
        {
            _context = context;
        }

        //Search logic for search-field.
        public IActionResult Results(string searchString, string searchPublisher, string searchRoom, string[] selectedAuthors, string[] selectedCategories, string[] selectedKeywords)
        {
            var books = from b in _context.Books
                        .Include(i => i.BookAuthors)
                            .ThenInclude(i => i.Author)
                        .Include(i => i.BookCategories)
                            .ThenInclude(i => i.Category)
                        .Include(i => i.BookKeywords)
                            .ThenInclude(i => i.Keyword)
                        .Include(i => i.Bookmarks)
                        .Include(i => i.Location)
                        select b;

            var authors = from a in _context.BookAuthors
                          .Include(i => i.Author)
                          select a;
            var categories = from c in _context.BookCategories
                             .Include(i => i.Category)
                             select c;
            var keywords = from k in _context.BookKeywords
                             .Include(i => i.Keyword)
                             select k;


            var sBooks = new List<Book>();
            var bookmarks = _context.Bookmarks.ToList();

            //Simple search field
            if(selectedAuthors.Length == 0 && selectedCategories.Length == 0 && selectedKeywords.Length == 0 && String.IsNullOrEmpty(searchPublisher) && String.IsNullOrEmpty(searchRoom))
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    string[] searchTerms = searchString.Split(' ');

                    foreach (var term in searchTerms)
                    {
                        sBooks.AddRange(books.Where(s => s.Title.Contains(term) 
                        || s.ISBN.Contains(term)
                        || s.Publisher.Contains(term)
                        || s.Year.ToString().Contains(term)
                        || s.Location.ToString().Contains(term)
                        ));
                    }
                }
            }

            //Advanced search field.
            else if (!String.IsNullOrEmpty(searchString) || !String.IsNullOrEmpty(searchPublisher) || !String.IsNullOrEmpty(searchRoom) || selectedAuthors.Length != 0 || selectedCategories.Length != 0 || selectedKeywords.Length != 0)
            {
                if (!String.IsNullOrEmpty(searchString)) 
                {
                    string[] searchTerms = searchString.Split(' ');

                    foreach (var term in searchTerms)
                    {
                        sBooks.AddRange(books.Where(s => s.Title.Contains(term)
                        || s.ISBN.Contains(term)
                        || s.Publisher.Contains(searchPublisher)
                        || s.Year.ToString().Contains(term)
                        || s.Location.Id.ToString().Contains(searchRoom)
                        ));
                    }
                }

                /*
                sBooks.AddRange(books.Where(s => s.Publisher.Contains(searchPublisher)
                || s.Location.Id.ToString().Contains(searchRoom)
                ));
                */
                HashSet<Book> multipleSearch = new HashSet<Book>();

                foreach (var a in selectedAuthors)
                {
                    multipleSearch.UnionWith(authors.Where(b => b.AuthorId == Convert.ToInt32(a)).Select(i => i.Book).Include(i => i.BookKeywords)
                                                                                                                            .ThenInclude(i => i.Keyword)
                                                                                                                       .Include(i => i.BookCategories)
                                                                                                                            .ThenInclude(i => i.Category)
                                                                                                                       .Include(i => i.BookAuthors)
                                                                                                                            .ThenInclude(i => i.Author));
                }
                foreach (var a in selectedCategories)
                {
                    multipleSearch.UnionWith(categories.Where(b => b.CategoryId == Convert.ToInt32(a)).Select(i => i.Book).Include(i => i.BookKeywords)
                                                                                                                            .ThenInclude(i => i.Keyword)
                                                                                                                       .Include(i => i.BookCategories)
                                                                                                                            .ThenInclude(i => i.Category)
                                                                                                                       .Include(i => i.BookAuthors)
                                                                                                                            .ThenInclude(i => i.Author));
                }
                foreach (var a in selectedKeywords)
                {
                    multipleSearch.UnionWith(keywords.Where(b => b.KeywordId == Convert.ToInt32(a)).Select(i => i.Book).Include(i => i.BookKeywords)
                                                                                                                            .ThenInclude(i => i.Keyword)
                                                                                                                       .Include(i => i.BookCategories)
                                                                                                                            .ThenInclude(i => i.Category)
                                                                                                                       .Include(i => i.BookAuthors)
                                                                                                                            .ThenInclude(i => i.Author));
                }

                                
                sBooks = multipleSearch.ToList();

                if (!String.IsNullOrEmpty(searchRoom))
                {
                    sBooks = sBooks.Where(i => i.LocationId.ToString().Contains(searchRoom)).ToList();
                }
                
            }

            ViewBag.Bookmarks = bookmarks;
            return View(sBooks);
        }

        public IActionResult Advanced()
        {
            var book = new Book
            {
                BookAuthors = new List<BookAuthor>(),
                BookCategories = new List<BookCategory>(),
                BookKeywords = new List<BookKeyword>()
            };

            LocationDropDownList();

            var authors = _context.Authors.Select(c => new
            {
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
            ViewBag.Keywords = new MultiSelectList(keywrd, "KeywordId", "KeywordName");

            return View();
        }

        private void LocationDropDownList(object selectedLocation = null)
        {
            var locationsQuery = from l in _context.Locations
                                 orderby l.LocationName
                                 select l;
            ViewBag.Id = new SelectList(locationsQuery.AsNoTracking(), "Id", "LocationName", selectedLocation);
        }


    }
}