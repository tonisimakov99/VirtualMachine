using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Data;
using WebApplication1.Services;

namespace WebApplication1.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        SignInManager<User> signInManager;
        public IVirtualMachineProvider provider;

        public SelectList Pools{ get; set; }
        public SelectList Subdivisions { get; set; }
        public List<string> Masks;

        public IndexModel(SignInManager<User> signInManager, IVirtualMachineProvider provider)
        {
            this.signInManager = signInManager;
            this.provider = provider;
            Pools = new SelectList(provider.Pools);
            Subdivisions = new SelectList(provider.Subdivisions);
            Masks = provider.NameMasks;
        }
        public class InputRequest
        { 
            public string Pool { get; set; }
            
            public string Subdivision { get; set; }

            [MaskValidation(new string[] { "XXXXXXXXXX", "XXXXXXX-#", "XXXX" })]
            public string Name { get; set; }
            public int RAMVolume { get; set; }
            public int HDDVolume { get; set; }
            public int CoreCount { get; set; }
        }
        [BindProperty]
        public InputRequest Input { get; set; }

        public bool IsAlert = false;
        public void OnGet()
        {

       
        }
        
        public async Task<ActionResult> OnPostAsync()
        {
            
            if (ModelState.IsValid)
            {
                var user = await signInManager.UserManager.FindByNameAsync(User.Identity.Name);
                if (!provider.NameIsBusy(Input.Name))
                {
                    provider.GetVirtualMachine(
                        user,
                        Input.Pool,
                        Input.Subdivision,
                        Input.RAMVolume,
                        Input.HDDVolume,
                        Input.CoreCount,
                        Input.Name);

                    IsAlert = true;
                }
                else 
                    ModelState.AddModelError("", "Virtual machine name is busy");
            }
            return null;  
        }
    }
}
