using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace auto
{
    public static class Constants
    {
        public static class Args
        {
            public static string Print()
            {
                var stringBuilder = new StringBuilder();
                var type = typeof(Args);
                stringBuilder.Append(Environment.NewLine);
                stringBuilder.Append($"Args:");
                stringBuilder.Append(Environment.NewLine);
                foreach (var p in type.GetFields(BindingFlags.Public | BindingFlags.Static |BindingFlags.FlattenHierarchy).Where(fi => fi.IsLiteral && !fi.IsInitOnly).ToList())
                {
                    stringBuilder.Append($"{Environment.NewLine}{p.GetValue(null)}{Environment.NewLine}");
                }
                return stringBuilder.ToString();
            }

            public const string GetFiles = "get-files";
            public const string GetAllFiles = "get-all-files";
            public const string GetRowsInFiles = "get-rows";
            public const string AddRowFirst = "add-row-first";
            public const string AddRowLast = "add-row-last";
            public const string ReplaceRow = "replace-row";
            public const string RemoveText = "remove-row";
            public const string GitCommit = "git-commit";
            public const string GitPush = "git-push";
            public const string GitStatus = "git-status";
            public const string GitUndo = "git-undo";
        }
    }
}
