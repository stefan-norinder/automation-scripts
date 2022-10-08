using System;
using System.Linq;

namespace Console
{
    public static class ContentHelpers
    {
        public static string AddRowFirst(string content, string row)
        {
            var array = content.Split(Environment.NewLine);
            var list = array.ToList();
            list.Insert(0, row);
            return string.Join(Environment.NewLine, list);
        }        
    }
}
