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

            MenuItem healBtn = new MenuItem("Max Health Core", "Restore Health Core");
            MenuItem restoreStaminaBtn = new MenuItem("Max Stamina Core", "Restore Stamina Core");
            MenuItem restoreEyeBtn = new MenuItem("Max DeadEye Core", "Restore DeadEye Core");

            menu.AddMenuItem(healBtn);
            menu.AddMenuItem(restoreStaminaBtn);
            menu.AddMenuItem(restoreEyeBtn);

            menu.OnItemSelect += (_menu, _item, _index) =>
            {
                if (_item == healBtn)
                {
                    Function.Call<int>((Hash)0xC6258F41D86676E0, PlayerPedId(), (int)0, (int)100); 
                }
                else if (_item == restoreStaminaBtn)
                {
                    Function.Call<int>((Hash)0xC6258F41D86676E0, PlayerPedId(), (int)1, (int)100);
                }
                else if (_item == restoreEyeBtn)
                {
                    Function.Call<int>((Hash)0xC6258F41D86676E0, PlayerPedId(), (int)2, (int)100);
                }
                
                
            };
        }
        
        public static Menu GetMenu() { return menu; }

    }
}
