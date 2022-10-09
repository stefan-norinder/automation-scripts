using System;
using System.Linq;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var filesAndRows = Enumerable.Empty<FilesWithRows>();
                var controller = new FilesController();
                switch (args.First())
                {
                    case "get-files":
                        filesAndRows = controller.GetFilesBySearchString(args[1]);
                        System.Console.WriteLine($"{filesAndRows.Print()}");
                        break;
                    case "get-all-files":
                        filesAndRows = controller.GetAllFiles();
                        System.Console.WriteLine($"{filesAndRows.Print()}");
                        break;
                    case "get-rows":
                        filesAndRows = controller.GetRowsBySearchString(args[1]);
                        System.Console.WriteLine($"{filesAndRows.Print()}");
                        break;
                    default:
                        throw new Exception(string.Join(", ", args) + " are not valid parameters");
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine($"Error!{Environment.NewLine}{Environment.NewLine}{e.Message}{Environment.NewLine}{Environment.NewLine}{e.StackTrace}{Environment.NewLine}{Environment.NewLine}");
            }
        }
    }
}
