using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace RedMenuClient.util
{
    public static class StorageManager
    {

        #region Setters
        /// <summary>
        /// Save a <see cref="string"/> value to client storage.
        /// </summary>
        /// <param name="key">The name of the value you want to store.</param>
        /// <param name="value">The value to store.</param>
        /// <param name="overrideExisting">If you want to override any exisiting values with the same key.</param>
        /// <returns>True if save was successful, false if save name is already in use.</returns>
        public static bool Save(string key, string value, bool overrideExisting)
        {
            if (Exists(key))
            {
                if (overrideExisting)
                {
                    if (GetType(key) != typeof(string))
                    {
                        Delete(key);
                    }
                }
                else
                {
                    return false;
                }
            }

            SetResourceKvp($"string_{key}", value);
            return true;
        }

        /// <summary>
        /// Save an <see cref="int"/> value to client storage.
        /// </summary>
        /// <param name="key">The name of the value you want to store.</param>
        /// <param name="value">The value to store.</param>
        /// <param name="overrideExisting">If you want to override any exisiting values with the same key.</param>
        /// <returns>True if save was successful, false if save name is already in use.</returns>
        public static bool Save(string key, int value, bool overrideExisting)
        {
            if (Exists(key))
            {
                if (overrideExisting)
                {
                    if (GetType(key) != typeof(int))
                    {
                        Delete(key);
                    }
                }
                else
                {
                    return false;
                }
            }

            SetResourceKvpInt($"int_{key}", value);
            return true;
        }

        /// <summary>
        /// Save a <see cref="float"/> value to client storage.
        /// </summary>
        /// <param name="key">The name of the value you want to store.</param>
        /// <param name="value">The value to store.</param>
        /// <param name="overrideExisting">If you want to override any exisiting values with the same key.</param>
        /// <returns>True if save was successful, false if save name is already in use.</returns>
        public static bool Save(string key, float value, bool overrideExisting)
        {
            if (Exists(key))
            {
                if (overrideExisting)
                {
                    if (GetType(key) != typeof(float))
                    {
                        Delete(key);
                    }
                }
                else
                {
                    return false;
                }
            }

            SetResourceKvpFloat($"float_{key}", value);
            return true;
        }

        /// <summary>
        /// Save a <see cref="bool"/> value to client storage.
        /// </summary>
        /// <param name="key">The name of the value you want to store.</param>
        /// <param name="value">The value to store.</param>
        /// <param name="overrideExisting">If you want to override any exisiting values with the same key.</param>
        /// <returns>True if save was successful, false if save name is already in use.</returns>
        public static bool Save(string key, bool value, bool overrideExisting)
        {
            if (Exists(key))
            {
                if (overrideExisting)
                {
                    if (GetType(key) != typeof(string))
                    {
                        Delete(key);
                    }
                }
                else
                {
                    return false;
                }
            }

            // string prefix because it's saved as a string, if there's ever a bool function implemented we'll use a bool prefix to prevent conflicting saved data.
            SetResourceKvp($"string_{key}", value.ToString());
            return true;
        }

        /// <summary>
        /// Save a <see cref="Vector3"/> value to client storage.
        /// </summary>
        /// <param name="key">The name of the value you want to store.</param>
        /// <param name="value">The value to store.</param>
        /// <param name="overrideExisting">If you want to override any exisiting values with the same key.</param>
        /// <returns>True if save was successful, false if save name is already in use.</returns>
        public static bool Save(string key, Vector3 value, bool overrideExisting)
        {
            if (Exists(key))
            {
                if (overrideExisting)
                {
                    if (GetType(key) != typeof(Vector3))
                    {
                        Delete(key);
                    }
                }
                else
                {
                    return false;
                }
            }

            // Unlikely to ever have a real Vector3 kvp option so it's safe to use vector3_<direction> as a prefix even though it's a float.
            SetResourceKvpFloat($"vector3_x_{key}", value.X);
            SetResourceKvpFloat($"vector3_y_{key}", value.Y);
            SetResourceKvpFloat($"vector3_z_{key}", value.Z);
            return true;
        }
        #endregion

        #region Getters
        /// <summary>
        /// Get a saved <see cref="string"/> value from client storage.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="output"></param>
        /// <returns>Returns false if the key does not exist.</returns>
        public static bool TryGet(string key, out string output)
        {
            if (!ExistsWithType(key, "string"))
            {
                output = null;
                return false;
            }
            output = GetResourceKvpString($"string_{key}");
            return true;
        }

        /// <summary>
        /// Get a saved <see cref="int"/> value from client storage.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="output"></param>
        /// <returns>Returns false if the key does not exist.</returns>
        public static bool TryGet(string key, out int output)
        {
            if (!ExistsWithType(key, "int"))
            {
                output = 0;
                return false;
            }
            output = GetResourceKvpInt($"int_{key}");
            return true;
        }

        /// <summary>
        /// Get a saved <see cref="float"/> value from client storage.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="output"></param>
        /// <returns>Returns false if the key does not exist.</returns>
        public static bool TryGet(string key, out float output)
        {
            if (!ExistsWithType(key, "float"))
            {
                output = 0f;
                return false;
            }
            output = GetResourceKvpFloat($"float_{key}");
            return true;
        }

        /// <summary>
        /// Get a saved <see cref="bool"/> value from client storage.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="output"></param>
        /// <returns>Returns false if the key does not exist.</returns>
        public static bool TryGet(string key, out bool output)
        {
            if (!ExistsWithType(key, "string"))
            {
                output = false;
                return false;
            }
            if (bool.TryParse(GetResourceKvpString($"string_{key}"), out bool value))
            {
                output = value;
                return true;
            }
            output = false;
            return false;
        }

        /// <summary>
        /// Get a saved <see cref="Vector3"/> value from client storage.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="output"></param>
        /// <returns>Returns false if the key does not exist.</returns>
        public static bool TryGet(string key, out Vector3 output)
        {
            if (ExistsWithType(key, "vector3_x") && ExistsWithType(key, "vector3_y") && ExistsWithType(key, "vector3_z"))
            {
                float x = GetResourceKvpFloat($"vector3_x_{key}");
                float y = GetResourceKvpFloat($"vector3_y_{key}");
                float z = GetResourceKvpFloat($"vector3_z_{key}");
                output = new Vector3(x, y, z);
                return true;
            }
            output = Vector3.Zero;
            return false;
        }
        #endregion

        #region misc
        /// <summary>
        /// Returns a list of all stored keys.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAllKeys()
        {
            List<string> kvps = new List<string>();
            int handle = StartFindKvp("");
            while (true)
            {
                string kvp = FindKvp(handle);
                if (string.IsNullOrEmpty(kvp))
                {
                    break;
                }
                kvps.Add(kvp);
            }
            EndFindKvp(handle);
            return kvps;
        }

        /// <summary>
        /// Returns true if this key is saved, regardless of the type.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Exists(string key)
        {
            return GetAllKeys().Any((v) => v.EndsWith($"_{key}"));
        }

        /// <summary>
        /// Returns true if this key is saved as the specified type.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool ExistsWithType(string key, string type)
        {
            return GetAllKeys().Any((v) => v == $"{type}_{key}");
        }

        /// <summary>
        /// Returns the type of the saved key. Returns null if the key does not exist or the key type is unknown.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static Type GetType(string key)
        {
            if (!Exists(key))
            {
                return null;
            }
            string foundKvp = GetAllKeys().Find((v) => v.EndsWith($"_{key}"));
            if (foundKvp.StartsWith("string_"))
            {
                return typeof(string);
            }
            else if (foundKvp.StartsWith("int_"))
            {
                return typeof(int);
            }
            else if (foundKvp.StartsWith("float_"))
            {
                return typeof(float);
            }
            else if (foundKvp.StartsWith("vector3_"))
            {
                return typeof(Vector3);
            }
            return null;
        }

        /// <summary>
        /// Deletes the specified key if it exists.
        /// </summary>
        /// <param name="key"></param>
        public static void Delete(string key)
        {
            if (Exists(key))
            {
                var type = GetType(key);
                if (type == typeof(string))
                {
                    DeleteResourceKvp($"string_{key}");
                }
                else if (type == typeof(int))
                {
                    DeleteResourceKvp($"int_{key}");
                }
                else if (type == typeof(float))
                {
                    DeleteResourceKvp($"float_{key}");
                }
                else if (type == typeof(Vector3))
                {
                    DeleteResourceKvp($"vector3_x_{key}");
                    DeleteResourceKvp($"vector3_y_{key}");
                    DeleteResourceKvp($"vector3_z_{key}");
                }
            }
        }
        #endregion
    }
}
