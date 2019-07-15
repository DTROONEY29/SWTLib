using LibraryData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWTlib.Models.ViewModels
{
    public class BookViewModel : JoinTableDataViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }

        public Location Location { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }
        public ICollection<BookCategory> BookCategories { get; set; }
        public ICollection<BookKeyword> BookKeywords { get; set; }
    }
}
