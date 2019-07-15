using LibraryData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWTlib.Models
{
    public class ListViewModel
    {
        public IEnumerable<Book> BookList { get; set; }
        public IEnumerable<Author> AuthorList { get; set; }
        public IEnumerable<Category> CategoryList { get; set; }
        public IEnumerable<Keyword> KeywordList { get; set; }
        public IEnumerable<Location> LocationList { get; set; }
        public IEnumerable<Bookmark> BookmarkList { get; set; }
        public IEnumerable<Rating> RatingList { get; set; }
    }
}
