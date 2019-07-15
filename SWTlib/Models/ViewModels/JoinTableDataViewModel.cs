using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWTlib.Models.ViewModels
{
    public class JoinTableDataViewModel : ListViewModel
    {
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public int KeywordId { get; set; }

        public string AuthorName { get; set; }
        public string CategoryName { get; set; }
        public string KeywordName { get; set; }

        public bool AAssigned { get; set; }
        public bool CAssigned { get; set; }
        public bool KAssigned { get; set; }
    }
}
