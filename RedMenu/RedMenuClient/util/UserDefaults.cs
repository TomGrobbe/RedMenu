using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using RedMenuShared;
using CitizenFX.Core.Native;

namespace RedMenuClient.util
{
    public static class UserDefaults
    {

        /// <summary>
        /// Default: false
        /// </summary>
        public static bool PlayerGodMode
        {
            get
            {
                if (StorageManager.TryGet("PlayerGodMode", out bool val))
                {
                    return val;
                }
                PlayerGodMode = false;
                return false;
            }
            set
            {
                StorageManager.Save("PlayerGodMode", value, true);
            }
        }

        /// <summary>
        /// Default: false
        /// </summary>
        public static bool PlayerInfiniteStamina
        {
            get
            {
                if (StorageManager.TryGet("PlayerInfiniteStamina", out bool val))
                {
                    return val;
                }
                PlayerInfiniteStamina = false;
                return false;
            }
            set
            {
                StorageManager.Save("PlayerInfiniteStamina", value, true);
            }
        }

        /// <summary>
        /// Default: false
        /// </summary>
        public static bool PlayerInfiniteDeadEye
        {
            get
            {
                if (StorageManager.TryGet("PlayerInfiniteDeadEye", out bool val))
                {
                    return val;
                }
                PlayerInfiniteDeadEye = false;
                return false;
            }
            set
            {
                StorageManager.Save("PlayerInfiniteDeadEye", value, true);
            }
        }

        /// <summary>
        /// Default: true
        /// </summary>
        public static bool MiscMinimapControls
        {
            get
            {
                if (StorageManager.TryGet("MiscMinimapControls", out bool val))
                {
                    return val;
                }
                MiscMinimapControls = true;
                return true;
            }
            set
            {
                StorageManager.Save("MiscMinimapControls", value, true);
            }
        }


        /// <summary>
        /// Default: false
        /// </summary>
        public static bool MiscAlwaysShowCores
        {
            get
            {
                if (StorageManager.TryGet("MiscAlwaysShowCores", out bool val))
                {
                    return val;
                }
                MiscAlwaysShowCores = false;
                return true;
            }
            set
            {
                StorageManager.Save("MiscAlwaysShowCores", value, true);
            }
        }

    }
}
