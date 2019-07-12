using LibraryData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWTlib.Models.ViewModels
{
    public class BookDetailModel : ListViewModel
    {
        public int BookId { get; set; }
        public Book Book { get; set; }
        public Location Location { get; set; }
    }
}
