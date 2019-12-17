using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuAPI;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace RedMenuClient
{
    class TickFunctions : BaseScript
    {
        public TickFunctions() { }

        /// <summary>
        /// Manages the radar toggle when holding down the select radar mode button.
        /// Until more radar natives are discovered, this will have to do with only an on/off toggle.
        /// </summary>
        /// <returns></returns>
        [Tick]
        internal static async Task RadarToggleTick()
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
            await Task.FromResult(0);
        }
    }
}
