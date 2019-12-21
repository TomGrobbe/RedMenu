using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuAPI;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using CitizenFX.Core.Native;
using RedMenuShared;
using RedMenuClient.util;

namespace RedMenuClient
{
    public class MainClient : BaseScript
    {

        public static bool PermissionsSetupDone { get; internal set; } = false;

        public MainClient()
        {
            if (!ConfigManager.EnablePermissions && !ConfigManager.IgnoreConfigWarning)
            {
                Debug.WriteLine("^3[WARNING] RedMenu is setup to ignore permissions! If this was not intended, please read the installation instructions. You can silence this warning by adding ^7setr ignore_config_warning \"true\" ^3to your server.cfg, above the ^7start RedMenu ^3line.^7");
            }

            if (GetCurrentResourceName() == "RedMenu")
            {
                DelayedConstructor();
            }
            else
            {
                Debug.WriteLine("^1[ERROR] RedMenu is not correctly installed. Please make sure that the folder is called RedMenu (case sensitive)! RedMenu will not function if it's incorrectly named.");
            }

            Function.Call((Hash)0xD4EE21B7CC7FD350, UserDefaults.MiscAlwaysShowCores); // _ALWAYS_SHOW_HORSE_CORES
            Function.Call((Hash)0x50C803A4CD5932C5, UserDefaults.MiscAlwaysShowCores); // _ALWAYS_SHOW_PLAYER_CORES

            if (ConfigManager.EnableMaxStats)
            {
                SetAttributePoints(PlayerPedId(), 0, GetMaxAttributePoints(PlayerPedId(), 0)); // health
                SetAttributePoints(PlayerPedId(), 1, GetMaxAttributePoints(PlayerPedId(), 1)); // stamina
                SetAttributePoints(PlayerPedId(), 2, GetMaxAttributePoints(PlayerPedId(), 2)); // dead eye
            }
        }


        /// <summary>
        /// Delayed constructor waits for permissions to be setup before doing anything.
        /// </summary>
        private static async void DelayedConstructor()
        {
            while (!PermissionsSetupDone)
            {
                await Delay(0);
            }

            menus.MainMenu.GetMenu();

            if (ConfigManager.UnlockFullMap)
            {
                SetMinimapHideFow(true);
            }

            //Needs more research.
            if (ConfigManager.EnableMaxStats)
            {
                SetAttributePoints(PlayerPedId(), 0, GetMaxAttributePoints(PlayerPedId(), 0)); // health
                SetAttributePoints(PlayerPedId(), 1, GetMaxAttributePoints(PlayerPedId(), 1)); // stamina
                SetAttributePoints(PlayerPedId(), 2, GetMaxAttributePoints(PlayerPedId(), 2)); // dead eye
            }
        }

    }
}
