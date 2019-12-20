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
    class MainMenu
    {
        private static Menu mainMenu = new Menu("RedMenu", "Welcome to RedMenu!");
        private static bool setupDone = false;

        private static void SetupMenu()
        {
            if (setupDone) return;
            setupDone = true;
            mainMenu.MenuTitle = GetPlayerName(PlayerId());

            MenuController.AddMenu(mainMenu);

            // Player Menu
            if (PermissionsManager.IsAllowed(Permission.PMMenu))
            {
                MenuController.AddSubmenu(mainMenu, PlayerMenu.GetMenu());
                MenuItem submenuBtn = new MenuItem("Player Menu", "All kinds of player related options.")
                {
                    RightIcon = MenuItem.Icon.ARROW_RIGHT
                };

                mainMenu.AddMenuItem(submenuBtn);
                MenuController.BindMenuItem(mainMenu, PlayerMenu.GetMenu(), submenuBtn);
            }

            // Weapons Menu
            if (PermissionsManager.IsAllowed(Permission.WMMenu))
            {
                MenuController.AddSubmenu(mainMenu, WeaponsMenu.GetMenu());
                MenuItem submenuBtn = new MenuItem("Weapons Menu", "Weapon and ammo related options.")
                {
                    RightIcon = MenuItem.Icon.ARROW_RIGHT
                };

                mainMenu.AddMenuItem(submenuBtn);
                MenuController.BindMenuItem(mainMenu, WeaponsMenu.GetMenu(), submenuBtn);
            }


            // Misc settings
            MenuController.AddSubmenu(mainMenu, MiscSettingsMenu.GetMenu());
            MenuItem miscBtn = new MenuItem("Misc Settings", "Miscellaneous settings and menu options.")
            {
                RightIcon = MenuItem.Icon.ARROW_RIGHT
            };

            mainMenu.AddMenuItem(miscBtn);
            MenuController.BindMenuItem(mainMenu, MiscSettingsMenu.GetMenu(), miscBtn);


            // Server Info
            MenuController.AddSubmenu(mainMenu, ServerInfoMenu.GetMenu());
            MenuItem serverBtn = new MenuItem("Server Info", "Information about this server.")
            {
                RightIcon = MenuItem.Icon.ARROW_RIGHT
            };

            mainMenu.AddMenuItem(serverBtn);
            MenuController.BindMenuItem(mainMenu, ServerInfoMenu.GetMenu(), serverBtn);

        }

        public static Menu GetMenu()
        {
            SetupMenu();
            return mainMenu;
        }
    }
}
