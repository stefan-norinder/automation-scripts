using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace Console
{
    public class GitController
    {
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

        private IEnumerable<FilesWithRows> ExecuteCommands(params string[] commands)
        {
            var result = new List<FilesWithRows>();
            foreach (var directory in FileHelpers.GetAllDirectories())
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
