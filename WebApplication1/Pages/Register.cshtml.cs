using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1
{
    public class RegisterModel : PageModel
    {
        SignInManager<User> signInManager;
        public RegisterModel(SignInManager<User> signInManager)
        {
            this.signInManager = signInManager;
        }
        public class InputRegister
        {
            [Required]
            [StringLength(100,ErrorMessage = "Login length must be at least 6 characters",MinimumLength = 6)]
            public string Login { get; set; }
            [Required]
            [StringLength(100, ErrorMessage = "Password length must be at least 6 characters", MinimumLength = 6)]
            public string Password { get; set; }
            [Required]
            [Compare("Password",ErrorMessage = "Passwords do not match")]
            public string ConfirmPassword { get; set; }
        }
        [BindProperty]
        public InputRegister Input { get; set; }

        public IEnumerable<IdentityError> Errors = new List<IdentityError>();
        public void OnGet()
        {
            
        }
        public async Task<ActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.UserManager.CreateAsync(new User(Input.Login), Input.Password);

                if(result.Succeeded)
                {
                    await signInManager.SignInAsync(new User(Input.Login), true);
                    return RedirectToPage("/Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                    return Page();
                }
            }
            else
                return Redirect(Url.Page("Register"));
        }
    }
}
