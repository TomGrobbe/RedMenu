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
    static class Util
    {

        /// <summary>
        /// Returns true if the control is pressed.
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static bool IsControlPressed(Control control)
        {
            if (CitizenFX.Core.Native.Function.Call<bool>(CitizenFX.Core.Native.Hash.IS_CONTROL_PRESSED, 0, (uint)control))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns true if the control is just pressed.
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static bool IsControlJustPressed(Control control)
        {
            if (CitizenFX.Core.Native.Function.Call<bool>(CitizenFX.Core.Native.Hash.IS_CONTROL_JUST_PRESSED, 0, (uint)control))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns true if the disabled control is pressed.
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static bool IsDisabledControlPressed(Control control)
        {
            if (CitizenFX.Core.Native.Function.Call<bool>(CitizenFX.Core.Native.Hash.IS_DISABLED_CONTROL_PRESSED, 0, (uint)control))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns true if the disabled control is just pressed.
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static bool IsDisabledControlJustPressed(Control control)
        {
            if (CitizenFX.Core.Native.Function.Call<bool>(CitizenFX.Core.Native.Hash.IS_DISABLED_CONTROL_JUST_PRESSED, 0, (uint)control))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns true if the control is released.
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static bool IsControlReleased(Control control)
        {
            if (CitizenFX.Core.Native.Function.Call<bool>(CitizenFX.Core.Native.Hash.IS_CONTROL_RELEASED, 0, (uint)control))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns true if the control is just released.
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static bool IsControlJustReleased(Control control)
        {
            if (CitizenFX.Core.Native.Function.Call<bool>(CitizenFX.Core.Native.Hash.IS_CONTROL_JUST_RELEASED, 0, (uint)control))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns true if the disabled control is released.
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static bool IsDisabledControlReleased(Control control)
        {
            if (CitizenFX.Core.Native.Function.Call<bool>(CitizenFX.Core.Native.Hash.IS_DISABLED_CONTROL_PRESSED, 0, (uint)control))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Returns true if the disabled control is just released.
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static bool IsDisabledControlJustReleased(Control control)
        {
            if (CitizenFX.Core.Native.Function.Call<bool>(CitizenFX.Core.Native.Hash.IS_DISABLED_CONTROL_JUST_RELEASED, 0, (uint)control))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns true if the control is enabled.
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static bool IsControlEnabled(Control control)
        {
            return CitizenFX.Core.Native.Function.Call<bool>(CitizenFX.Core.Native.Hash.IS_CONTROL_ENABLED, 0, (uint)control);
        }

    }
}
