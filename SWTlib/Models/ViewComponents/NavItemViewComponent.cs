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
    public class NavItemViewComponent : ViewComponent
    {
        private readonly LibraryContext _context;
        public NavItemViewComponent(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? id)
        {
            int? userid = HttpContext.Session.GetInt32("_Id");
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userid);

            return View(user);
        }
    }
}
