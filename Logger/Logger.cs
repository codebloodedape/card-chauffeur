using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace Logger
{
    public class Logger
    {
        static string logFilePath;
        public static bool CreateLogFile(string moduleName)
        {
            logFilePath = moduleName + "-log";
            try
            {
                Utils.CreateFile(logFilePath);
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Couldn't create file " + logFilePath + " as you do not have permission to create files in the directory "
                    + Directory.GetCurrentDirectory());
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unhandled exception occured.");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Tries to log, if failed, ignores and moves on
        /// </summary>
        /// <param name="log"></param>
        public static void Log(string log)
        {
            Utils.AddToFile(logFilePath, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff") + ":" + log, true);
        }

        /// <summary>
        /// Tries to log, if failed, ignores and moves on
        /// </summary>
        /// <param name="ex"></param>
        public static void Log(Exception ex)
        {
            Utils.AddToFile(logFilePath, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff") + ":" + ex.Message + "\n" + ex.StackTrace, true);
        }
    }

    public class Recovery
    {
        static string recoveryFileName = "recovery";
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

        public static T Recover<T>() where T : class
        {
            string json;
            try
            {
                json = Utils.ReadFile(recoveryFileName);
                T state = JsonConvert.DeserializeObject<T>(json);
                return state;
            }
            catch(Exception ex)
            {
                Logger.Log(ex);
                return null;
            }
        }
    }
    
    internal class Utils
    {
        private static string getFilePath(string fileName)
        {
            return Directory.GetCurrentDirectory() + "\\" + fileName + ".txt";
        }

        internal static void CreateFile(string fileName)
        {
            using (FileStream fs = File.Create(getFilePath(fileName)))
            {}
        }

        internal static bool AddToFile(string fileName, string text, bool append)
        {
            string filePath = getFilePath(fileName);
            try
            {
                using (StreamWriter file = new StreamWriter(filePath, append))
                {
                    file.WriteLine(text);
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Unable to log for some reason. Ignoring the exception.
            }
            return false;
        }

        internal static string ReadFile(string fileName)
        {
            string filePath = getFilePath(fileName);
            return File.ReadAllText(filePath);
        }
    }
}
