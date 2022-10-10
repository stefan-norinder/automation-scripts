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
            return ManipulateRow(fileName, textToAdd, StringExtensions.AddRowFirst);
        }

        public IEnumerable<FilesWithRows> AddRowLast(string fileName, string textToAdd)
        {
            return ManipulateRow(fileName, textToAdd, StringExtensions.AddRowLast);
        }

        public IEnumerable<FilesWithRows> ReplaceText(string fileName, string textToReplace, string newText)
        {
            return ManipulateRow(fileName, textToReplace, null, newText, StringExtensions.ReplaceText);
        }

        public IEnumerable<FilesWithRows> ReplaceRow(string fileName, string textToReplace, string newText)
        {
            return ManipulateRow(fileName, textToReplace, null, newText, StringExtensions.ReplaceText);
        }

        public IEnumerable<FilesWithRows> RemoveText(string fileName, string textToRemove)
        {
            return ManipulateRow(fileName, textToRemove, StringExtensions.RemoveText);
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
