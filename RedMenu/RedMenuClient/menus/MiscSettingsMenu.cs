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

namespace RedMenuClient.menus
{
    class MiscSettingsMenu
    {
        private static Menu menu = new Menu("Misc Settings", $"RedMenu Version {ConfigManager.Version}");
        private static bool setupDone = false;

        private static void SetupMenu()
        {
            if (setupDone) return;
            setupDone = true;

            MenuCheckboxItem minimapKeybind = new MenuCheckboxItem("Minimap Controls", "When enabled, holding down the Select Radar Option button will allow you to toggle the minimap on/off.", UserDefaults.MiscMinimapControls);

            menu.AddMenuItem(minimapKeybind);

            menu.OnCheckboxChange += (m, item, index, _checked) =>
            {
                if (item == minimapKeybind)
                {
                    UserDefaults.MiscMinimapControls = _checked;
                }
            };
            
        }

        public static Menu GetMenu()
        {
            SetupMenu();
            return menu;
        }
    }
}
