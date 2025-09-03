using System.Management.Automation;

namespace RepoStarter
{
    internal static class ErrorHandler
    {
        internal static void ReportException<T>(Cmdlet cmdlet, T exception, ErrorCategory errorCategory)
            where T : Exception
            => cmdlet.WriteError(new(exception, typeof(T).Name, errorCategory, null));
    }
}
