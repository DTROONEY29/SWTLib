using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibraryData.Models
{
    public class Keyword
    {
        public int Id { get; set; }
        [Required]
        public string KeywordName { get; set; }

        public ICollection<BookKeyword> BookKeywords { get; set; }
    }
}
