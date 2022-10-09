using System.Management.Automation;

namespace Console
{
    public class GitController
    {
        public void CommitAllChildDirectories(string message)
        {
            foreach (var directory in FileHelpers.GetAllDirectories())
                using (var powershell = PowerShell.Create())
                {
                    powershell.AddScript($"cd '{directory}'");

                    powershell.AddScript(@"git add .");
                    powershell.AddScript($"git commit -m '{message}'");

                    var results = powershell.Invoke();
                }
        }

        public void PushAllChildDirectories(string message)
        {
            foreach (var directory in FileHelpers.GetAllDirectories())
                using (var powershell = PowerShell.Create())
                {
                    powershell.AddScript($"cd '{directory}'");

                    powershell.AddScript(@"git push");

                    var results = powershell.Invoke();
                }
        }
    }
}
