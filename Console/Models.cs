using System;
using System.Collections.Generic;
using System.Linq;

namespace Console
{
    public class FilesWithRows
    {
        public FilesWithRows()
        {
            Rows = new string[0];
        }

        public string File { get; set; }
        public string[] Rows { get; set; }

        public override string ToString() => $"{File}{Environment.NewLine}{string.Join(Environment.NewLine, Rows)}";
    }

    public static class FilesWithRowsExtensions
    {
        public static string Print(this IEnumerable<FilesWithRows> collection) => string.Join($"{Environment.NewLine}{Environment.NewLine}", collection.Select(x => x.ToString()));

    }
}
