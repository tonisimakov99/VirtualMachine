using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;

namespace WebApplication1.Services
{
    public class VmOptions
    {
        public int RAM { get; set; }
        public int HDD { get; set; }
        public int CoreCount { get; set; }
        public List<string> NameMasks { get; set; }
        public List<string> Pools { get; internal set; }
        public List<string> Subdivisions { get; internal set; }
    }
}
