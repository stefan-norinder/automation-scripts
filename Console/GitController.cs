using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace auto
{
    public class GitController
    {
        private readonly IFileService fileService;

        public GitController(IFileService fileService=null)
        {
            this.fileService = fileService ?? new FileService();
        }

        public IEnumerable<FilesWithRows> CommitAllChildDirectories(string message)
        {
            return ExecuteCommands(@"git add .", $"git commit -m '{message}'" );
            
        }
        public IEnumerable<FilesWithRows> GetStatusAllChildDirectories()
        {
            return ExecuteCommands(@"git status" );
        }

        public IEnumerable<FilesWithRows> PushAllChildDirectories()
        {
            return ExecuteCommands(@"git push");
        }
        public IEnumerable<FilesWithRows> UndoAllWorkInChildDirectories()
        {
            return ExecuteCommands(@"git checkout .");
        }

        private IEnumerable<FilesWithRows> ExecuteCommands(params string[] commands)
        {
            var result = new List<FilesWithRows>();
            foreach (var directory in fileService.GetAllDirectories())
            {
                using (var powershell = PowerShell.Create())
                {
                    powershell.AddScript($"cd '{directory}'");

                    foreach (var command in commands)
                    {
                        powershell.AddScript(command);
                    }

                    var results = powershell.Invoke();
                    if (results.Any())
                    {
                        result.Add(new FilesWithRows { File = directory, Rows = results.Select(x => x.ToString()).ToArray() });
                    }
                }
            }
            return result;
        }
    }
}
