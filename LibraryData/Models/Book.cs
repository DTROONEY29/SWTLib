using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibraryData.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int ISBN { get; set; }
        public string Publisher { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public bool Status { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; }
        public Rental Rental { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }
        public ICollection<BookCategory> BookCategories { get; set; }
        public ICollection<BookKeyword> BookKeywords { get; set; }
    }
}
