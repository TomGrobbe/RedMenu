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
    class WeaponsMenu
    {
        private static Menu menu = new Menu("Weapons Menu", $"Weapon & Ammo Options");
        private static bool setupDone = false;

        private static void SetupMenu()
        {
            if (setupDone) return;
            setupDone = true;

            MenuItem item = new MenuItem("Soon.", "This menu is coming soon.")
            {
                Enabled = false,
                LeftIcon = MenuItem.Icon.LOCK
            };

            menu.AddMenuItem(item);
        }

        public static Menu GetMenu()
        {
            SetupMenu();
            return menu;
        }
    }
}
