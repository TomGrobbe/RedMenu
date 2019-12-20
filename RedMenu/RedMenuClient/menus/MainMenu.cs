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

        public static void SetupMenu()
        {
            if (setupDone) return;
            setupDone = true;

            MenuController.AddMenu(mainMenu);
            MenuController.AddSubmenu(mainMenu, PlayerMenu.GetMenu());
            PlayerMenu.SetupMenu();

            MenuItem playerMenuBtn = new MenuItem("Player Menu", "All kinds of player related options.")
            {
                RightIcon = MenuItem.Icon.ARROW_RIGHT
            };

            mainMenu.AddMenuItem(playerMenuBtn);
            MenuController.BindMenuItem(mainMenu, PlayerMenu.GetMenu(), playerMenuBtn);
        }

        public static Menu GetMenu() { return mainMenu; }
    }
}
