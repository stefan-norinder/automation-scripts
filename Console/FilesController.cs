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

        /// <summary>
        /// Returns files with rows where the search string is found
        /// </summary>
        /// <param name="searchString">String  to search for in rows</param>
        /// <returns>Files and rows where searchstring i present</returns>
        public IEnumerable<FilesWithRows> SearchInAllFiles(string searchString)
        {
            var files = fileService.GetAllFiles();
            return SearchInFiles(searchString, files);
        }
        /// <summary>
        /// Returns files with rows where the search string is found
        /// </summary>
        /// <param name="fileFilter">Files to search in, use of wildcards (*) are valid</param>
        /// <param name="searchString">String  to search for in rows</param>
        /// <returns>Files and rows where searchstring i present</returns>
        public IEnumerable<FilesWithRows> SearchInFiles(string fileFilter, string searchString)
        {
            var files = fileService.GetFileNames(fileFilter);
            return SearchInFiles(searchString, files);
        }

        public IEnumerable<FilesWithRows> GetAllFilesWithEmptyRows()
        {
            var files = fileService.GetAllFiles();
            return files.Select(x => new FilesWithRows { File = x });
        }

        public IEnumerable<FilesWithRows> GetFilesWithEmptyRows(string searchString)
        {
            var files = fileService.GetFileNames(searchString);
            return files.Select(x => new FilesWithRows { File = x });
        }
        /// <summary>
        /// Adds a row to all files matching the fileName
        /// </summary>
        /// <param name="fileName">Files to update, use of wildcards (*) are valid</param>
        /// <param name="textToAdd">Row to add</param>
        /// <returns>Collection of files with updated row</returns>
        public IEnumerable<FilesWithRows> AddRowFirst(string fileName, string textToAdd)
        {
            var files = SearchForFilesByFileName(fileName); 
            files.AddRowFirst(textToAdd);
            fileService.Save(files);
            return files;
        }

        /// <summary>
        /// Adds a row to all files matching the fileName
        /// </summary>
        /// <param name="fileName">Files to update, use of wildcards (*) are valid</param>
        /// <param name="textToAdd">Row to add</param>
        /// <returns>Collection of files with updated row</returns>
        public IEnumerable<FilesWithRows> AddRowLast(string fileName, string textToAdd)
        {
            var files = SearchForFilesByFileName(fileName);
            files.AddRowLast(textToAdd);
            fileService.Save(files);
            return files;
        }

        /// <summary>
        /// Replaces a row in all files matching the fileName
        /// </summary>
        /// <param name="fileName">Files to update, use of wildcards (*) are valid</param>
        /// <param name="textToReplace">Text to remove</param>
        /// <param name="newText">Text to replace the removed text with</param>
        /// <returns>Collection of files with updated row</returns>
        public IEnumerable<FilesWithRows> ReplaceRow(string fileName, string textToReplace, string newText)
        {
            var files = SearchForFilesByFileName(fileName);
            files.ReplaceRow(textToReplace, newText);
            fileService.Save(files);
            return files;
        }
        /// <summary>
        /// Remove a row in all files matching the fileName
        /// </summary>
        /// <param name="fileName">Files to update, use of wildcards (*) are valid</param>
        /// <param name="textToRemove">Rows containing this text will be removed</param>
        /// <returns>Collection of files with updated row</returns>
        public IEnumerable<FilesWithRows> RemoveRow(string fileName, string textToRemove)
        {
            var files = SearchForFilesByFileName(fileName);
            files.RemoveRow(textToRemove);
            fileService.Save(files);
            return files;
        }
        #region private
        private IEnumerable<FilesWithRows> SearchForFilesByFileName(string fileName)
        {
            var filenames = fileService.GetFileNames(fileName);
            var files = GetFilesWithRows(filenames);
            return files;
        }
        private IEnumerable<FilesWithRows> GetFilesWithRows(string[] files)
        {
            var filesWithRows = new List<FilesWithRows>();
            foreach (var file in files)
            {
                var content = fileService.Read(file);
                var rows = content.ToListOfRows();
                filesWithRows.Add(new FilesWithRows { File = file, Rows = rows.ToArray() });
            }
            return filesWithRows;
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
        #endregion
    }
}
