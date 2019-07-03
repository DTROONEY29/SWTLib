using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SWTlib.Controllers
{
    public class KeywordController : Controller
    {
        // GET: Keyword
        public ActionResult Index()
        {
            return View();
        }

        // GET: Keyword/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Keyword/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Keyword/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Keyword/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Keyword/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Keyword/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Keyword/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}