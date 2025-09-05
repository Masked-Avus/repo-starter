using System.Management.Automation;

namespace RepoStarter
{
    internal static class Extensions
    {
        internal static void CreateDirectory(this PowerShell instance, string name)
            => instance.AddScript($"New-Item -ItemType \"Directory\" -Path \".\\{name}\"");
    }
}
