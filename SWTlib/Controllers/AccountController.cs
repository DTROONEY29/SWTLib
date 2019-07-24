using LibraryData;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class AccountController : Controller
{
    private readonly LibraryContext _context;
    public AccountController(LibraryContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("[controller]/[action]")]
    public IActionResult Login(string returnUrl = "/")
    {
        return Challenge(new AuthenticationProperties() { RedirectUri = returnUrl });
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return Redirect("https://gitlab.rz.uni-bamberg.de/users/sign_out");
    }

}

