using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace Logger
{
    public class Logger
    {
        static string logFilePath;

        /// <summary>
        /// Creates a log file by the Module name
        /// </summary>
        /// <param name="moduleName">Name of the log file</param>
        /// <returns></returns>
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
}
