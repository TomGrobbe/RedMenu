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
    static class PlayerMenu
    {
        private static Menu menu = new Menu("Player Menu", "Player Related Options");
        private static bool setupDone = false;
        private static Menu appearanceMenu = new Menu("Ped Appearance", "Player Customization");

        private static void SetupMenu()
        {
            if (setupDone) return;
            setupDone = true;

            MenuItem innerHealthCoreBtn = new MenuItem("Max Health Core", "Fully restores the inner Health core.");
            MenuItem innerStaminaCoreBtn = new MenuItem("Max Stamina Core", "Fully restores the inner Stamina core.");
            MenuItem innerDeadEyeCoreBtn = new MenuItem("Max DeadEye Core", "Fully restores the inner DeadEye core.");
            // Needs more native research.
            //MenuListItem maxOuterCoresList = new MenuListItem("Restore Outer Core", new List<string>() { "All", "Health", "Stamina", "Dead Eye" }, 0, "Fully restores any or all outer cores to their max value.");
            MenuCheckboxItem godModeBox = new MenuCheckboxItem("God Mode", "Prevents you from taking damage.", UserDefaults.PlayerGodMode);
            //MenuCheckboxItem infiniteStamina = new MenuCheckboxItem("Infinite Stamina", "Run forever!", UserDefaults.PlayerInfiniteStamina);
            //MenuCheckboxItem infiniteDeadEye = new MenuCheckboxItem("Infinite DeadEye", "Useless?", UserDefaults.PlayerInfiniteDeadEye);

            MenuItem clearPedTasks = new MenuItem("Clear Ped Tasks", "Clear all ped tasks immediately, breaking free of any animation.");
            MenuItem hogtieSelf = new MenuItem("Hogtie Yourself", "Knocks you to the ground and get hogtied.");
            MenuItem cleanPed = new MenuItem("Clean Ped", "Remove all dirt and other decals from the ped.");

            MenuDynamicListItem playerOutfit = new MenuDynamicListItem("Select Outfit", "0", new MenuDynamicListItem.ChangeItemCallback((item, left) =>
            {
                if (int.TryParse(item.CurrentItem, out int val))
                {
                    int newVal = val;
                    if (left)
                    {
                        newVal--;
                        if (newVal < 0)
                        {
                            newVal = 0;
                        }
                    }
                    else
                    {
                        newVal++;
                    }
                    SetPedOutfitPreset(PlayerPedId(), newVal, 0);
                    return newVal.ToString();
                }
                return "0";
            }), "Select a predefined outfit for this ped. Outfits are made by Rockstar. Note the selected value can go up indefinitely because we don't know how to check for the max amount of outfits yet, so more native research is needed.");



            if (PermissionsManager.IsAllowed(Permission.PMRestoreHealth))
            {
                menu.AddMenuItem(innerHealthCoreBtn);
            }
            if (PermissionsManager.IsAllowed(Permission.PMRestoreStamina))
            {
                menu.AddMenuItem(innerStaminaCoreBtn);
            }
            if (PermissionsManager.IsAllowed(Permission.PMRestoreDeadEye))
            {
                menu.AddMenuItem(innerDeadEyeCoreBtn);
            }
            if (PermissionsManager.IsAllowed(Permission.PMMaxOuterCores))
            {
                //menu.AddMenuItem(maxOuterCoresList);
            }
            if (PermissionsManager.IsAllowed(Permission.PMGodMode))
            {
                menu.AddMenuItem(godModeBox);
                if (UserDefaults.PlayerGodMode)
                {
                    SetEntityInvincible(PlayerPedId(), true);
                }
            }
            if (PermissionsManager.IsAllowed(Permission.PMInfiniteStamina))
            {
                // need more research
            }
            if (PermissionsManager.IsAllowed(Permission.PMInfiniteDeadEye))
            {
                // need more research
            }
            if (PermissionsManager.IsAllowed(Permission.PMClearTasks))
            {
                menu.AddMenuItem(clearPedTasks);
            }
            if (PermissionsManager.IsAllowed(Permission.PMHogtieSelf))
            {
                menu.AddMenuItem(hogtieSelf);
            }
            if (PermissionsManager.IsAllowed(Permission.PMCleanPed))
            {
                menu.AddMenuItem(cleanPed);
            }
            if (PermissionsManager.IsAllowed(Permission.PMSelectPlayerModel) || PermissionsManager.IsAllowed(Permission.PMSelectOutfit))
            {
                MenuItem appearanceMenuBtn = new MenuItem("Player Appearance", "Player appearance options.") { RightIcon = MenuItem.Icon.ARROW_RIGHT };
                MenuController.AddSubmenu(menu, appearanceMenu);
                menu.AddMenuItem(appearanceMenuBtn);
                MenuController.BindMenuItem(menu, appearanceMenu, appearanceMenuBtn);

                if (PermissionsManager.IsAllowed(Permission.PMSelectPlayerModel))
                {
                    List<string> males = new List<string>();
                    List<string> females = new List<string>();
                    List<string> cutscene = new List<string>();
                    List<string> other = new List<string>();
                    MenuListItem malePeds = new MenuListItem("Males", males, 0, "Select a male ped model.");
                    MenuListItem femalePeds = new MenuListItem("Females", females, 0, "Select a female ped model.");
                    MenuListItem cutscenePeds = new MenuListItem("Cutscene", cutscene, 0, "Select a cutscene ped model.");
                    MenuListItem otherPeds = new MenuListItem("Other", other, 0, "Select a ped model.");
                    for (int i = 0; i < data.PedModels.MalePedHashes.Count(); i++)
                    {
                        males.Add($"{data.PedModels.MalePedHashes[i]} ({i + 1}/{data.PedModels.MalePedHashes.Count()})");
                    }
                    for (int i = 0; i < data.PedModels.FemalePedHashes.Count(); i++)
                    {
                        females.Add($"{data.PedModels.FemalePedHashes[i]} ({i + 1}/{data.PedModels.FemalePedHashes.Count()})");
                    }
                    for (int i = 0; i < data.PedModels.CutscenePedHashes.Count(); i++)
                    {
                        cutscene.Add($"{data.PedModels.CutscenePedHashes[i]} ({i + 1}/{data.PedModels.CutscenePedHashes.Count()})");
                    }
                    for (int i = 0; i < data.PedModels.OtherPedHashes.Count(); i++)
                    {
                        other.Add($"{data.PedModels.OtherPedHashes[i]} ({i + 1}/{data.PedModels.OtherPedHashes.Count()})");
                    }

                    appearanceMenu.AddMenuItem(malePeds);
                    appearanceMenu.AddMenuItem(femalePeds);
                    appearanceMenu.AddMenuItem(otherPeds);
                    appearanceMenu.AddMenuItem(cutscenePeds);

                    appearanceMenu.OnListItemSelect += async (m, item, listIndex, itemIndex) =>
                    {
                        uint model = 0;
                        if (item == malePeds)
                        {
                            model = (uint)GetHashKey(data.PedModels.MalePedHashes[listIndex]);
                        }
                        else if (item == femalePeds)
                        {
                            model = (uint)GetHashKey(data.PedModels.FemalePedHashes[listIndex]);
                        }
                        else if (item == cutscenePeds)
                        {
                            model = (uint)GetHashKey(data.PedModels.CutscenePedHashes[listIndex]);
                        }
                        else if (item == otherPeds)
                        {
                            model = (uint)GetHashKey(data.PedModels.OtherPedHashes[listIndex]);
                        }

                        if (IsModelInCdimage(model))
                        {
                            RequestModel(model, false);
                            while (!HasModelLoaded(model))
                            {
                                await BaseScript.Delay(0);
                            }
                            SetPlayerModel(PlayerId(), (int)model, 0);
                            SetPedOutfitPreset(PlayerPedId(), 0, 0);
                            SetModelAsNoLongerNeeded(model);
                            playerOutfit.CurrentItem = "0";
                        }
                        else
                        {
                            Debug.WriteLine($"^1[ERROR] This ped model is not present in the game files {model}.^7");
                        }
                    };
                }

                if (PermissionsManager.IsAllowed(Permission.PMSelectOutfit))
                {
                    appearanceMenu.AddMenuItem(playerOutfit);
                }
            }

            menu.OnDynamicListItemSelect += (m, item, currentItem) =>
            {
                if (item == playerOutfit)
                {
                    if (int.TryParse(currentItem, out int val))
                    {
                        SetPedOutfitPreset(PlayerPedId(), val, 0);
                    }
                }
            };

            menu.OnCheckboxChange += (m, item, index, _checked) =>
            {
                if (item == godModeBox)
                {
                    UserDefaults.PlayerGodMode = _checked;
                    SetEntityInvincible(PlayerPedId(), _checked);
                }
            };

            menu.OnItemSelect += (m, item, index) =>
            {
                if (item == innerHealthCoreBtn)
                {
                    Function.Call<int>((Hash)0xC6258F41D86676E0, PlayerPedId(), (int)0, (int)100);
                }
                else if (item == innerStaminaCoreBtn)
                {
                    Function.Call<int>((Hash)0xC6258F41D86676E0, PlayerPedId(), (int)1, (int)100);
                }
                else if (item == innerDeadEyeCoreBtn)
                {
                    Function.Call<int>((Hash)0xC6258F41D86676E0, PlayerPedId(), (int)2, (int)100);
                }
                else if (item == clearPedTasks)
                {
                    Function.Call(Hash.CLEAR_PED_TASKS_IMMEDIATELY, PlayerPedId(), true, false);
                }
                else if (item == hogtieSelf)
                {
                    Function.Call(Hash.TASK_KNOCKED_OUT_AND_HOGTIED, PlayerPedId(), false, false);
                }
                else if (item == cleanPed)
                {
                    ClearPedEnvDirt(PlayerPedId());
                }
            };

            //menu.OnListItemSelect += (m, item, listIndex, itemIndex) =>
            //{
            //    if (item == maxOuterCoresList)
            //    {
            //        switch (listIndex)
            //        {
            //            // 0x3FC4C027FD0936F4 = Gets the currently set max value for the ped's outer cores, this increases when the ped gains attribute points but will never exceed the absolute max points value. Lowest default for every core is 258.
            //            // GetMaxAttributePoints = Gets the absolute max points this ped could ever have if their attribute points were maxed out. Default for every core is 1100.
            //            case 0: // all
            //                SetAttributePoints(PlayerPedId(), 0, Function.Call<int>((Hash)0x3FC4C027FD0936F4, PlayerPedId(), 0));
            //                SetAttributePoints(PlayerPedId(), 1, Function.Call<int>((Hash)0x3FC4C027FD0936F4, PlayerPedId(), 1));
            //                SetAttributePoints(PlayerPedId(), 2, Function.Call<int>((Hash)0x3FC4C027FD0936F4, PlayerPedId(), 2));
            //                break;
            //            case 1: // health
            //                SetAttributePoints(PlayerPedId(), 0, Function.Call<int>((Hash)0x3FC4C027FD0936F4, PlayerPedId(), 0));
            //                break;
            //            case 2: // stamina
            //                SetAttributePoints(PlayerPedId(), 1, Function.Call<int>((Hash)0x3FC4C027FD0936F4, PlayerPedId(), 1));
            //                break;
            //            case 3: // dead eye
            //                SetAttributePoints(PlayerPedId(), 2, Function.Call<int>((Hash)0x3FC4C027FD0936F4, PlayerPedId(), 2));
            //                break;
            //            default: // invalid index
            //                break;
            //        }
            //    }
            //};
        }

        public static Menu GetMenu()
        {
            SetupMenu();
            return menu;
        }

    }
}
