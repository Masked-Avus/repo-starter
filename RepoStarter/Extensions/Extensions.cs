using System.Management.Automation;

namespace RepoStarter
{
    internal static class Extensions
    {
        internal static void CreateDirectory(this PowerShell instance, string name)
            => instance.AddScript($"New-Item -ItemType \"Directory\" -Path \".\\{name}\"");

        internal static void ReportException<T>(this Cmdlet cmdlet, T exception, ErrorCategory errorCategory)
            where T : Exception
            => cmdlet.WriteError(new(exception, typeof(T).Name, errorCategory, null));
    }
}
