using System;
using System.Collections.Generic;
using System.Linq;

namespace Console
{
    public static class StringExtensions
    {
        public static string AddRowFirst(this string content, string row)
        {
            return InsertRowAtPosition(content, row, 1);
        }

        public static string AddRowLast(this string content, string row)
        {
            return InsertRowAtPosition(content, row, NumberOfRows(content) + 1);
        }

        public static string InsertRowAtPosition(this string content, string row, int position)
        {
            List<string> list = GetContentAsListOfRows(content);
            list.Insert(position - 1, row);
            return string.Join(Environment.NewLine, list);
        }

        public static string GetRowByIndex(this string content, int position)
        {
            List<string> list = GetContentAsListOfRows(content);
            string row = list.ElementAt(position - 1);
            if (string.IsNullOrEmpty(row)) return string.Empty;
            return row.Trim();
        }

        public static string[] GetRowsBySearch(this string content, string search)
        {
            var result = new List<string>();
            List<string> list = GetContentAsListOfRows(content);
            for (int i = 0; i < list.Count(); i++)
            {
                if (list[i].Contains(search.Trim(), StringComparison.InvariantCultureIgnoreCase)) result.Add($"[{i+1}] {list[i].Trim()}");
            }
            return result.ToArray();
        }

        public static int FindRowNumberOfFirstInstanceOf(this string content, string search)
        {
            var list = GetContentAsListOfRows(content);
            for (int i = 0; i < list.Count(); i++)
            {
                if (string.Equals(list[i].Trim(), search.Trim(), StringComparison.InvariantCultureIgnoreCase)) return i + 1;
            }
            throw new SearchStringNotFoundException(search);
        }

        private static List<string> GetContentAsListOfRows(this string content)
        {
            var array = content.Split(Environment.NewLine);
            return array.ToList();
        }

        private static int NumberOfRows(this string content)
        {
            var array = content.Split(Environment.NewLine);
            return array.Count();
        }
    }
}
