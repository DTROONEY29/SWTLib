using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibraryData.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string CategoryName { get; set; }

        public ICollection<BookCategory> BookCategories { get; set; }
    }
}
