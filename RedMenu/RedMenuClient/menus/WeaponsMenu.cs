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

            foreach (var name in data.WeaponsData.WeaponHashes)
            {
                MenuItem item = new MenuItem(name);
                menu.AddMenuItem(item);
            }

            menu.OnItemSelect += (m, item, index) =>
            {
                uint model = (uint)GetHashKey(data.WeaponsData.WeaponHashes[index]);
                GiveWeaponToPed_2(PlayerPedId(), model, 500, true, false, 0, false, 0.5f, 1.0f, 0, false, 0f, false);
            };
        }

        public static Menu GetMenu()
        {
            SetupMenu();
            return menu;
        }
    }
}
