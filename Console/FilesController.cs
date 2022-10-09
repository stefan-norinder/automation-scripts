using System;
using System.Collections.Generic;
using System.Linq;

namespace Console
{
    public class FilesController
    {
        public IEnumerable<FilesWithRows> GetRowsBySearchString(string searchString)
        {
            var filesWithRows = new List<FilesWithRows>();
            var files = FileHelpers.GetAllFiles();
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

        public IEnumerable<FilesWithRows> GetAllFiles()
        {
            var files = FileHelpers.GetAllFiles();
            return files.Select(x => new FilesWithRows { File = x });
        }

        public IEnumerable<FilesWithRows> GetFilesBySearchString(string searchString)
        {
            var files = FileHelpers.GetFilesRecursiveByName(searchString);
            return files.Select(x => new FilesWithRows { File = x });
        }

        public IEnumerable<FilesWithRows> AddRowFirst(string fileName, string textToAdd)
        {
            return AddRow(fileName, textToAdd, StringExtensions.AddRowFirst);
        }

        public IEnumerable<FilesWithRows> AddRowLast(string fileName, string textToAdd)
        {
            return AddRow(fileName, textToAdd, StringExtensions.AddRowLast);
        }

        private IEnumerable<FilesWithRows> AddRow(string fileName, string textToAdd, Func<string,string,string> func)
        {
            var filesAndRows = new List<FilesWithRows>();
            var files = FileHelpers.GetFilesRecursiveByName(fileName);
            foreach (var file in files)
            {
                var content = FileHelpers.Read(file);
                content = func(content,textToAdd);
                FileHelpers.Write(file, content);
                filesAndRows.Add(new FilesWithRows { File = file, Rows = content.ToRows() });
            }
            return filesAndRows;
        }
    }
}
