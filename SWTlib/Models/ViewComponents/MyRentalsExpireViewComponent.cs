using LibraryData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWTlib.Models.ViewComponents
{
    public class MyRentalsExpireViewComponent : ViewComponent
    {
        private readonly LibraryContext _context;
        public MyRentalsExpireViewComponent(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var today = DateTime.Today.Date;
            var timeSpan = today.AddDays(7);

            var allRentals = await _context.Rentals
                .Include(i => i.Book)
                .OrderBy(i => i.ReturnDate)
                .ToListAsync();

            var rentals = allRentals.Where(x => x.ReturnDate.Date <= timeSpan.Date).ToList();

            return View(rentals);
        }
    }
}

