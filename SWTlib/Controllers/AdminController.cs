using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SWTlib.Controllers

{
    public class AdminController : Controller
    {
        public IActionResult WaitList()
        {
            return View();
        }


    }
}