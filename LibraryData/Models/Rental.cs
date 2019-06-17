using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibraryData.Models
{
    public class Rental
    {
        public int Id { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
               ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime RentalDate {
            get
            {
                return this.rentalDate.HasValue
                ? this.rentalDate.Value
                : DateTime.Now;
            }

            set { this.rentalDate = value; }
        }

        private DateTime? rentalDate = null;
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
               ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }

        //public Customer Customer { get; set; }
    }
}
