using System;
using System.Collections.Generic;
using System.Linq;

namespace auto
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

        public static string ReplaceText(this string content, string textToReplace, string newText)
        {
            return content.Replace(textToReplace, newText);
        }

        public static string RemoveText(this string content, string textToReplace)
        {
            return content.Replace(textToReplace, string.Empty);
        }

        public static string InsertRowAtPosition(this string content, string row, int position)
        {
            var list = ToListOfRows(content);
            list.Insert(position - 1, row);
            return string.Join(Environment.NewLine, list);
        }

        public static string GetRowByIndex(this string content, int position)
        {
            var list = ToListOfRows(content);
            string row = list.ElementAt(position - 1);
            if (string.IsNullOrEmpty(row)) return string.Empty;
            return row.Trim();
        }
        public static string[] ToRowsWithLineNumber(this string content)
        {
            var rows = content.ToListOfRows();
            for (int i = 0; i < rows.Count; i++)
            {
                rows[i] = rows[i].AddRowNumber(i + 1);
            }

            return rows.ToArray();
        }

        public static string[] GetRowsBySearch(this string content, string search)
        {
            var result = new List<string>();
            var list = ToListOfRows(content);
            for (int i = 0; i < list.Count(); i++)
            {
                if (search.StartsWith("*") && search.EndsWith("*"))
                {
                    if (list[i].Trim().Contains(search.Trim().Replace("*", ""), StringComparison.InvariantCultureIgnoreCase)) result.AddRow(list[i], i + 1);
                }
                else if (search.EndsWith("*"))
                {
                    if (list[i].Trim().StartsWith(search.Trim().Replace("*", ""), StringComparison.InvariantCultureIgnoreCase)) result.AddRow(list[i], i + 1);
                }
                else if (search.StartsWith("*"))
                {
                    if (list[i].Trim().EndsWith(search.Trim().Replace("*", ""), StringComparison.InvariantCultureIgnoreCase)) result.AddRow(list[i], i + 1);
                }
                else if (string.Equals(list[i].Trim(), search.Trim().Replace("*", ""), StringComparison.InvariantCultureIgnoreCase)) result.AddRow(list[i], i + 1);
            }
            return result.ToArray();
        }

        public static int FindRowNumberOfFirstInstanceOf(this string content, string search)
        {
            var list = content.ToListOfRows();
            for (int i = 0; i < list.Count(); i++)
            {
                if (string.Equals(list[i].Trim(), search.Trim().Replace("*", ""), StringComparison.InvariantCultureIgnoreCase)) return i + 1;
            }
            throw new SearchStringNotFoundException(search);
        }

        public static string RowsToString(this string[] rows)
        {
            return string.Join(Environment.NewLine, rows);
        }

        public static void AddRow(this List<string> collection, string item, int rowNumber)
        {
            collection.Add(item.AddRowNumber(rowNumber));
        }
        public static string AddRowNumber(this string str, int rowNumber)
        {
            return $"[{rowNumber}] {str.Trim()}";
        }

        public static List<string> ToListOfRows(this string content)
        {
            if (string.IsNullOrEmpty(content)) return Enumerable.Empty<string>().ToList();
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
