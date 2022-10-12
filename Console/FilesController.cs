using System;
using System.Collections.Generic;
using System.Linq;

namespace auto
{
    public class FilesController
    {
        private readonly IFileService fileService;

        public FilesController(IFileService fileService = null)
        {
            this.fileService = fileService ?? new FileService();
        }

        public IEnumerable<FilesWithRows> SearchInAllFiles(string searchString)
        {
            var files = fileService.GetAllFiles();
            return SearchInFiles(searchString, files);
        }
        public IEnumerable<FilesWithRows> SearchInFiles(string fileFilter, string searchString)
        {
            var files = fileService.GetFilesRecursiveByName(fileFilter);
            return SearchInFiles(searchString, files);
        }

        public IEnumerable<FilesWithRows> GetAllFiles()
        {
            var files = fileService.GetAllFiles();
            return files.Select(x => new FilesWithRows { File = x });
        }

        public IEnumerable<FilesWithRows> GetFiles(string searchString)
        {
            var files = fileService.GetFilesRecursiveByName(searchString);
            return files.Select(x => new FilesWithRows { File = x });
        }

        public IEnumerable<FilesWithRows> AddRowFirst(string fileName, string textToAdd)
        {
            var files = SearchInFiles(fileName, textToAdd);
            files.AddRowFirst(fileName);
            fileService.Save(files);
            return files;
        }

        public IEnumerable<FilesWithRows> AddRowLast(string fileName, string textToAdd)
        {
            var files = SearchInFiles(fileName, textToAdd);
            files.AddRowLast(fileName);
            fileService.Save(files);
            return files;
        }

        public IEnumerable<FilesWithRows> ReplaceRow(string fileName, string textToReplace, string newText)
        {
            var files = SearchInFiles(fileName, textToReplace);
            files.ReplaceRow(textToReplace, newText);
            fileService.Save(files);
            return files;
        }

        public IEnumerable<FilesWithRows> RemoveRow(string fileName, string textToRemove)
        {
            var files = SearchInFiles(fileName, textToRemove);
            files.RemoveRow(textToRemove);
            fileService.Save(files);
            return files;
        }

        private IEnumerable<FilesWithRows> SearchInFiles(string searchString, string[] files)
        {
            var filesWithRows = new List<FilesWithRows>();
            foreach (var file in files)
            {
                var content = fileService.Read(file);
                var rows = content.GetRowsBySearch(searchString);
                filesWithRows.Add(new FilesWithRows { File = file, Rows = rows });
            }
            return filesWithRows;
        }

        private IEnumerable<FilesWithRows> ManipulateRow(string fileName, string text, Func<string, string, string> func, string newText = "", Func<string, string, string, string> func2 = null)
        {
            var filesAndRows = new List<FilesWithRows>();
            var files = fileService.GetFilesRecursiveByName(fileName);
            foreach (var file in files)
            {
                var content = fileService.Read(file);
                content = func != null ? func(content, text) : func2(content, text, newText);
                fileService.Write(file, content);
                filesAndRows.Add(new FilesWithRows { File = file, Rows = content.ToRows() });
            }
            return filesAndRows;
        }
    }
}
