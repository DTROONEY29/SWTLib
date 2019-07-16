using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using LibraryData;
using LibraryData.Models;
using Microsoft.AspNetCore.Mvc;

namespace SWTlib.Controllers
{
    public class WishListController : Controller
    {
        private readonly LibraryContext _context;
        public WishListController(LibraryContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var wishes = _context.WishList.ToList();
            ViewBag.Wishlsit = wishes;

            return View();
        }

        [Route("component/[action]")]
        public IActionResult WishList() => ViewComponent("WishList");

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Title,ISBN,AuthorName,Rating")] WishListEntry wish)
        {
            var highestRating = _context.WishList.Max(m => m.Rating);

            if (ModelState.IsValid)
            {
                wish.Rating = highestRating + 1;
                _context.WishList.Add(wish);
                _context.SaveChanges();
                
                return RedirectToAction("Index", "WishList");
            }
            return View(wish);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var wish = _context.WishList.Find(id);

            var wishes = _context.WishList.Where(i => i.Rating > wish.Rating).ToList();
            foreach (var w in wishes)
            {
                w.Rating -= 1;
            }

            if (ModelState.IsValid)
            {
                _context.WishList.Remove(wish);
                _context.SaveChanges();

                return RedirectToAction("Index", "WishList");
            }
            return View(wish);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ArrowUp(int id)
        {
            
            var wishListRating = _context.WishList.Find(id);
            if (wishListRating == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var oldRatingA = wishListRating.Rating;
            var newRatingA = oldRatingA - 1;

            var affectedRating = _context.WishList.FirstOrDefault(i => i.Rating == newRatingA);
            if (affectedRating == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var oldRatingB = affectedRating.Rating;
            var newRatingB = oldRatingB + 1;

            try
            {
                wishListRating.Rating = newRatingA;
                affectedRating.Rating = newRatingB;
                _context.SaveChanges();
                return RedirectToAction("Index", "WishList");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ArrowDown(int id)
        {            
            
            var wishListRating = _context.WishList.Find(id);
            if (wishListRating == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var oldRatingA = wishListRating.Rating;
            var newRatingA = oldRatingA + 1;

            var affectedRating = _context.WishList.FirstOrDefault(i => i.Rating == newRatingA);
            if (affectedRating == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var oldRatingB = affectedRating.Rating;
            var newRatingB = oldRatingB - 1;


            try
            {
                wishListRating.Rating = newRatingA;
                affectedRating.Rating = newRatingB;
                _context.SaveChanges();
                return RedirectToAction("Index", "WishList");
            }
            catch
            {
                return View();
            }
        }

    }
}