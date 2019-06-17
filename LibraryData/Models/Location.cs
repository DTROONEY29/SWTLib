using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibraryData.Models
{
    public class Location
    {
        public int Id { get; set; }
        [Required]
        public string LocationName { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
