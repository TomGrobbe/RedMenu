using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace RedMenuShared
{
    public static class ConfigManager
    {
        public static string Version
        {
            get
            {
                return GetResourceMetadata(GetCurrentResourceName(), "version", 0) ?? "unknown";
            }
        }


        public static string ServerInfoSubtitle
        {
            get
            {
                return GetConvar("server_info_subtitle", "www.vespura.com");
            }
        }


        public static bool UnlockFullMap
        {
            get
            {
                return GetConvar("unlock_full_map", "true").ToLower() == "true";
            }
        }


        public static bool EnablePermissions
        {
            get
            {
                return GetConvar("enable_permissions", "false").ToLower() == "true";
            }
        }

        public static bool IgnoreConfigWarning
        {
            get
            {
                return GetConvar("ignore_config_warning", "false").ToLower() == "true";
            }
        }

        public static bool EnableMaxStats
        {
            get
            {
                return GetConvar("enable_max_stats", "true").ToLower() == "true";
            }
        }


    }
}
