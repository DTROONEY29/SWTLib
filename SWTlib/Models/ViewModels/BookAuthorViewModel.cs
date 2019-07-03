using LibraryData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWTlib.Models.ViewModels
{
    public class BookAuthorViewModel : ViewTestModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }

        public Location Location { get; set; }
    }
}
