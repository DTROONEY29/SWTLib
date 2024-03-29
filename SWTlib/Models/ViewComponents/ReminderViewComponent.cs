﻿using LibraryData;
using LibraryData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWTlib.Models.ViewComponents
{
    public class ReminderViewComponent : ViewComponent
    {
        private readonly LibraryContext _context;
        public ReminderViewComponent(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? id)
        {
            int? userid = HttpContext.Session.GetInt32("_Id");
            var reminders = await _context.Reminders
                .Include(i => i.Book)
                    .ThenInclude(i => i.Rental)
                .Where(x => x.UserId == userid)
                .OrderBy(x => x.Book.Rental.ReturnDate)
                .ToListAsync();

            var rentals = await _context.Rentals.ToListAsync();
            ViewData["Rentals"] = rentals;

            return View(reminders);
        }
    }
}
