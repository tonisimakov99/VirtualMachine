using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;

namespace WebApplication1
{
    public class User : IdentityUser
    {
        public User(string userName):base(userName)
        {

        }
        public ICollection<VirtualMachineRequest> Requests { get; set; }

    }
}
