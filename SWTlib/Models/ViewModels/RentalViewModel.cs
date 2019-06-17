using LibraryData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWTlib.Models.ViewModels
{
    public class RentalViewModel : ViewTestModel
    {
        public int RentalId { get; set; }
       
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public IEnumerable<Rental> RentalList { get; set; }
    }
}
