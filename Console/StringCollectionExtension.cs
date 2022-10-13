using System;
using System.Collections.Generic;

namespace auto
{
    public static class StringCollectionExtension
    {
        private const string filtredFileEndings = ".dll .exe .pdb";
        private const string filtredDirectories =
            "\\bin " +
            "\\node_modules ";
        public static string[] FilterFileEndings(this string[] files)
        {
            var items = Filter(files, filtredFileEndings, EndsWith);
            return Filter(items, filtredDirectories, Contains);
        }
        public static string[] FilterDirectories(this string[] files)
        {
            return Filter(files, filtredDirectories, Contains);
        }
        public static string GetOrDefault(this string[] args, int position)
        {
            return args.Length > position ? args[position] : string.Empty;
        }
        #region private 
        private static string[] Filter(this string[] files, string blacklist, Func<string, string, bool> func)
        {
            var list = new List<string>();
            foreach (var file in files)
            {
                bool add = true;
                foreach (var str in blacklist.Split(" ", StringSplitOptions.RemoveEmptyEntries))
                {
                    if (func(file, str))
                    {
                        add = false;
                    }
                }
                if (add) list.Add(file);
            }
            return list.ToArray();
        }
        private static bool EndsWith(string str, string str2)
        {
            return str.EndsWith(str2);
        }
        private static bool Contains(string str, string str2)
        {
            return str.Contains(str2);
        }
        #endregion
    }
}
