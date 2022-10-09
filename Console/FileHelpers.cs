using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Console
{
    public static class FileHelpers
    {
        public static string[] GetFilesRecursiveByName(string fileName, string path = "")
        {
            if (string.IsNullOrEmpty(path)) path = CurrentDirectory;

            return GetFilesFiltred(fileName, path);
        }

        public static string[] GetAllFiles(string path = "")
        {
            if (string.IsNullOrEmpty(path)) path = CurrentDirectory;

            return GetFilesFiltred(string.Empty, path);
        }

        public static void CreateFile(string fileName, string relativePath, string content = "")
        {
            string path = $"{CurrentDirectory}/{relativePath}/{fileName}";
            using (var fileStream = File.Create(path))
            {
                var info = new UTF8Encoding(true).GetBytes(content);

                fileStream.Write(info, 0, info.Length);
            }
        }
        public static void Write(string path, string content)
        {
            using (var fileStream = File.OpenWrite(path))
            {
                content = content.ReplaceText("\\r\\n", Environment.NewLine);
                var info = new UTF8Encoding(true).GetBytes(content);

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

        public static string Read(string path, bool pathIsRelative = false)
        {
            return pathIsRelative ? File.ReadAllText($"{CurrentDirectory}/{path}") : File.ReadAllText(path);
        }

        #region private 

        private static string CurrentDirectory => Directory.GetCurrentDirectory();

        private static string[] GetFilesFiltred(string fileName, string path)
        {
            return Directory.GetFiles(path, $"*{fileName}*", SearchOption.AllDirectories).Filter();
        }

        #endregion
    }
    public static class StringCollectionExtension
    {
        private const string filtredFileEndings = ".dll .exe .pdb";
        public static string[] Filter(this string[] files)
        {
            var list = new List<string>();
            foreach (var file in files)
            {
                bool add = true;
                foreach (var str in filtredFileEndings.Split(" "))
                {
                    if (file.EndsWith(str))
                    {
                        add = false;
                    }
                }
                if (add) list.Add(file);
                
            }
            return list.ToArray();
        }
    }
}
