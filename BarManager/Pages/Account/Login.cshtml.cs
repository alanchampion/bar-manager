using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BarManager.Pages.Account
{
    [Authorize]
    public class LoginModel : PageModel
    {
        public RedirectToPageResult OnGet()
        {
            return new RedirectToPageResult("/Index");
        }
    }
}