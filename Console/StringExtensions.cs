﻿using System;
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

        public static string ReplaceText(this string content, string textToReplace, string newText)
        {
            System.Console.WriteLine($"{textToReplace} > {newText}");
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
        public static string[] ToRows(this string content)
        {
            var rows = content.ToListOfRows();
            for (int i = 0; i < rows.Count; i++)
            {
                rows[i] = rows[i].AddRowNumber(i+1);
            }

            return rows.ToArray();
        }

        public static string[] GetRowsBySearch(this string content, string search)
        {
            var result = new List<string>();
            var list = ToListOfRows(content);
            for (int i = 0; i < list.Count(); i++)
            {
                if (list[i].Contains(search.Trim(), StringComparison.InvariantCultureIgnoreCase)) result.AddRow(list[i], i +1);
            }
            return result.ToArray();
        }

        public static int FindRowNumberOfFirstInstanceOf(this string content, string search)
        {
            var list = content.ToListOfRows();
            for (int i = 0; i < list.Count(); i++)
            {
                if (string.Equals(list[i].Trim(), search.Trim(), StringComparison.InvariantCultureIgnoreCase)) return i + 1;
            }
            throw new SearchStringNotFoundException(search);
        }

        public static void AddRow(this List<string> collection, string item, int rowNumber)
        {
            collection.Add(item.AddRowNumber(rowNumber));
        }
        public static string AddRowNumber(this string str, int rowNumber)
        {
            return $"[{rowNumber}] {str.Trim()}";
        }

        private static List<string> ToListOfRows(this string content)
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
