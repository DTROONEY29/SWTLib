using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData.Models
{
    public class Bookmark
    {
        public int Id { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }

        //public int CustomerId { get; set; } 
        //public Customer Customer { get; set; } 
    }
}
