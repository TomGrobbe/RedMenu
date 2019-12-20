using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuAPI;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using RedMenuShared;
using RedMenuClient.util;

namespace RedMenuClient
{
    class TickFunctions : BaseScript
    {
        public TickFunctions() { }


        private static int lastPed = 0;
        [Tick]
        internal static async Task PedChangeDetectionTick()
        {
            async Task PedChanged()
            {
                // Update godmode.

                if (PermissionsManager.IsAllowed(Permission.PMGodMode) && UserDefaults.PlayerGodMode)
                {
                    SetEntityInvincible(PlayerPedId(), true);
                }
                else
                {
                    SetEntityInvincible(PlayerPedId(), false);
                }

                // This needs more native research for the outer cores.
                //if (ConfigManager.EnableMaxStats)
                //{
                //    SetAttribute(PlayerPedId(), 0, GetMaxAttributePoints(PlayerPedId(), 0));
                //    SetAttributePoints(PlayerPedId(), 1, GetMaxAttributePoints(PlayerPedId(), 1));
                //    SetAttributePoints(PlayerPedId(), 2, GetMaxAttributePoints(PlayerPedId(), 2));
                //}

                // todo: add infinite stamina and infinite dead eye checks



                lastPed = PlayerPedId();
                await Task.FromResult(0);
            }


            if (lastPed != PlayerPedId())
            {
                await PedChanged();
            }
            int ped = PlayerPedId();
            while (ped == PlayerPedId())
            {
                await Delay(1000);
            }

            // ped changed.
            await PedChanged();
        }

        /// <summary>
        /// Manages the radar toggle when holding down the select radar mode button.
        /// Until more radar natives are discovered, this will have to do with only an on/off toggle.
        /// </summary>
        /// <returns></returns>
        [Tick]
        internal static async Task RadarToggleTick()
        {
            if (UserDefaults.MiscMinimapControls)
            {
                if (Util.IsControlPressed(Control.SelectRadarMode))
                {
                    //UiPrompt promptY = new UiPrompt(new Control[1] { Control.ContextY }, "Compass");
                    //UiPrompt promptX = new UiPrompt(new Control[1] { Control.ContextX }, "Expanded");
                    UiPrompt promptA = new UiPrompt(new Control[1] { Control.ContextA }, "Regular");
                    UiPrompt promptB = new UiPrompt(new Control[1] { Control.ContextB }, "Off", "BRT2MountPrompt");
                    //promptY.Prepare();
                    //promptX.Prepare();
                    promptA.Prepare();
                    promptB.Prepare();
                    bool enabled = false;
                    while (Util.IsControlPressed(Control.SelectRadarMode))
                    {
                        if (!enabled)
                        {
                            //promptY.SetEnabled(true, true);
                            //promptX.SetEnabled(true, true);
                            promptA.SetEnabled(true, true);
                            promptB.SetEnabled(true, true);
                            enabled = true;
                        }

                        if (Util.IsControlJustReleased(Control.ContextB))
                        {
                            DisplayRadar(false);
                        }
                        if (Util.IsControlJustReleased(Control.ContextA))
                        {
                            DisplayRadar(true);
                        }
                        //if (Util.IsControlJustReleased(Control.ContextX))
                        //{
                        //    DisplayRadar(true);
                        //}
                        //if (Util.IsControlJustReleased(Control.ContextY))
                        //{
                        //    DisplayRadar(true);
                        //}

                        await Delay(0);
                    }
                    //promptY.Dispose();
                    //promptX.Dispose();
                    promptA.Dispose();
                    promptB.Dispose();
                    while (Util.IsControlPressed(Control.SelectRadarMode))
                    {
                        await Delay(0);
                    }
                }
            }
            else
            {
                await Delay(1000);
            }
            await Task.FromResult(0);
        }
    }
}
