﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]/[action]")]
public class AccountController : Controller
{
    [HttpGet]
    public IActionResult Login(string returnUrl = "/")
    {
        return Challenge(new AuthenticationProperties() { RedirectUri = returnUrl });
    }
}