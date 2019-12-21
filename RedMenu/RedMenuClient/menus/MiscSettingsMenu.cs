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

            MenuCheckboxItem minimapKeybind = new MenuCheckboxItem("Minimap Controls", "Holding down the Select Radar Option button will allow you to toggle the minimap on/off when this option is enabled.", UserDefaults.MiscMinimapControls);
            MenuCheckboxItem showCores = new MenuCheckboxItem("Always Show Cores", "The cores above your radar will always be displayed when this option is enabled. The game will automatically show or hide the cores if this is disabled.", UserDefaults.MiscAlwaysShowCores);

            menu.AddMenuItem(minimapKeybind);
            menu.AddMenuItem(showCores);

            menu.OnCheckboxChange += (m, item, index, _checked) =>
            {
                if (item == minimapKeybind)
                {
                    UserDefaults.MiscMinimapControls = _checked;
                }
                else if (item == showCores)
                {
                    UserDefaults.MiscAlwaysShowCores = _checked;
                    Function.Call((Hash)0xD4EE21B7CC7FD350, UserDefaults.MiscAlwaysShowCores); // _ALWAYS_SHOW_HORSE_CORES
                    Function.Call((Hash)0x50C803A4CD5932C5, UserDefaults.MiscAlwaysShowCores); // _ALWAYS_SHOW_PLAYER_CORES
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
