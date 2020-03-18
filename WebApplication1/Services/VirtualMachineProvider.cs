using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;

namespace WebApplication1.Services
{
    public class VirtualMachineProvider : IVirtualMachineProvider
    {
        public int AvailableVolumeRAM { get; }
        public int AvailableVolumeHDD { get; }
        public int AvailableCountCores { get; }
        public List<string> Pools { get; set; }
        public List<string> Subdivisions { get; set; }
        public List<string> NameMasks { get; set; }

        private VmContext context;
        public VirtualMachineProvider(IOptions<VmOptions> options, VmContext context)
        {
            var opt = options.Value;
            AvailableCountCores = opt.CoreCount;
            AvailableVolumeHDD = opt.HDD;
            AvailableVolumeRAM = opt.RAM;
            Pools = opt.Pools;
            Subdivisions = opt.Subdivisions;
            NameMasks = opt.NameMasks;
            this.context = context;
        }

        public void GetVirtualMachine(User user, string pool, string subdivision, int ram, int hdd, int coreCount, string name)
        {
            if (!NameIsBusy(name))
            {
                context.Requests.Add(new VirtualMachineRequest
                {
                    Pool = pool,
                    User = user,
                    Subdivision = subdivision,
                    RAMVolume = ram,
                    HDDVolume = hdd,
                    CoreCount = coreCount,
                    Name = name
                });
                context.SaveChanges();
            }
        }

        public string FindPoolById(int id)
        {
            return Pools[id];
        }

        public string FindSubdivisionById(int id)
        {
            return Subdivisions[id];
        }

        public bool NameIsBusy(string name)
        {
            if (context.Requests.Count() == 0)
                return false;
            var bla = context.Requests.FirstOrDefault(t => t.Name == name);
            return bla != null;
        }
    }
}
