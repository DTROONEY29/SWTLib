using LibraryData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWTlib.Models
{
    public class ViewTestModel
    {
        public IEnumerable<Book> BookList { get; set; }

        public IEnumerable<Author> AuthorList { get; set; }
        public IEnumerable<BookAuthor> BookAuthorList { get; set; }

        public IEnumerable<Category> CategoryList { get; set; }
        public IEnumerable<BookCategory> BookCategoryList { get; set; }

        public IEnumerable<Keyword> KeywordList { get; set; }
        public IEnumerable<BookKeyword> BookKeywordList { get; set; }

    }
}
