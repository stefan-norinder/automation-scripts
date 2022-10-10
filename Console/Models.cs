using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Console
{
    public class FilesWithRows
    {
        private string[] rows;
        private string file;
        public FilesWithRows()
        {
            rows = Array.Empty<string>();
        }
        public string File { get { return file; } set { file = value; } }

        public string OutputFileInfo() => $">> {file.Replace(Directory.GetCurrentDirectory(), string.Empty)}";
        public string[] Rows { get { return rows; } set { rows = value; } }

        public override string ToString() => $"{OutputFileInfo()}{Environment.NewLine}{string.Join(Environment.NewLine, Rows)}";
    }

    public static class FilesWithRowsExtensions
    {

        public static void AddRowFirst(this IEnumerable<FilesWithRows> collection, string file, string row)
        {
            AddRowAtPoistion(ref collection, file, row, GetFirst);
        }
        public static void AddRowLast(this IEnumerable<FilesWithRows> collection, string file, string row)
        {
            AddRowAtPoistion(ref collection, file, row, GetLast);
        }
        public static void ReplaceRow(this IEnumerable<FilesWithRows> collection, string file, string row, string newRow)
        {
            foreach (var filesWithRows in collection.Where(x => x.File == file))
            {
                for (int i = 0; i < filesWithRows.Rows.Length; i++)
                {
                    if (filesWithRows.Rows[i] == row)
                    {
                        filesWithRows.Rows[i] = newRow;
                    }
                }
            }
        }

        private static void AddRowAtPoistion(ref IEnumerable<FilesWithRows> collection, string file, string row, Func<int, int> position)
        {
            foreach (var filesWithRows in collection.Where(x => x.File == file))
            {                
                var rows = filesWithRows.Rows.ToList();
                rows.Insert(position(rows.Count()), row);
                filesWithRows.Rows = rows.ToArray();
            }
        }

        private static int GetFirst(int length) => 0;
        private static int GetLast(int length) => length;

        public static string Print(this IEnumerable<FilesWithRows> collection) => string.Join($"{Environment.NewLine}{Environment.NewLine}", collection.Select(x => x.ToString()));


    }
}
