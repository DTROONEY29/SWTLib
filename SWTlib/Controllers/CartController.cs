using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryData;
using LibraryData.Models;
using Microsoft.AspNetCore.Mvc;
using SWTlib.Models.Helpers;

namespace SWTlib.Controllers
{
    // TO BE DONE
    public class CartController : Controller
    {
        private readonly LibraryContext _context;
        public CartController(LibraryContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<Rental>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            return View();
        }

        [Route("Rent /{id}")]
        public IActionResult Rent(int id)
        {
            if (SessionHelper.GetObjectFromJson<List<Rental>>(HttpContext.Session, "cart") == null)
            {
                List<Rental> cart = new List<Rental>();
                cart.Add(new Rental { Book = _context.Books.Find(id)});
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<Rental> cart = SessionHelper.GetObjectFromJson<List<Rental>>(HttpContext.Session, "cart");
                                
                cart.Add(new Rental { Book = _context.Books.Find(id) });
  
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }    
    }
}