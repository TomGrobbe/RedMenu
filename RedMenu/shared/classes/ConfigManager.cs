using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace RedMenuServer
{
    public static class ConfigManager
    {
        public static string Version
        {
            get
            {
                return GetConvar("rm_version", "1.0.0");
            }
#if SERVER
            set
            {
                SetConvarReplicated("rm_version", value);
            }
#endif
        }
    }
}
