using Newtonsoft.Json;
using System;

namespace Logger
{
    public class Recovery
    {
        static string recoveryFileName = "recovery";

        /// <summary>
        /// Save the state of the application in a file
        /// </summary>
        /// <typeparam name="T">Type of the state</typeparam>
        /// <param name="state">Object representing the state</param>
        /// <returns></returns>
        public static bool Save<T>(T state) where T : class
        {
            try
            {
                Utils.CreateFile(recoveryFileName);
                string json = JsonConvert.SerializeObject(state);
                return Utils.AddToFile(recoveryFileName, json, false);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return false;
            }
        }

        /// <summary>
        /// Retrieves the saved state
        /// </summary>
        /// <typeparam name="T">Type of the state</typeparam>
        /// <returns>Object representing the atate</returns>
        public static T Recover<T>() where T : class
        {
            string json;
            try
            {
                json = Utils.ReadFile(recoveryFileName);
                T state = JsonConvert.DeserializeObject<T>(json);
                return state;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return null;
            }
        }
    }

}
