using System;
using System.IO;
using System.Text;

namespace Logger
{
    public class Logger
    {
        private static string logFilePath;
        public static bool CreateLogFile(string moduleName, string directory = null)
        {
            string currentDirectoryPath;
            if (directory != null)
            {
                currentDirectoryPath = directory;
            }
            else
            {
                try
                {
                    currentDirectoryPath = Directory.GetCurrentDirectory();
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine("Couldn't start the application as you do not have permission to read in the current directory");
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unhandled exception occured.");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    return false;
                }
            }

            string logDirectoryPath = currentDirectoryPath + "\\log";
            if (!Directory.Exists(logDirectoryPath))
            {
                try
                {
                    Directory.CreateDirectory(logDirectoryPath);
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine("Couldn't start the application as you do not have permission to create files in the directory "
                        + logDirectoryPath);
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unhandled exception occured.");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    return false;
                }
            }

            logFilePath = logDirectoryPath + "\\" + moduleName + ".log";

            try
            {
                using (FileStream fs = File.Create(logFilePath))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes("Log Initialised\n");
                    fs.Write(info, 0, info.Length);
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Couldn't start the application as you do not have permission to create files in the directory "
                    + logDirectoryPath);
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

        public static void Log(string text)
        {
            try
            {
                using (StreamWriter file = new StreamWriter(logFilePath, true))
                {
                    file.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff") + ":" + text);
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine("Unable to log. Exiting the game...");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }

        public static void Log(Exception ex)
        {
            try
            {
                using (StreamWriter file = new StreamWriter(logFilePath, true))
                {
                    file.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff") + ":" + ex.Message + "\n" + ex.StackTrace);
                }
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Unable to log. Exiting the game...");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }
    }
}
