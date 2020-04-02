using System.IO;

namespace Logger
{
    internal class Utils
    {
        private static string getFilePath(string fileName)
        {
            return Directory.GetCurrentDirectory() + "\\" + fileName + ".txt";
        }

        internal static void CreateFile(string fileName)
        {
            using (FileStream fs = File.Create(getFilePath(fileName)))
            { }
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
            catch
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

