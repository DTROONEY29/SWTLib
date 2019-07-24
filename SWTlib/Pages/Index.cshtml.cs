using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SWTlib.Models;
using LibraryData;
using LibraryData.Models;
using User = LibraryData.Models.User;

using Microsoft.AspNetCore.Http;

namespace SWTlib.Pages
{
    public class IndexModel : PageModel
    {
        private readonly LibraryContext _context;

        public const string SessionKeyName = "_Name";
        public const string SessionKeyEmail = "_Email";
        public const string SessionKeyId = "_Id";
        public const string SessionKeyRoleId = "_RoleId";
        public const string SessionKeyAvatar = "_Avatar";

        public IndexModel(LibraryContext context)
        {
            _context = context;
        }
        public string GitlabAvatar { get; set; }

        public string GitlabLogin { get; set; }

        public string GitlabName { get; set; }

        public string GitlabUrl { get; set; }

        public string GitlabEmail { get; set; }

        public string GitlabId { get; set; }





        public async Task OnGetAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                GitlabName = User.FindFirst(c => c.Type == ClaimTypes.Name)?.Value;
                GitlabId = User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                GitlabAvatar = User.FindFirst(c => c.Type == "urn:gitlab:avatar")?.Value;
                GitlabEmail = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;

                string accessToken = await HttpContext.GetTokenAsync("access_token");
                string idToken = await HttpContext.GetTokenAsync("id_token");


                int x = 0;
                Int32.TryParse(GitlabId, out x);


                var user = new User();
                var waitListEntry = new WaitListEntry();

                user.Name = GitlabName;
                user.Id = x;
                user.Email = GitlabEmail;
                user.RoleId = 3;


                waitListEntry.Name = GitlabName;
                waitListEntry.Id = x;
                waitListEntry.Email = GitlabEmail;
                waitListEntry.RoleId = 3;


                bool flag = false;
                try
                {
                    _context.Users.Add(user);
                    _context.SaveChanges();


                }
                catch (Exception ex)

                {
                    HttpContext.Session.SetString(SessionKeyName, GitlabName);
                    HttpContext.Session.SetString(SessionKeyEmail, GitlabEmail);
                    HttpContext.Session.SetInt32(SessionKeyId, x);
                    HttpContext.Session.SetInt32(SessionKeyRoleId, 3);
                    HttpContext.Session.SetString(SessionKeyAvatar, GitlabAvatar);

                    flag = true;

                }
                finally
                {
                    if (!flag)
                    {
                        try
                        {
                            _context.Users.Remove(user);
                            _context.WaitList.Add(waitListEntry);
                            _context.SaveChanges();
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
        }
    }
}

