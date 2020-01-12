using MenuAPI;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using CitizenFX.Core.Native;

namespace RedMenuClient.menus
{
    class EntitySpawner
    {
        private static readonly Menu menu = new Menu("Entities", "Array of animals and vehicles to spawn");
        private static readonly Menu horseMenu = new Menu("Horses", "Spawn Horses");
        private static readonly Menu animalMenu = new Menu("Misc Animals", "Spawn Animals");
        private static readonly Menu vehicleMenu = new Menu("Vehicles", "Spawn Vehicles, not yet able to bind the vehicle to the horse.");


        private static bool setupDone = false;

        private static void SetupMenu()
        {
            if (setupDone) return;
            setupDone = true;

            MenuItem horseMenuBtn = new MenuItem("Horses", "Spawn a Horse") { RightIcon = MenuItem.Icon.ARROW_RIGHT };
            MenuController.AddSubmenu(menu, horseMenu);
            menu.AddMenuItem(horseMenuBtn);
            MenuController.BindMenuItem(menu, horseMenu, horseMenuBtn);
            foreach (var name in data.PedModels.HorseHashes)
            {
                MenuItem item = new MenuItem(name);
                horseMenu.AddMenuItem(item);
            }

            MenuItem animalMenuBtn = new MenuItem("Misc Animals", "Spawn an animal") { RightIcon = MenuItem.Icon.ARROW_RIGHT };
            MenuController.AddSubmenu(menu, animalMenu);
            menu.AddMenuItem(animalMenuBtn);
            MenuController.BindMenuItem(menu, animalMenu, animalMenuBtn);
            foreach (var name in data.PedModels.AnimalHashes)
            {
                MenuItem item = new MenuItem(name);
                animalMenu.AddMenuItem(item);
            }

            MenuItem vehicleMenuBtn = new MenuItem("Vehicles", "Spawn a Vehicle") { RightIcon = MenuItem.Icon.ARROW_RIGHT };
            MenuController.AddSubmenu(menu, vehicleMenu);
            menu.AddMenuItem(vehicleMenuBtn);
            MenuController.BindMenuItem(menu, vehicleMenu, vehicleMenuBtn);
            foreach (var name in data.VehicleData.vehicles)
            {
                MenuItem item = new MenuItem(name.ToString());
                vehicleMenu.AddMenuItem(item);
            }

            vehicleMenu.OnItemSelect += (m, item, index) =>
            {
                uint horse = (uint)GetHashKey(data.PedModels.HorseHashes[index]);
                uint model = data.VehicleData.vehicles[index];
                var ped = PlayerPedId();
                Vector3 pos = GetEntityCoords(ped, true, true);
                CreateVehicle(model, pos.X, pos.Y - 6f, pos.Z, 0, true, true, true, true);
            };

            horseMenu.OnItemSelect += (m, item, index) =>
            {
                uint model = (uint)GetHashKey(data.PedModels.HorseHashes[index]);
                var ped = PlayerPedId();
                Vector3 pos = GetEntityCoords(ped, true, true);
                var animal = CreatePed(model, pos.X, pos.Y - 2f, pos.Z, 0, true, true, true, true);
                Function.Call((Hash)0x77FF8D35EEC6BBC4, animal, 1, 0);
            };

            animalMenu.OnItemSelect += (m, item, index) =>
            {
                uint model = (uint)GetHashKey(data.PedModels.AnimalHashes[index]);
                var ped = PlayerPedId();
                Vector3 pos = GetEntityCoords(ped, true, true);
                var animal = CreatePed(model, pos.X, pos.Y - 2f, pos.Z, 0, true, true, true, true);
                Function.Call((Hash)0x77FF8D35EEC6BBC4, animal, 1, 0);
            };
        }

        public static Menu GetMenu()
        {
            SetupMenu();
            return menu;
        }
    }
}
