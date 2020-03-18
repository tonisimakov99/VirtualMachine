using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1
{
    public class LoginModel : PageModel
    {
        SignInManager<User> signInManager;
        public LoginModel(SignInManager<User> signInManager)
        {
            this.signInManager = signInManager;
        }
        public class InputLogin
        {
            [Required]
            [StringLength(100, ErrorMessage = "Login length must be at least 6 characters", MinimumLength = 6)]
            public string Login { get; set; }
            [Required]
            [StringLength(100, ErrorMessage = "Password length must be at least 6 characters", MinimumLength = 6)]
            public string Password { get; set; }
        }
        [BindProperty]
        public InputLogin Input { get; set; }
        public void OnGet()
        {

        }
        public async Task<ActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(Input.Login, Input.Password, true,false);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(new User(Input.Login), true);
                    return Redirect(Url.Page("Index"));
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username and (or) password");
                    return Page();
                }
            }
            else
                return Redirect(Url.Page("Login"));
        }
    }
}
