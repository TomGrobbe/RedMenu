using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuAPI;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using CitizenFX.Core.Native;

namespace RedMenuClient.menus
{
    static class PlayerMenu
    {
        private static Menu menu = new Menu("Player Menu", "Player Related Options");
        private static bool setupDone = false;

        public static void SetupMenu()
        {
            if (setupDone) return;
            setupDone = true;

            MenuItem healBtn = new MenuItem("Max Health", "Restore full Health inner core.");
            MenuItem restoreStaminaBtn = new MenuItem("Max Stamina", "Restore full Stamina inner core.");
            MenuItem restoreEyeBtn = new MenuItem("Max Dead Eye", "Restore full Dead Eye inner core.");

            menu.AddMenuItem(healBtn);
            menu.AddMenuItem(restoreStaminaBtn);
            menu.AddMenuItem(restoreEyeBtn);
            menu.OnItemSelect += (_menu, _item, _index) =>
            {
                int index = (int)_index;
                switch(index)
                {
                    case 0:
                        Function.Call<int>((Hash)0xC6258F41D86676E0, PlayerPedId(), (int)0, (int)100);
                        break;
                    case 1:
                        Function.Call<int>((Hash)0xC6258F41D86676E0, PlayerPedId(), (int)1, (int)100);
                        break;
                    case 2:
                        Function.Call<int>((Hash)0xC6258F41D86676E0, PlayerPedId(), (int)2, (int)100);
                        break;
                }
                
            };
        }


        public static Menu GetMenu() { return menu; }


    }
}
