using System;
using System.Collections.Generic;
using System.Linq;

namespace Console
{
    public static class ContentHelpers
    {
        public static string AddRowFirst(string content, string row)
        {
            return InsertRowAtPosition(content, row, 0);
        }

        public static string AddRowLast(string content, string row)
        {
            return InsertRowAtPosition(content, row, NumberOfRows(content));
        }

        public static string InsertRowAtPosition(string content, string row, int position)
        {
            List<string> list = GetContentAsListOfRows(content);
            list.Insert(position, row);
            return string.Join(Environment.NewLine, list);
        }

        private static List<string> GetContentAsListOfRows(string content)
        {
            var array = content.Split(Environment.NewLine);
            return array.ToList();
        }

        private static int NumberOfRows(string content)
        {
            var array = content.Split(Environment.NewLine);
            return array.Count();
        }
    }
}
