using auto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace auto
{
    public interface IFileService
    {
        void CreateFile(string fileName, string relativePath, string content = "");
        void DeleteFile(string fileName, string relativePath);
        bool FileExists(string path);
        string[] GetAllDirectories();
        string[] GetAllFiles(string path = "");
        string[] GetFileNames(string fileName, string path = "");
        string Read(string path, bool pathIsRelative = false);
        void Write(string path, string content);

        public void Save(IEnumerable<FilesWithRows> files);
    }

    public class FileService : IFileService
    {
        private IFilesAdapter filesAdapter;
        private IFilesAdapter FilesAdapter { get { if (filesAdapter == null) { filesAdapter = new FilesAdapter(); } return filesAdapter; } }

        public FileService(IFilesAdapter filesAdapter = null)
        {
            this.filesAdapter = filesAdapter ?? new FilesAdapter();
        }

        public string[] GetFileNames(string fileName, string path = "")
        {
            if (string.IsNullOrEmpty(path)) path = FilesAdapter.CurrentDirectory;

            return GetFilesFiltred(fileName, path);
        }

        public string[] GetAllDirectories()
        {
            string[] directories = filesAdapter.GetDirectories(filesAdapter.CurrentDirectory);
            return directories.FilterDirectories();
        }

        public string[] GetAllFiles(string path = "")
        {
            if (string.IsNullOrEmpty(path)) path = FilesAdapter.CurrentDirectory;

            return GetFilesFiltred(string.Empty, path);
        }

        public void CreateFile(string fileName, string relativePath, string content = "")
        {
            string path = $"{FilesAdapter.CurrentDirectory}/{relativePath}/{fileName}";
            using (var fileStream = File.Create(path))
            {
                var info = new UTF8Encoding(true).GetBytes(content);

                fileStream.Write(info, 0, info.Length);
            }
        }
        public void Write(string path, string content)
        {
            using (var fileStream = File.OpenWrite(path))
            {
                content = content.ReplaceText("\\r\\n", Environment.NewLine);
                var info = new UTF8Encoding(true).GetBytes(content);

                fileStream.Write(info, 0, info.Length);
            }
        }

        public void DeleteFile(string fileName, string relativePath)
        {
            File.Delete($"{FilesAdapter.CurrentDirectory}/{relativePath}/{fileName}");
        }

        public bool FileExists(string path)
        {
            return File.Exists($"{FilesAdapter.CurrentDirectory}/{path}");
        }

        public string Read(string path, bool pathIsRelative = false)
        {
            return pathIsRelative ? File.ReadAllText($"{FilesAdapter.CurrentDirectory}/{path}") : File.ReadAllText(path);
        }

        public void Save(IEnumerable<FilesWithRows> files)
        {
            foreach (var fileAndRows in files)
            {
                Write(fileAndRows.File, fileAndRows.Rows.RowsToString());
            }
        }

        #region private 

        private string[] GetFilesFiltred(string fileName, string path)
        {
            return FilesAdapter.GetFiles(path, $"{fileName}").FilterFileEndings();
        }

        #endregion
    }
}
