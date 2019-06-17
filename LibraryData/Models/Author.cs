using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibraryData.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
