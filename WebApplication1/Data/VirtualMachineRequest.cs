using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Data
{
    public class VirtualMachineRequest
    {
        public int Id { get; set; }
        public int RAMVolume { get; set; }
        public int HDDVolume { get; set; }
        public int CoreCount { get; set; }
        public string Name { get; set; }

        public string Pool { get; set; }
        public string Subdivision { get; set; }
        public User User{get;set;}
    }
}
