using LibraryData.Models;
using SWTlib.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWTlib.Models.ViewModels
{
    public class RentalViewModel : ListViewModel
    {
        public int RentalId { get; set; }
       
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public IEnumerable<Rental> RentalList { get; set; }
    }
}
