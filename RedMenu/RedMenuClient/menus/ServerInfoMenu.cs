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
    class ServerInfoMenu
    {
        private static Menu menu = new Menu("Server Info", $"{ConfigManager.ServerInfoSubtitle}");
        private static bool setupDone = false;

        private static void SetupMenu()
        {
            if (setupDone) return;
            setupDone = true;

            MenuItem item = new MenuItem("Soon.", "This menu is coming soon. Server owners can configure the information displayed here.")
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
