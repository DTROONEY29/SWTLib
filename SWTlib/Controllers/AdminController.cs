using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using LibraryData;
using LibraryData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace SWTlib.Controllers

{
    public class AdminController : Controller
    {
        private readonly LibraryContext _context;
        public AdminController(LibraryContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var entries = _context.WaitList.ToList();
            RoleDropDownList();
            
            return View(entries);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Approve(int userid, int roleid)
        {
            var entry = _context.WaitList.FirstOrDefault(i => i.Id == userid);

           if (ModelState.IsValid)
            {

            
                User user = new User
                {
                    Name = entry.Name,
                    Id = entry.Id,
                    Email = entry.Email,
                    RoleId = roleid
                };


                _context.Users.Add(user);
                _context.WaitList.Remove(entry);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        private void RoleDropDownList(object selectedRole = null)
        {
            var rolesQuery = from l in _context.Roles
                                 orderby l.RoleName
                                 select l;
            ViewBag.RoleName = new SelectList(rolesQuery.AsNoTracking(), "RoleId", "RoleName", selectedRole);
        }
    }
}