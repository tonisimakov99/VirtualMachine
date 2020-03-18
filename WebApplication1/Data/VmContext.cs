using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Data
{
    public class VmContext : IdentityDbContext<User>
    {
        public DbSet<VirtualMachineRequest> Requests { get; set; }
        public VmContext(DbContextOptions options):base(options)
        {
            Database.EnsureCreated();
        }
    }
}
