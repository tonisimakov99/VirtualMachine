using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;

namespace WebApplication1.Services
{
    public interface IVirtualMachineProvider
    {
        int AvailableVolumeRAM { get; }
        int AvailableVolumeHDD { get; }
        int AvailableCountCores { get; }
        List<string> Pools { get; set; }
        List<string> Subdivisions { get; set; }

        List<string> NameMasks { get; set;}
        void GetVirtualMachine(User user, string pool, string subdivision, int ram, int hdd, int coreCount, string name);
        string FindPoolById(int id);
        string FindSubdivisionById(int id);

        bool NameIsBusy(string name);
    }
}
