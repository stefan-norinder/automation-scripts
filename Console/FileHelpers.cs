using System.IO;

namespace Console
{
    public static class FileHelpers
    {
        public static string[] GetFilesRecursiveByName(string fileName, string path = "")
        {
            if (string.IsNullOrEmpty(path)) path = Directory.GetCurrentDirectory();
            
            return  Directory.GetFiles(path,$"*{fileName}", SearchOption.AllDirectories);
        }
    }
}
