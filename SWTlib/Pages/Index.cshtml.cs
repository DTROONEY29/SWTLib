using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GitLabApiClient;
using GitLabApiClient.Models.Issues.Responses;
using GitLabApiClient.Models.Users.Responses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SWTlib.Models;
using LibraryData;
using LibraryData.Models;
using User = LibraryData.Models.User;

namespace SWTlib.Pages
{
    public class IndexModel : PageModel
    {
        private readonly LibraryContext _context;

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

                var client = new GitLabClient("https://gitlab.rz.uni-bamberg.de/", accessToken);

                int x = 0;
                Int32.TryParse(GitlabId, out x);

                Console.WriteLine(GitlabName);                
                Console.WriteLine(GitlabId);
                Console.WriteLine(GitlabEmail);
                Console.WriteLine("----------");
                var user = new User();
                user.Name = GitlabName;
                user.Id = x;
                user.Email = GitlabEmail;
                _context.Users.Add(user);
                _context.SaveChanges();



            }
        }
    }
}