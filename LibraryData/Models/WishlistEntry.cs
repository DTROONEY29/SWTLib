using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData.Models
{
    public class WishListEntry
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string AuthorName { get; set; }
        public int Rating { get; set; }
    }
}
