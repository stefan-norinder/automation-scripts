using System.IO;
using System.Text;

namespace Console
{
    public static class FileHelpers
    {
        public static string[] GetFilesRecursiveByName(string fileName, string path = "")
        {
            if (string.IsNullOrEmpty(path)) path = CurrentDirectory;

            return Directory.GetFiles(path, $"*{fileName}", SearchOption.AllDirectories);
        }

        public static void CreateFile(string fileName, string relativePath, string content = "")
        {
            string path = $"{CurrentDirectory}/{relativePath}/{fileName}";
            using (var fileStream = File.Create(path))
            {
                var  info = new UTF8Encoding(true).GetBytes(content);

                fileStream.Write(info, 0, info.Length);
            }
        }

        public static void DeleteFile(string fileName, string relativePath)
        {
            File.Delete($"{CurrentDirectory}/{relativePath}/{fileName}");
        }

        public static bool FileExists(string path)
        {
            return File.Exists($"{CurrentDirectory}/{path}");
        }

        #region private 

        private static string CurrentDirectory => Directory.GetCurrentDirectory();
        
        #endregion
    }
}
