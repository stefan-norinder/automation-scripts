using System.Management.Automation;

namespace Console
{
    public class GitController
    {
        public string CheckStatus(string directory)
        {             
            using (PowerShell powershell = PowerShell.Create())
            {
                powershell.AddScript($"cd '{directory}'");

                powershell.AddScript(@"git add .");
                
                var results = powershell.Invoke();
            }

            return string.Empty;
        }
    }
}
