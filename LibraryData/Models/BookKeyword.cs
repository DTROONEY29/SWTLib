using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData.Models
{
    public class BookKeyword
    {
        public int BookId { get; set; }
        public int KeywordId { get; set; }

        public Book Book { get; set; }
        public Keyword Keyword { get; set; }
    }
}
