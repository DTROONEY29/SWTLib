using LibraryData;
using LibraryData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWTlib.Models.ViewComponents
{
    public class WishListViewComponent : ViewComponent
    {
        private readonly LibraryContext _context;
        public WishListViewComponent(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? id)
        {

            var wishList = await _context.WishList
                .OrderBy(x => x.Rating)
                .ToListAsync();

            return View(wishList);
        }

    }
}
