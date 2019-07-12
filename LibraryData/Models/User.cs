using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsChairMember { get; set; }

        public ICollection<Bookmark> Bookmarks { get; set; }
        public ICollection<Rental> Rentals { get; set; }
    }
}
