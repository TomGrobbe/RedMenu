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
            MenuListItem maxOuterCoresList = new MenuListItem("Restore Outer Core", new List<string>() { "All", "Health", "Stamina", "Dead Eye" }, 0, "Fully restores any or all outer cores to their max value.");
            MenuCheckboxItem godModeBox = new MenuCheckboxItem("God Mode", "Prevents you from taking damage.", UserDefaults.PlayerGodMode);
            MenuCheckboxItem infiniteStamina = new MenuCheckboxItem("Infinite Stamina", "Run forever!", UserDefaults.PlayerInfiniteStamina);
            MenuCheckboxItem infiniteDeadEye = new MenuCheckboxItem("Infinite DeadEye", "Useless?", UserDefaults.PlayerInfiniteDeadEye);

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
                menu.AddMenuItem(maxOuterCoresList);
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
                menu.AddMenuItem(infiniteStamina);
            }
            if (PermissionsManager.IsAllowed(Permission.PMInfiniteDeadEye))
            {
                menu.AddMenuItem(infiniteDeadEye);
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

                if (PermissionsManager.IsAllowed(Permission.PMCustomizeMpPeds))
                {
                    MenuItem femaleCustom = new MenuItem("MP Female Customization", "Customize your MP female ped.") { RightIcon = MenuItem.Icon.ARROW_RIGHT };
                    MenuItem maleCustom = new MenuItem("MP Male Customization", "Customize your MP male ped.") { RightIcon = MenuItem.Icon.ARROW_RIGHT };

                    Menu femaleCustomMenu = new Menu("Customization", "MP Female Customization");
                    Menu maleCustomMenu = new Menu("Customization", "MP Male Customization");

                    MenuController.AddSubmenu(appearanceMenu, femaleCustomMenu);
                    MenuController.AddSubmenu(appearanceMenu, maleCustomMenu);

                    #region female
                    {
                        List<string> spurs = new List<string>();
                        List<string> pants = new List<string>();
                        List<string> shirts = new List<string>();
                        List<string> chaps = new List<string>();
                        List<string> faces = new List<string>();
                        List<string> ponchos = new List<string>();
                        List<string> badges = new List<string>();
                        List<string> vests = new List<string>();
                        List<string> legArmor = new List<string>();
                        List<string> glasses = new List<string>();
                        List<string> bandanas = new List<string>();
                        List<string> coats = new List<string>();
                        List<string> bodyArmor = new List<string>();
                        List<string> masks = new List<string>();
                        List<string> boots = new List<string>();
                        List<string> buckles = new List<string>();
                        List<string> rings = new List<string>();
                        List<string> neckwear = new List<string>();
                        List<string> wristbands = new List<string>();
                        List<string> feetStyle = new List<string>();
                        List<string> belts = new List<string>();
                        List<string> hair = new List<string>();
                        List<string> suspenders = new List<string>();
                        List<string> gauntlets = new List<string>();
                        List<string> bags = new List<string>();
                        List<string> unk1 = new List<string>();
                        List<string> hats = new List<string>();
                        List<string> gunBelts = new List<string>();
                        List<string> skirts = new List<string>();
                        List<string> belts2 = new List<string>();
                        List<string> ponchos2 = new List<string>();
                        List<string> bodyStyle = new List<string>();
                        List<string> offHandHolsters = new List<string>();
                        List<string> coats2 = new List<string>();
                        List<string> unk2 = new List<string>();
                        List<string> gloves = new List<string>();
                        List<string> gunBeltAccessory = new List<string>();
                        List<string> rings2 = new List<string>();
                        //List<string> beard = new List<string>();
                        List<string> buckles2 = new List<string>();
                        foreach (var k in data.FemaleCustomization.spurs) { spurs.Add($"({data.FemaleCustomization.spurs.IndexOf(k) + 1}/{data.FemaleCustomization.spurs.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.FemaleCustomization.pants) { pants.Add($"({data.FemaleCustomization.pants.IndexOf(k) + 1}/{data.FemaleCustomization.pants.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.FemaleCustomization.shirts) { shirts.Add($"({data.FemaleCustomization.shirts.IndexOf(k) + 1}/{data.FemaleCustomization.shirts.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.FemaleCustomization.chaps) { chaps.Add($"({data.FemaleCustomization.chaps.IndexOf(k) + 1}/{data.FemaleCustomization.chaps.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.FemaleCustomization.faces) { faces.Add($"({data.FemaleCustomization.faces.IndexOf(k) + 1}/{data.FemaleCustomization.faces.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.FemaleCustomization.ponchos) { ponchos.Add($"({data.FemaleCustomization.ponchos.IndexOf(k) + 1}/{data.FemaleCustomization.ponchos.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.FemaleCustomization.badges) { badges.Add($"({data.FemaleCustomization.badges.IndexOf(k) + 1}/{data.FemaleCustomization.badges.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.FemaleCustomization.vests) { vests.Add($"({data.FemaleCustomization.vests.IndexOf(k) + 1}/{data.FemaleCustomization.vests.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.FemaleCustomization.legArmor) { legArmor.Add($"({data.FemaleCustomization.legArmor.IndexOf(k) + 1}/{data.FemaleCustomization.legArmor.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.FemaleCustomization.glasses) { glasses.Add($"({data.FemaleCustomization.glasses.IndexOf(k) + 1}/{data.FemaleCustomization.glasses.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.FemaleCustomization.bandanas) { bandanas.Add($"({data.FemaleCustomization.bandanas.IndexOf(k) + 1}/{data.FemaleCustomization.bandanas.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.FemaleCustomization.coats) { coats.Add($"({data.FemaleCustomization.coats.IndexOf(k) + 1}/{data.FemaleCustomization.coats.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.FemaleCustomization.bodyArmor) { bodyArmor.Add($"({data.FemaleCustomization.bodyArmor.IndexOf(k) + 1}/{data.FemaleCustomization.bodyArmor.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.FemaleCustomization.masks) { masks.Add($"({data.FemaleCustomization.masks.IndexOf(k) + 1}/{data.FemaleCustomization.masks.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.FemaleCustomization.boots) { boots.Add($"({data.FemaleCustomization.boots.IndexOf(k) + 1}/{data.FemaleCustomization.boots.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.FemaleCustomization.buckles) { buckles.Add($"({data.FemaleCustomization.buckles.IndexOf(k) + 1}/{data.FemaleCustomization.buckles.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.FemaleCustomization.rings) { rings.Add($"({data.FemaleCustomization.rings.IndexOf(k) + 1}/{data.FemaleCustomization.rings.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.FemaleCustomization.neckwear) { neckwear.Add($"({data.FemaleCustomization.neckwear.IndexOf(k) + 1}/{data.FemaleCustomization.neckwear.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.FemaleCustomization.wristbands) { wristbands.Add($"({data.FemaleCustomization.wristbands.IndexOf(k) + 1}/{data.FemaleCustomization.wristbands.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.FemaleCustomization.feetStyle) { feetStyle.Add($"({data.FemaleCustomization.feetStyle.IndexOf(k) + 1}/{data.FemaleCustomization.feetStyle.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.FemaleCustomization.belts) { belts.Add($"({data.FemaleCustomization.belts.IndexOf(k) + 1}/{data.FemaleCustomization.belts.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.FemaleCustomization.hair) { hair.Add($"({data.FemaleCustomization.hair.IndexOf(k) + 1}/{data.FemaleCustomization.hair.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.FemaleCustomization.suspenders) { suspenders.Add($"({data.FemaleCustomization.suspenders.IndexOf(k) + 1}/{data.FemaleCustomization.suspenders.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.FemaleCustomization.gauntlets) { gauntlets.Add($"({data.FemaleCustomization.gauntlets.IndexOf(k) + 1}/{data.FemaleCustomization.gauntlets.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.FemaleCustomization.bags) { bags.Add($"({data.FemaleCustomization.bags.IndexOf(k) + 1}/{data.FemaleCustomization.bags.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.FemaleCustomization.unk1) { unk1.Add($"({data.FemaleCustomization.unk1.IndexOf(k) + 1}/{data.FemaleCustomization.unk1.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.FemaleCustomization.hats) { hats.Add($"({data.FemaleCustomization.hats.IndexOf(k) + 1}/{data.FemaleCustomization.hats.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.FemaleCustomization.gunBelts) { gunBelts.Add($"({data.FemaleCustomization.gunBelts.IndexOf(k) + 1}/{data.FemaleCustomization.gunBelts.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.FemaleCustomization.skirts) { skirts.Add($"({data.FemaleCustomization.skirts.IndexOf(k) + 1}/{data.FemaleCustomization.skirts.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.FemaleCustomization.belts2) { belts2.Add($"({data.FemaleCustomization.belts2.IndexOf(k) + 1}/{data.FemaleCustomization.belts2.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.FemaleCustomization.ponchos2) { ponchos2.Add($"({data.FemaleCustomization.ponchos2.IndexOf(k) + 1}/{data.FemaleCustomization.ponchos2.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.FemaleCustomization.bodyStyle) { bodyStyle.Add($"({data.FemaleCustomization.bodyStyle.IndexOf(k) + 1}/{data.FemaleCustomization.bodyStyle.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.FemaleCustomization.offHandHolsters) { offHandHolsters.Add($"({data.FemaleCustomization.offHandHolsters.IndexOf(k) + 1}/{data.FemaleCustomization.offHandHolsters.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.FemaleCustomization.coats2) { coats2.Add($"({data.FemaleCustomization.coats2.IndexOf(k) + 1}/{data.FemaleCustomization.coats2.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.FemaleCustomization.unk2) { unk2.Add($"({data.FemaleCustomization.unk2.IndexOf(k) + 1}/{data.FemaleCustomization.unk2.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.FemaleCustomization.gloves) { gloves.Add($"({data.FemaleCustomization.gloves.IndexOf(k) + 1}/{data.FemaleCustomization.gloves.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.FemaleCustomization.gunBeltAccessory) { gunBeltAccessory.Add($"({data.FemaleCustomization.gunBeltAccessory.IndexOf(k) + 1}/{data.FemaleCustomization.gunBeltAccessory.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.FemaleCustomization.rings2) { rings2.Add($"({data.FemaleCustomization.rings2.IndexOf(k) + 1}/{data.FemaleCustomization.rings2.Count()}) 0x{k.ToString("X08")}"); }
                        //foreach (var k in data.FemaleCustomization.beard) { buckles2.Add($"({data.FemaleCustomization.beard.IndexOf(k) + 1}/{data.FemaleCustomization.beard.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.FemaleCustomization.buckles2) { buckles2.Add($"({data.FemaleCustomization.buckles2.IndexOf(k) + 1}/{data.FemaleCustomization.buckles2.Count()}) 0x{k.ToString("X08")}"); }

                        femaleCustomMenu.AddMenuItem(new MenuListItem("Spurs", spurs, 0));
                        femaleCustomMenu.AddMenuItem(new MenuListItem("Pants", pants, 0));
                        femaleCustomMenu.AddMenuItem(new MenuListItem("Shirts", shirts, 0));
                        femaleCustomMenu.AddMenuItem(new MenuListItem("Chaps", chaps, 0));
                        femaleCustomMenu.AddMenuItem(new MenuListItem("Faces", faces, 0));
                        femaleCustomMenu.AddMenuItem(new MenuListItem("Ponchos", ponchos, 0));
                        femaleCustomMenu.AddMenuItem(new MenuListItem("Badges", badges, 0));
                        femaleCustomMenu.AddMenuItem(new MenuListItem("Vests", vests, 0));
                        femaleCustomMenu.AddMenuItem(new MenuListItem("Leg Armor", legArmor, 0));
                        femaleCustomMenu.AddMenuItem(new MenuListItem("Glasses", glasses, 0));
                        femaleCustomMenu.AddMenuItem(new MenuListItem("Bandanas", bandanas, 0));
                        femaleCustomMenu.AddMenuItem(new MenuListItem("Coats", coats, 0));
                        femaleCustomMenu.AddMenuItem(new MenuListItem("Body Armor", bodyArmor, 0));
                        femaleCustomMenu.AddMenuItem(new MenuListItem("Masks", masks, 0));
                        femaleCustomMenu.AddMenuItem(new MenuListItem("Boots", boots, 0));
                        femaleCustomMenu.AddMenuItem(new MenuListItem("Buckles", buckles, 0));
                        femaleCustomMenu.AddMenuItem(new MenuListItem("Rings", rings, 0));
                        femaleCustomMenu.AddMenuItem(new MenuListItem("Neckwear", neckwear, 0));
                        femaleCustomMenu.AddMenuItem(new MenuListItem("Wristbands", wristbands, 0));
                        femaleCustomMenu.AddMenuItem(new MenuListItem("Feet Style", feetStyle, 0));
                        femaleCustomMenu.AddMenuItem(new MenuListItem("Belts", belts, 0));
                        femaleCustomMenu.AddMenuItem(new MenuListItem("Hair", hair, 0));
                        femaleCustomMenu.AddMenuItem(new MenuListItem("Suspenders", suspenders, 0));
                        femaleCustomMenu.AddMenuItem(new MenuListItem("Gauntlets", gauntlets, 0));
                        femaleCustomMenu.AddMenuItem(new MenuListItem("Bags", bags, 0));
                        femaleCustomMenu.AddMenuItem(new MenuListItem("Unknown 1", unk1, 0));
                        femaleCustomMenu.AddMenuItem(new MenuListItem("Hats", hats, 0));
                        femaleCustomMenu.AddMenuItem(new MenuListItem("Gun Belts", gunBelts, 0));
                        femaleCustomMenu.AddMenuItem(new MenuListItem("Skirts", skirts, 0));
                        femaleCustomMenu.AddMenuItem(new MenuListItem("Belts 2", belts2, 0));
                        femaleCustomMenu.AddMenuItem(new MenuListItem("Ponchos 2", ponchos2, 0));
                        femaleCustomMenu.AddMenuItem(new MenuListItem("Body Style", bodyStyle, 0));
                        femaleCustomMenu.AddMenuItem(new MenuListItem("Off-Hand Holsters", offHandHolsters, 0));
                        femaleCustomMenu.AddMenuItem(new MenuListItem("Coats 2", coats2, 0));
                        femaleCustomMenu.AddMenuItem(new MenuListItem("Unknown 2", unk2, 0));
                        femaleCustomMenu.AddMenuItem(new MenuListItem("Gloves", gloves, 0));
                        femaleCustomMenu.AddMenuItem(new MenuListItem("Gun Belt Accessory", gunBeltAccessory, 0));
                        femaleCustomMenu.AddMenuItem(new MenuListItem("Rings 2", rings2, 0));
                        //femaleCustomMenu.AddMenuItem(new MenuListItem("Beard", beard, 0));
                        femaleCustomMenu.AddMenuItem(new MenuListItem("Buckles 2", buckles2, 0));

                        femaleCustomMenu.OnListIndexChange += (m, item, oldIndex, newIndex, itemIndex) =>
                        {
                            uint hash;
                            switch (itemIndex)
                            {
                                case 0: hash = data.FemaleCustomization.spurs[newIndex]; break;
                                case 1: hash = data.FemaleCustomization.pants[newIndex]; break;
                                case 2: hash = data.FemaleCustomization.shirts[newIndex]; break;
                                case 3: hash = data.FemaleCustomization.chaps[newIndex]; break;
                                case 4: hash = data.FemaleCustomization.faces[newIndex]; break;
                                case 5: hash = data.FemaleCustomization.ponchos[newIndex]; break;
                                case 6: hash = data.FemaleCustomization.badges[newIndex]; break;
                                case 7: hash = data.FemaleCustomization.vests[newIndex]; break;
                                case 8: hash = data.FemaleCustomization.legArmor[newIndex]; break;
                                case 9: hash = data.FemaleCustomization.glasses[newIndex]; break;
                                case 10: hash = data.FemaleCustomization.bandanas[newIndex]; break;
                                case 11: hash = data.FemaleCustomization.coats[newIndex]; break;
                                case 12: hash = data.FemaleCustomization.bodyArmor[newIndex]; break;
                                case 13: hash = data.FemaleCustomization.masks[newIndex]; break;
                                case 14: hash = data.FemaleCustomization.boots[newIndex]; break;
                                case 15: hash = data.FemaleCustomization.buckles[newIndex]; break;
                                case 16: hash = data.FemaleCustomization.rings[newIndex]; break;
                                case 17: hash = data.FemaleCustomization.neckwear[newIndex]; break;
                                case 18: hash = data.FemaleCustomization.wristbands[newIndex]; break;
                                case 19: hash = data.FemaleCustomization.feetStyle[newIndex]; break;
                                case 20: hash = data.FemaleCustomization.belts[newIndex]; break;
                                case 21: hash = data.FemaleCustomization.hair[newIndex]; break;
                                case 22: hash = data.FemaleCustomization.suspenders[newIndex]; break;
                                case 23: hash = data.FemaleCustomization.gauntlets[newIndex]; break;
                                case 24: hash = data.FemaleCustomization.bags[newIndex]; break;
                                case 25: hash = data.FemaleCustomization.unk1[newIndex]; break;
                                case 26: hash = data.FemaleCustomization.hats[newIndex]; break;
                                case 27: hash = data.FemaleCustomization.gunBelts[newIndex]; break;
                                case 28: hash = data.FemaleCustomization.skirts[newIndex]; break;
                                case 29: hash = data.FemaleCustomization.belts2[newIndex]; break;
                                case 30: hash = data.FemaleCustomization.ponchos2[newIndex]; break;
                                case 31: hash = data.FemaleCustomization.bodyStyle[newIndex]; break;
                                case 32: hash = data.FemaleCustomization.offHandHolsters[newIndex]; break;
                                case 33: hash = data.FemaleCustomization.coats2[newIndex]; break;
                                case 34: hash = data.FemaleCustomization.unk2[newIndex]; break;
                                case 35: hash = data.FemaleCustomization.gloves[newIndex]; break;
                                case 36: hash = data.FemaleCustomization.gunBeltAccessory[newIndex]; break;
                                case 37: hash = data.FemaleCustomization.rings2[newIndex]; break;
                                //case 38: hash = data.FemaleCustomization.beard[newIndex]; break;
                                case 38: hash = data.FemaleCustomization.buckles2[newIndex]; break;
                                default:
                                    hash = 0;
                                    break;
                            }
                            if (hash != 0)
                            {
                                Function.Call((Hash)0xD3A7B003ED343FD9, PlayerPedId(), hash, true, true, false);
                            }
                        };
                    }
                    #endregion

                    #region male
                    {
                        List<string> spurs = new List<string>();
                        List<string> pants = new List<string>();
                        List<string> shirts = new List<string>();
                        List<string> chaps = new List<string>();
                        List<string> faces = new List<string>();
                        List<string> ponchos = new List<string>();
                        List<string> badges = new List<string>();
                        List<string> vests = new List<string>();
                        List<string> legArmor = new List<string>();
                        List<string> glasses = new List<string>();
                        List<string> bandanas = new List<string>();
                        List<string> coats = new List<string>();
                        List<string> bodyArmor = new List<string>();
                        List<string> masks = new List<string>();
                        List<string> boots = new List<string>();
                        List<string> buckles = new List<string>();
                        List<string> rings = new List<string>();
                        List<string> neckwear = new List<string>();
                        List<string> wristbands = new List<string>();
                        List<string> feetStyle = new List<string>();
                        List<string> belts = new List<string>();
                        List<string> hair = new List<string>();
                        List<string> suspenders = new List<string>();
                        List<string> gauntlets = new List<string>();
                        List<string> bags = new List<string>();
                        List<string> unk1 = new List<string>();
                        List<string> hats = new List<string>();
                        List<string> gunBelts = new List<string>();
                        //List<string> skirts = new List<string>();
                        List<string> belts2 = new List<string>();
                        List<string> ponchos2 = new List<string>();
                        List<string> bodyStyle = new List<string>();
                        List<string> offHandHolsters = new List<string>();
                        List<string> coats2 = new List<string>();
                        List<string> unk2 = new List<string>();
                        List<string> gloves = new List<string>();
                        List<string> gunBeltAccessory = new List<string>();
                        List<string> rings2 = new List<string>();
                        List<string> beard = new List<string>();
                        List<string> buckles2 = new List<string>();
                        foreach (var k in data.MaleCustomization.spurs) { spurs.Add($"({data.MaleCustomization.spurs.IndexOf(k) + 1}/{data.MaleCustomization.spurs.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.MaleCustomization.pants) { pants.Add($"({data.MaleCustomization.pants.IndexOf(k) + 1}/{data.MaleCustomization.pants.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.MaleCustomization.shirts) { shirts.Add($"({data.MaleCustomization.shirts.IndexOf(k) + 1}/{data.MaleCustomization.shirts.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.MaleCustomization.chaps) { chaps.Add($"({data.MaleCustomization.chaps.IndexOf(k) + 1}/{data.MaleCustomization.chaps.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.MaleCustomization.faces) { faces.Add($"({data.MaleCustomization.faces.IndexOf(k) + 1}/{data.MaleCustomization.faces.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.MaleCustomization.ponchos) { ponchos.Add($"({data.MaleCustomization.ponchos.IndexOf(k) + 1}/{data.MaleCustomization.ponchos.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.MaleCustomization.badges) { badges.Add($"({data.MaleCustomization.badges.IndexOf(k) + 1}/{data.MaleCustomization.badges.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.MaleCustomization.vests) { vests.Add($"({data.MaleCustomization.vests.IndexOf(k) + 1}/{data.MaleCustomization.vests.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.MaleCustomization.legArmor) { legArmor.Add($"({data.MaleCustomization.legArmor.IndexOf(k) + 1}/{data.MaleCustomization.legArmor.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.MaleCustomization.glasses) { glasses.Add($"({data.MaleCustomization.glasses.IndexOf(k) + 1}/{data.MaleCustomization.glasses.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.MaleCustomization.bandanas) { bandanas.Add($"({data.MaleCustomization.bandanas.IndexOf(k) + 1}/{data.MaleCustomization.bandanas.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.MaleCustomization.coats) { coats.Add($"({data.MaleCustomization.coats.IndexOf(k) + 1}/{data.MaleCustomization.coats.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.MaleCustomization.bodyArmor) { bodyArmor.Add($"({data.MaleCustomization.bodyArmor.IndexOf(k) + 1}/{data.MaleCustomization.bodyArmor.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.MaleCustomization.masks) { masks.Add($"({data.MaleCustomization.masks.IndexOf(k) + 1}/{data.MaleCustomization.masks.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.MaleCustomization.boots) { boots.Add($"({data.MaleCustomization.boots.IndexOf(k) + 1}/{data.MaleCustomization.boots.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.MaleCustomization.buckles) { buckles.Add($"({data.MaleCustomization.buckles.IndexOf(k) + 1}/{data.MaleCustomization.buckles.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.MaleCustomization.rings) { rings.Add($"({data.MaleCustomization.rings.IndexOf(k) + 1}/{data.MaleCustomization.rings.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.MaleCustomization.neckwear) { neckwear.Add($"({data.MaleCustomization.neckwear.IndexOf(k) + 1}/{data.MaleCustomization.neckwear.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.MaleCustomization.wristbands) { wristbands.Add($"({data.MaleCustomization.wristbands.IndexOf(k) + 1}/{data.MaleCustomization.wristbands.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.MaleCustomization.feetStyle) { feetStyle.Add($"({data.MaleCustomization.feetStyle.IndexOf(k) + 1}/{data.MaleCustomization.feetStyle.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.MaleCustomization.belts) { belts.Add($"({data.MaleCustomization.belts.IndexOf(k) + 1}/{data.MaleCustomization.belts.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.MaleCustomization.hair) { hair.Add($"({data.MaleCustomization.hair.IndexOf(k) + 1}/{data.MaleCustomization.hair.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.MaleCustomization.suspenders) { suspenders.Add($"({data.MaleCustomization.suspenders.IndexOf(k) + 1}/{data.MaleCustomization.suspenders.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.MaleCustomization.gauntlets) { gauntlets.Add($"({data.MaleCustomization.gauntlets.IndexOf(k) + 1}/{data.MaleCustomization.gauntlets.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.MaleCustomization.bags) { bags.Add($"({data.MaleCustomization.bags.IndexOf(k) + 1}/{data.MaleCustomization.bags.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.MaleCustomization.unk1) { unk1.Add($"({data.MaleCustomization.unk1.IndexOf(k) + 1}/{data.MaleCustomization.unk1.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.MaleCustomization.hats) { hats.Add($"({data.MaleCustomization.hats.IndexOf(k) + 1}/{data.MaleCustomization.hats.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.MaleCustomization.gunBelts) { gunBelts.Add($"({data.MaleCustomization.gunBelts.IndexOf(k) + 1}/{data.MaleCustomization.gunBelts.Count()}) 0x{k.ToString("X08")}"); }
                        //foreach (var k in data.MaleCustomization.skirts) { skirts.Add($"({data.MaleCustomization.skirts.IndexOf(k) + 1}/{data.MaleCustomization.skirts.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.MaleCustomization.belts2) { belts2.Add($"({data.MaleCustomization.belts2.IndexOf(k) + 1}/{data.MaleCustomization.belts2.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.MaleCustomization.ponchos2) { ponchos2.Add($"({data.MaleCustomization.ponchos2.IndexOf(k) + 1}/{data.MaleCustomization.ponchos2.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.MaleCustomization.bodyStyle) { bodyStyle.Add($"({data.MaleCustomization.bodyStyle.IndexOf(k) + 1}/{data.MaleCustomization.bodyStyle.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.MaleCustomization.offHandHolsters) { offHandHolsters.Add($"({data.MaleCustomization.offHandHolsters.IndexOf(k) + 1}/{data.MaleCustomization.offHandHolsters.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.MaleCustomization.coats2) { coats2.Add($"({data.MaleCustomization.coats2.IndexOf(k) + 1}/{data.MaleCustomization.coats2.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.MaleCustomization.unk2) { unk2.Add($"({data.MaleCustomization.unk2.IndexOf(k) + 1}/{data.MaleCustomization.unk2.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.MaleCustomization.gloves) { gloves.Add($"({data.MaleCustomization.gloves.IndexOf(k) + 1}/{data.MaleCustomization.gloves.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.MaleCustomization.gunBeltAccessory) { gunBeltAccessory.Add($"({data.MaleCustomization.gunBeltAccessory.IndexOf(k) + 1}/{data.MaleCustomization.gunBeltAccessory.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.MaleCustomization.rings2) { rings2.Add($"({data.MaleCustomization.rings2.IndexOf(k) + 1}/{data.MaleCustomization.rings2.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.MaleCustomization.beard) { buckles2.Add($"({data.MaleCustomization.beard.IndexOf(k) + 1}/{data.MaleCustomization.beard.Count()}) 0x{k.ToString("X08")}"); }
                        foreach (var k in data.MaleCustomization.buckles2) { buckles2.Add($"({data.MaleCustomization.buckles2.IndexOf(k) + 1}/{data.MaleCustomization.buckles2.Count()}) 0x{k.ToString("X08")}"); }

                        maleCustomMenu.AddMenuItem(new MenuListItem("Spurs", spurs, 0));
                        maleCustomMenu.AddMenuItem(new MenuListItem("Pants", pants, 0));
                        maleCustomMenu.AddMenuItem(new MenuListItem("Shirts", shirts, 0));
                        maleCustomMenu.AddMenuItem(new MenuListItem("Chaps", chaps, 0));
                        maleCustomMenu.AddMenuItem(new MenuListItem("Faces", faces, 0));
                        maleCustomMenu.AddMenuItem(new MenuListItem("Ponchos", ponchos, 0));
                        maleCustomMenu.AddMenuItem(new MenuListItem("Badges", badges, 0));
                        maleCustomMenu.AddMenuItem(new MenuListItem("Vests", vests, 0));
                        maleCustomMenu.AddMenuItem(new MenuListItem("Leg Armor", legArmor, 0));
                        maleCustomMenu.AddMenuItem(new MenuListItem("Glasses", glasses, 0));
                        maleCustomMenu.AddMenuItem(new MenuListItem("Bandanas", bandanas, 0));
                        maleCustomMenu.AddMenuItem(new MenuListItem("Coats", coats, 0));
                        maleCustomMenu.AddMenuItem(new MenuListItem("Body Armor", bodyArmor, 0));
                        maleCustomMenu.AddMenuItem(new MenuListItem("Masks", masks, 0));
                        maleCustomMenu.AddMenuItem(new MenuListItem("Boots", boots, 0));
                        maleCustomMenu.AddMenuItem(new MenuListItem("Buckles", buckles, 0));
                        maleCustomMenu.AddMenuItem(new MenuListItem("Rings", rings, 0));
                        maleCustomMenu.AddMenuItem(new MenuListItem("Neckwear", neckwear, 0));
                        maleCustomMenu.AddMenuItem(new MenuListItem("Wristbands", wristbands, 0));
                        maleCustomMenu.AddMenuItem(new MenuListItem("Feet Style", feetStyle, 0));
                        maleCustomMenu.AddMenuItem(new MenuListItem("Belts", belts, 0));
                        maleCustomMenu.AddMenuItem(new MenuListItem("Hair", hair, 0));
                        maleCustomMenu.AddMenuItem(new MenuListItem("Suspenders", suspenders, 0));
                        maleCustomMenu.AddMenuItem(new MenuListItem("Gauntlets", gauntlets, 0));
                        maleCustomMenu.AddMenuItem(new MenuListItem("Bags", bags, 0));
                        maleCustomMenu.AddMenuItem(new MenuListItem("Unknown 1", unk1, 0));
                        maleCustomMenu.AddMenuItem(new MenuListItem("Hats", hats, 0));
                        maleCustomMenu.AddMenuItem(new MenuListItem("Gun Belts", gunBelts, 0));
                        //maleCustomMenu.AddMenuItem(new MenuListItem("Skirts", skirts, 0));
                        maleCustomMenu.AddMenuItem(new MenuListItem("Belts 2", belts2, 0));
                        maleCustomMenu.AddMenuItem(new MenuListItem("Ponchos 2", ponchos2, 0));
                        maleCustomMenu.AddMenuItem(new MenuListItem("Body Style", bodyStyle, 0));
                        maleCustomMenu.AddMenuItem(new MenuListItem("Off-Hand Holsters", offHandHolsters, 0));
                        maleCustomMenu.AddMenuItem(new MenuListItem("Coats 2", coats2, 0));
                        maleCustomMenu.AddMenuItem(new MenuListItem("Unknown 2", unk2, 0));
                        maleCustomMenu.AddMenuItem(new MenuListItem("Gloves", gloves, 0));
                        maleCustomMenu.AddMenuItem(new MenuListItem("Gun Belt Accessory", gunBeltAccessory, 0));
                        maleCustomMenu.AddMenuItem(new MenuListItem("Rings 2", rings2, 0));
                        maleCustomMenu.AddMenuItem(new MenuListItem("Beard", beard, 0));
                        maleCustomMenu.AddMenuItem(new MenuListItem("Buckles 2", buckles2, 0));

                        maleCustomMenu.OnListIndexChange += (m, item, oldIndex, newIndex, itemIndex) =>
                        {
                            uint hash;
                            switch (itemIndex)
                            {
                                case 0: hash = data.MaleCustomization.spurs[newIndex]; break;
                                case 1: hash = data.MaleCustomization.pants[newIndex]; break;
                                case 2: hash = data.MaleCustomization.shirts[newIndex]; break;
                                case 3: hash = data.MaleCustomization.chaps[newIndex]; break;
                                case 4: hash = data.MaleCustomization.faces[newIndex]; break;
                                case 5: hash = data.MaleCustomization.ponchos[newIndex]; break;
                                case 6: hash = data.MaleCustomization.badges[newIndex]; break;
                                case 7: hash = data.MaleCustomization.vests[newIndex]; break;
                                case 8: hash = data.MaleCustomization.legArmor[newIndex]; break;
                                case 9: hash = data.MaleCustomization.glasses[newIndex]; break;
                                case 10: hash = data.MaleCustomization.bandanas[newIndex]; break;
                                case 11: hash = data.MaleCustomization.coats[newIndex]; break;
                                case 12: hash = data.MaleCustomization.bodyArmor[newIndex]; break;
                                case 13: hash = data.MaleCustomization.masks[newIndex]; break;
                                case 14: hash = data.MaleCustomization.boots[newIndex]; break;
                                case 15: hash = data.MaleCustomization.buckles[newIndex]; break;
                                case 16: hash = data.MaleCustomization.rings[newIndex]; break;
                                case 17: hash = data.MaleCustomization.neckwear[newIndex]; break;
                                case 18: hash = data.MaleCustomization.wristbands[newIndex]; break;
                                case 19: hash = data.MaleCustomization.feetStyle[newIndex]; break;
                                case 20: hash = data.MaleCustomization.belts[newIndex]; break;
                                case 21: hash = data.MaleCustomization.hair[newIndex]; break;
                                case 22: hash = data.MaleCustomization.suspenders[newIndex]; break;
                                case 23: hash = data.MaleCustomization.gauntlets[newIndex]; break;
                                case 24: hash = data.MaleCustomization.bags[newIndex]; break;
                                case 25: hash = data.MaleCustomization.unk1[newIndex]; break;
                                case 26: hash = data.MaleCustomization.hats[newIndex]; break;
                                case 27: hash = data.MaleCustomization.gunBelts[newIndex]; break;
                                //case 28: hash = data.MaleCustomization.skirts[newIndex]; break;
                                case 28: hash = data.MaleCustomization.belts2[newIndex]; break;
                                case 29: hash = data.MaleCustomization.ponchos2[newIndex]; break;
                                case 30: hash = data.MaleCustomization.bodyStyle[newIndex]; break;
                                case 31: hash = data.MaleCustomization.offHandHolsters[newIndex]; break;
                                case 32: hash = data.MaleCustomization.coats2[newIndex]; break;
                                case 33: hash = data.MaleCustomization.unk2[newIndex]; break;
                                case 34: hash = data.MaleCustomization.gloves[newIndex]; break;
                                case 35: hash = data.MaleCustomization.gunBeltAccessory[newIndex]; break;
                                case 36: hash = data.MaleCustomization.rings2[newIndex]; break;
                                case 37: hash = data.MaleCustomization.beard[newIndex]; break;
                                case 38: hash = data.MaleCustomization.buckles2[newIndex]; break;
                                default:
                                    hash = 0;
                                    break;
                            }
                            if (hash != 0)
                            {
                                Function.Call((Hash)0xD3A7B003ED343FD9, PlayerPedId(), hash, true, true, false);
                            }
                            else
                            {
                                Function.Call((Hash)0xD3A7B003ED343FD9, PlayerPedId(), hash, true, true, false);
                            }
                        };
                    }
                    #endregion


                    appearanceMenu.AddMenuItem(femaleCustom);
                    appearanceMenu.AddMenuItem(maleCustom);

                    MenuController.BindMenuItem(appearanceMenu, femaleCustomMenu, femaleCustom);
                    MenuController.BindMenuItem(appearanceMenu, maleCustomMenu, maleCustom);
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

            menu.OnListItemSelect += (m, item, listIndex, itemIndex) =>
            {
                if (item == maxOuterCoresList)
                {
                    switch (listIndex)
                    {
                        case 0: // all
                            Debug.WriteLine($"Max attribute points for this ped health: {GetAttributePoints(PlayerPedId(), 0)}");
                            Debug.WriteLine($"Max attribute points for this ped stamina: {GetAttributePoints(PlayerPedId(), 1)}");
                            Debug.WriteLine($"Max attribute points for this ped dead eye: {GetAttributePoints(PlayerPedId(), 2)}");
                            //SetAttributePoints(PlayerPedId(), 0, Function.Call<int>((Hash)0x3FC4C027FD0936F4, PlayerPedId(), 0));
                            //SetAttributePoints(PlayerPedId(), 1, Function.Call<int>((Hash)0x3FC4C027FD0936F4, PlayerPedId(), 1));
                            //SetAttributePoints(PlayerPedId(), 2, Function.Call<int>((Hash)0x3FC4C027FD0936F4, PlayerPedId(), 2));
                            break;
                        case 1: // health
                            Debug.WriteLine($"Max attribute points for this ped health: {GetAttributePoints(PlayerPedId(), 0)}");
                            break;
                        case 2: // stamina
                            Debug.WriteLine($"Max attribute points for this ped stamina: {GetAttributePoints(PlayerPedId(), 1)}");
                            break;
                        case 3: // dead eye
                            Debug.WriteLine($"Max attribute points for this ped dead eye: {GetAttributePoints(PlayerPedId(), 2)}");
                            break;
                        default: // invalid index
                            break;
                    }
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
