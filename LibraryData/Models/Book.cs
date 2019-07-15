using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibraryData.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [Column(TypeName = "varchar(20)")]
        [Remote("IsIsbnExistend", "Book", AdditionalFields = "Id", ErrorMessage = "ISBN already exists.")]
        public string ISBN { get; set; }
        public string Publisher { get; set; }
        public int? Year { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public bool Status { get; set; }

        public int RatingUp { get; set; }
        public int RatingDown { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; }
        public Rental Rental { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }
        public ICollection<BookCategory> BookCategories { get; set; }
        public ICollection<BookKeyword> BookKeywords { get; set; }
        public ICollection<Bookmark> Bookmarks { get; set; }
    }
}
