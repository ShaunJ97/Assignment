using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment.Pages.LogoutPage
{
    public class LogoutModel : PageModel
    {
        public string SessionID;
        public const string SessionKeyName3 = "sessionID";

        public IActionResult OnGet()
        {
            SessionID = HttpContext.Session.GetString(SessionKeyName3);

            if (string.IsNullOrEmpty(SessionID))
            {
                return RedirectToPage("/Login/Login");
            }
            else
            {
                HttpContext.Session.Clear();
                return Page();
            }
             
        }
    }
}
