using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Console
{
    public class FilesWithRows
    {
        private string[] rows = new string[0];
        private string file;
        public FilesWithRows()
        {
            rows = new string[0];
        }

        public string File { get { return $">> {file.Replace(Directory.GetCurrentDirectory(), string.Empty)}"; } set { file = value; } }
        public string[] Rows { get { return rows; } set { rows = value; } }

        public override string ToString() => $"{File}{Environment.NewLine}{string.Join(Environment.NewLine, Rows)}";
    }

    public static class FilesWithRowsExtensions
    {
        public static string Print(this IEnumerable<FilesWithRows> collection) => string.Join($"{Environment.NewLine}{Environment.NewLine}", collection.Select(x => x.ToString()));

    }
}
