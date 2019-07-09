using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GitLabApiClient;
using GitLabApiClient.Models.Issues.Requests;
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

        public IReadOnlyList<Repository> Repositories { get; set; }

        public async Task OnGetAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                GitlabName = User.FindFirst(c => c.Type == ClaimTypes.Name)?.Value;
                GitlabLogin = User.FindFirst(c => c.Type == "urn:gitlab:login")?.Value;
                GitlabUrl = User.FindFirst(c => c.Type == "urn:gitlab:url")?.Value;
                GitlabAvatar = User.FindFirst(c => c.Type == "urn:gitlab:avatar")?.Value;

                string accessToken = await HttpContext.GetTokenAsync("access_token");

                var gitlab = new GitLabClient("https://gitlab.rz.uni-bamberg.de", accessToken);
                await gitlab.Issues.CreateAsync(new CreateIssueRequest("1179", "TEST"));


            }
        }
    }
}