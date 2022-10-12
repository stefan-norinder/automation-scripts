using System.IO;

namespace auto
{
    public interface IFilesAdapter
    {
        string[] GetFilesFiltred(string fileName, string path);
        string CurrentDirectory { get; }
        string[] GetDirectories(string root);
        string[] GetFiles(string path, string fileName);
    }

    public class FilesAdapter : IFilesAdapter
    {
        public string[] GetFilesFiltred(string fileName, string path) => Directory.GetFiles(path, $"{fileName}", SearchOption.AllDirectories);

        public string[] GetFiles(string path, string fileName) => Directory.GetFiles(path, $"{fileName}", SearchOption.AllDirectories);

        public string[] GetDirectories(string root) => Directory.GetDirectories(root);

        public string CurrentDirectory => Directory.GetCurrentDirectory();

    }
}
