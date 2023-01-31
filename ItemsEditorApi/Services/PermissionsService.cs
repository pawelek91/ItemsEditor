using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PermissionsService : IPermissionsService
    {
        //symulacja - to by strzelało do mikroserwisu z uprawnieniami
        public async Task<bool> UserHasRightsToModule(string apiKey, string moduleName)
            => await Task.FromResult(true);
    }
}
