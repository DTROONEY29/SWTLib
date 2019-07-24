using LibraryData;
using Microsoft.AspNetCore.Http;
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
            var sessionId = HttpContext.Session.GetInt32("_Id");
            var user = _context.Users.FirstOrDefault(i => i.Id == sessionId);

            var allRentals = await _context.Rentals.Where(u => u.UserId == user.Id)
                .Include(i => i.Book)
                .OrderBy(i => i.ReturnDate)
                .ToListAsync();

            var rentals = allRentals.Where(x => x.ReturnDate.Date <= timeSpan.Date).ToList();

            return View(rentals);
        }
    }
}

