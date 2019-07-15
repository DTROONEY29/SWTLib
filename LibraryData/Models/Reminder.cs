using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData.Models
{
    public class Reminder
    {
        public int BookId { get; set; }
        public Book Book { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
