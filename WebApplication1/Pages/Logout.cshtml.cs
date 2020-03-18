using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1
{
    public class LogoutModel : PageModel
    {
        SignInManager<User> signInManager;
        public LogoutModel(SignInManager<User> signInManager)
        {
            this.signInManager = signInManager;
        }
        public async Task<ActionResult> OnGet()
        {
            await signInManager.SignOutAsync();
            return Redirect(Url.Page("Login"));
        }
    }
}