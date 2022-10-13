using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace auto
{
    class Program
    {
        static void Main(string[] args)
        {
            ConvertPercentToQuotes(args);

            var output = new ConsoleOutput();

            try
            {
                var fileName = args.GetOrDefault(1);
                var searchText = args.GetOrDefault(2);
                var replacementText = args.GetOrDefault(3);
                OutputIntroText(output, fileName, searchText, replacementText);
                var filesAndRows = Enumerable.Empty<FilesWithRows>();
                var filesController = new FilesController();
                var gitController = new GitController();

                switch (args.First())
                {
                    case "Help":
                    case "?":
                        output.WriteLine($"{Constants.Args.Print()}");
                        break;
                    case Constants.Args.GetFiles:
                        filesAndRows = filesController.GetFilesWithEmptyRows(fileName);
                        output.WriteLine($"{filesAndRows.Print()}");
                        break;
                    case Constants.Args.GetAllFiles:
                        filesAndRows = filesController.GetAllFilesWithEmptyRows();
                        output.WriteLine($"{filesAndRows.Print()}");
                        break;
                    case Constants.Args.GetRowsInFiles:
                        filesAndRows = filesController.SearchInFiles(fileName, searchText);
                        output.WriteLine($"{filesAndRows.Print(onlyFilesWithMatchingRows: true)}");
                        break;
                    case Constants.Args.AddRowFirst:
                        filesAndRows = filesController.AddRowFirst(fileName, searchText);
                        output.WriteLine($"{filesAndRows.Print(onlyFilesWithMatchingRows: true)}");
                        break;
                    case Constants.Args.AddRowLast:
                        filesAndRows = filesController.AddRowLast(fileName, searchText);
                        output.WriteLine($"{filesAndRows.Print(onlyFilesWithMatchingRows: true)}");
                        break;
                    case Constants.Args.ReplaceRow:
                        filesAndRows = filesController.ReplaceRow(fileName, searchText, replacementText);
                        output.WriteLine($"{filesAndRows.Print(onlyFilesWithMatchingRows: true)}");
                        break;
                    case Constants.Args.RemoveText:
                        filesAndRows = filesController.RemoveRow(fileName, searchText);
                        output.WriteLine($"{filesAndRows.Print(onlyFilesWithMatchingRows: true)}");
                        break;
                    case Constants.Args.GitCommit:
                        filesAndRows = gitController.CommitAllChildDirectories(fileName);
                        output.WriteLine($"{filesAndRows.Print()}");
                        break;
                    case Constants.Args.GitPush:
                        filesAndRows = gitController.PushAllChildDirectories();
                        output.WriteLine($"{filesAndRows.Print()}");
                        break;
                    case Constants.Args.GitStatus:
                        filesAndRows = gitController.GetStatusAllChildDirectories();
                        output.WriteLine($"{filesAndRows.Print()}");
                        break;
                    default:
                        throw new Exception(string.Join(", ", args) + " are not valid parameters");
                }
            }
            catch (Exception e)
            {
                if (e.Message == "Index was outside the bounds of the array.") output.WriteLine("Fel antal argument.");
                else output.WriteLine($"Error!{Environment.NewLine}{Environment.NewLine}{e.Message}{Environment.NewLine}{Environment.NewLine}{e.StackTrace}{Environment.NewLine}{Environment.NewLine}");
            }
        }

        private static void ConvertPercentToQuotes(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                args[i] = args[i].Replace("%", "\"");
            }
        }
        #region private
        private static void OutputIntroText(ConsoleOutput output, string fileName, string searchText, string replacementText)
        {
            PrintHey(output);
            output.WriteLine($"{Environment.NewLine}...and welcome to some sort of multi file and git manipulation application!");
            output.WriteLine($"Type \"auto.exe ?\" for help");
            output.WriteLine($"Please mind that if you want to include qoutes (\") in the search string, use percent (%) instead{Environment.NewLine}");
            output.WriteLine($"Search params: {Environment.NewLine}File name: \"{fileName}\"{Environment.NewLine}Search text: \"{searchText}\"{Environment.NewLine}Replacement text: \"{replacementText}\"{Environment.NewLine}");
        }

        private static void PrintHey(ConsoleOutput output)
        {
            output.WriteLine(@"          _____                    _____                _____          ");
            output.WriteLine(@"         /\    \                  /\    \              |\    \         ");
            output.WriteLine(@"        /::\____\                /::\    \             |:\____\        ");
            output.WriteLine(@"       /:::/    /               /::::\    \            |::|   |        ");
            output.WriteLine(@"      /:::/    /               /::::::\    \           |::|   |        ");
            output.WriteLine(@"     /:::/    /               /:::/\:::\    \          |::|   |        ");
            output.WriteLine(@"    /:::/____/               /:::/__\:::\    \         |::|   |        ");
            output.WriteLine(@"   /::::\    \              /::::\   \:::\    \        |::|   |        ");
            output.WriteLine(@"  /::::::\    \   _____    /::::::\   \:::\    \       |::|___|______  ");
            output.WriteLine(@" /:::/\:::\    \ /\    \  /:::/\:::\   \:::\    \      /::::::::\    \ ");
            output.WriteLine(@"/:::/  \:::\    /::\____\/:::/__\:::\   \:::\____\    /::::::::::\____\");
            output.WriteLine(@"\::/    \:::\  /:::/    /\:::\   \:::\   \::/    /   /:::/~~~~/~~      ");
            output.WriteLine(@" \/____/ \:::\/:::/    /  \:::\   \:::\   \/____/   /:::/    /         ");
            output.WriteLine(@"          \::::::/    /    \:::\   \:::\    \      /:::/    /          ");
            output.WriteLine(@"           \::::/    /      \:::\   \:::\____\    /:::/    /           ");
            output.WriteLine(@"           /:::/    /        \:::\   \::/    /    \::/    /            ");
            output.WriteLine(@"          /:::/    /          \:::\   \/____/      \/____/             ");
            output.WriteLine(@"         /:::/    /            \:::\    \                              ");
            output.WriteLine(@"        /:::/    /              \:::\____\                             ");
            output.WriteLine(@"        \::/    /                \::/    /                             ");
            output.WriteLine(@"         \/____/                  \/____/                              ");
        }

        #endregion
    }
}
