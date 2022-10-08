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

        public static string InsertRowAtPosition(this string content, string row, int position)
        {
            List<string> list = GetContentAsListOfRows(content);
            list.Insert(position -1, row);
            return string.Join(Environment.NewLine, list);
        }

        public static int FindRowNumber(this string content, string search)
        {
            var list = GetContentAsListOfRows(content);
            for (int i = 0; i < list.Count(); i++)
            {
                if (list[i] == search) return i + 1;
            }
            throw new Exception($"Unable to find string {search}");
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
