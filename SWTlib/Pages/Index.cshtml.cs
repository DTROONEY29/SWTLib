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

namespace SWTlib.Pages
{
    public class IndexModel : PageModel
    {
        public string GitlabAvatar { get; set; }

        public string GitlabLogin { get; set; }

        public string GitlabName { get; set; }

        public string GitlabUrl { get; set; }

        public string GitlabEmail { get; set; }

        public string Id { get; set; }




        public async Task OnGetAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                GitlabName = User.FindFirst(c => c.Type == ClaimTypes.Name)?.Value;
                GitlabLogin = User.FindFirst(c => c.Type == "urn:gitlab:login")?.Value;
                GitlabUrl = User.FindFirst(c => c.Type == "urn:gitlab:url")?.Value;
                GitlabAvatar = User.FindFirst(c => c.Type == "urn:gitlab:avatar")?.Value;
                GitlabEmail = User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                string accessToken = await HttpContext.GetTokenAsync("access_token");
                string idToken = await HttpContext.GetTokenAsync("id_token");

                var client = new GitLabClient("https://gitlab.rz.uni-bamberg.de/", accessToken);              



                Console.WriteLine(GitlabName);
                
                Console.WriteLine(Id);

                Console.WriteLine(client.HostUrl);

            }
        }
    }
}