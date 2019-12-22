using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using Newtonsoft.Json;

namespace RedMenuShared
{
    /// <summary>
    /// All permissions.
    /// </summary>
    public enum Permission
    {
        // Player Menu
        PMMenu,
        PMRestoreHealth,
        PMRestoreStamina,
        PMRestoreDeadEye,
        PMMaxOuterCores,
        PMGodMode,
        PMInfiniteStamina,
        PMInfiniteDeadEye,
        PMClearTasks,
        PMHogtieSelf,
        PMCleanPed,
        PMSelectPlayerModel,
        PMSelectOutfit,
        PMCustomizeMpPeds,

        // Weapons Menu
        WMMenu,
        WMGetAllWeapons,
        WMGetSingleWeapon,
        WMRemoveAllWeapons,
        WMRemoveCurrentWeapon,
        WMMaxAmmo,
        WMGiveAmmo
    }


    public class PermissionsManager : BaseScript
    {
        /// <summary>
        /// Constructor, only used for the client side to trigger the permissions event.
        /// </summary>
        public PermissionsManager()
        {
#if CLIENT
            TriggerServerEvent("rm:getUserPermissions");
#endif
        }

#if SERVER
        /// <summary>
        /// Returns the actual ace name corresponding to the provided the <see cref="Permission"/> enum name.
        /// </summary>
        /// <param name="permissionName"><see cref="Permission"/> enum name.</param>
        /// <returns></returns>
        private static string GetAceNameFromPermission(string permissionName)
        {
            const string prefix = "RedMenu";
            if (permissionName.StartsWith("PM"))
            {
                return prefix + ".PlayerMenu." + permissionName.Substring(2);
            }
            if (permissionName.StartsWith("WM"))
            {
                return prefix + ".WeaponsMenu." + permissionName.Substring(2);
            }
            return null;
        }

        /// <summary>
        /// Eventhandler to send all allowed permissions to the requesting client.
        /// </summary>
        /// <param name="source">THe client that requests the permissions.</param>
        [EventHandler("rm:getUserPermissions")]
        private static void GetUserPermissions([FromSource]Player source)
        {
            List<string> perms = new List<string>();
            foreach (var v in Enum.GetNames(typeof(Permission)))
            {
                string aceName = GetAceNameFromPermission(v);
                if (!string.IsNullOrEmpty(aceName))
                {
                    if (IsPlayerAceAllowed(source.Handle.ToString(), aceName))
                    {
                        perms.Add(v);
                    }
                }
            }
            source.TriggerEvent("rm:setUserPermissions", perms);
        }
#endif

#if CLIENT
        /// <summary>
        /// List of all allowed permissions, for internal use please use <see cref="IsAllowed(Permission)"/> instead.
        /// </summary>
        private static List<Permission> allowedPermissions = new List<Permission>();

        /// <summary>
        /// Sets the allowed permissions received from the server event.
        /// </summary>
        /// <param name="permissions">The permissions object received from the server.</param>
        [EventHandler("rm:setUserPermissions")]
        private static void SetPermissions(List<object> permissions)
        {
            allowedPermissions.Clear();
            foreach (var s in permissions)
            {
                if (Enum.TryParse(s.ToString(), out Permission p))
                {
                    allowedPermissions.Add(p);
                }
            }
            RedMenuClient.MainClient.PermissionsSetupDone = true;
        }

        /// <summary>
        /// Returns true if the user has this permission.
        /// </summary>
        /// <param name="permission">The permission to check.</param>
        /// <returns></returns>
        internal static bool IsAllowed(Permission permission)
        {
            if (allowedPermissions.Contains(permission))
            {
                return true;
            }
            return false;
        }
#endif
#if SERVER
        internal static bool IsPlayerAllowed(Player player, Permission permission)
        {
            if (IsPlayerAceAllowed(player.Handle, GetAceNameFromPermission(Enum.GetName(typeof(Permission), permission))))
            {
                return true;
            }

            return false;
        }
#endif
    }
}
