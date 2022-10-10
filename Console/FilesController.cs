using System;
using System.Collections.Generic;
using System.Linq;

namespace Console
{
    public class FilesController
    {
        public IEnumerable<FilesWithRows> SearchInAllFiles(string searchString)
        {
            var files = FileHelpers.GetAllFiles();
            return SearchInFiles(searchString, files);
        }
        public IEnumerable<FilesWithRows> SearchInFiles(string fileFilter, string searchString)
        {
            var files = FileHelpers.GetFilesRecursiveByName(fileFilter);
            return SearchInFiles(searchString, files);
        }

        public IEnumerable<FilesWithRows> GetAllFiles()
        {
            var files = FileHelpers.GetAllFiles();
            return files.Select(x => new FilesWithRows { File = x });
        }

        public IEnumerable<FilesWithRows> GetFiles(string searchString)
        {
            var files = FileHelpers.GetFilesRecursiveByName(searchString);
            return files.Select(x => new FilesWithRows { File = x });
        }

        public IEnumerable<FilesWithRows> AddRowFirst(string fileName, string textToAdd)
        {
            var files = SearchInFiles(fileName, textToAdd);
            files.AddRowFirst(fileName);
            files.Save();
            return files;
        }

        public IEnumerable<FilesWithRows> AddRowLast(string fileName, string textToAdd)
        {
            var files = SearchInFiles(fileName, textToAdd);
            files.AddRowLast(fileName);
            files.Save();
            return files;
        }

        public IEnumerable<FilesWithRows> ReplaceRow(string fileName, string textToReplace, string newText)
        {
            var files = SearchInFiles(fileName, textToReplace);
            files.ReplaceRow(textToReplace,newText);
            files.Save();
            return files;
        }

        public IEnumerable<FilesWithRows> RemoveRow(string fileName, string textToRemove)
        {
            var files = SearchInFiles(fileName, textToRemove);
            files.RemoveRow(textToRemove);
            files.Save();
            return files;
        }

        private IEnumerable<FilesWithRows> SearchInFiles(string searchString, string[] files)
        {
            var filesWithRows = new List<FilesWithRows>();
            foreach (var file in files)
            {
                var content = FileHelpers.Read(file);
                var rows = content.GetRowsBySearch(searchString);
                if (rows.Any())
                {
                    filesWithRows.Add(new FilesWithRows { File = file, Rows = rows });
                }
            }
            return filesWithRows;
        }

        private IEnumerable<FilesWithRows> ManipulateRow(string fileName, string text, Func<string, string, string> func, string newText = "", Func<string, string, string,string> func2 = null)
        {
            var filesAndRows = new List<FilesWithRows>();
            var files = FileHelpers.GetFilesRecursiveByName(fileName);
            foreach (var file in files)
            {
                var content = FileHelpers.Read(file);
                content = func != null ? func(content,text) : func2(content,text,newText);
                FileHelpers.Write(file, content);
                filesAndRows.Add(new FilesWithRows { File = file, Rows = content.ToRows() });
            }
            return filesAndRows;
        }
    }
}
