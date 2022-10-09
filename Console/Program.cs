using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var filesAndRows = Enumerable.Empty<FilesWithRows>();
                var filesController = new FilesController();
                var gitController = new GitController();

                switch (args.First())
                {
                    case "Help":
                    case "?":
                        System.Console.WriteLine($"{Constants.Args.Print()}");
                        break;
                    case Constants.Args.GetFiles:
                        filesAndRows = filesController.GetFilesBySearchString(args[1]);
                        System.Console.WriteLine($"{filesAndRows.Print()}");
                        break;
                    case Constants.Args.GetAllFiles:
                        filesAndRows = filesController.GetAllFiles();
                        System.Console.WriteLine($"{filesAndRows.Print()}");
                        break;
                    case Constants.Args.GetRows:
                        filesAndRows = filesController.GetRowsBySearchString(args[1]);
                        System.Console.WriteLine($"{filesAndRows.Print()}");
                        break;
                    case Constants.Args.AddRowFirst:
                        filesAndRows = filesController.AddRowFirst(args[1], args[2]);
                        System.Console.WriteLine($"{filesAndRows.Print()}");
                        break;
                    case Constants.Args.AddRowLast:
                        filesAndRows = filesController.AddRowLast(args[1], args[2]);
                        System.Console.WriteLine($"{filesAndRows.Print()}");
                        break;
                    case Constants.Args.ReplaceText:
                        filesAndRows = filesController.ReplaceText(args[1], args[2], args[3]);
                        System.Console.WriteLine($"{filesAndRows.Print()}");
                        break;
                    case Constants.Args.RemoveText:
                        filesAndRows = filesController.RemoveText(args[1], args[2]);
                        System.Console.WriteLine($"{filesAndRows.Print()}");
                        break;
                    case Constants.Args.GitStatus:
                        System.Console.WriteLine($"{gitController.CheckStatus(args[1])}");
                        break;
                    default:
                        throw new Exception(string.Join(", ", args) + " are not valid parameters");
                }
            }
            catch (Exception e)
            {
                if (e.Message == "Index was outside the bounds of the array.") System.Console.WriteLine("Fel antal argument.");
                else System.Console.WriteLine($"Error!{Environment.NewLine}{Environment.NewLine}{e.Message}{Environment.NewLine}{Environment.NewLine}{e.StackTrace}{Environment.NewLine}{Environment.NewLine}");
            }
        }
    }
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
            public const string GetRows = "get-rows";
            public const string AddRowFirst = "add-row-first";
            public const string AddRowLast = "add-row-last";
            public const string ReplaceText = "replace-text";
            public const string RemoveText = "remove-text";
            public const string GitStatus = "git-status";
        }
    }
}
