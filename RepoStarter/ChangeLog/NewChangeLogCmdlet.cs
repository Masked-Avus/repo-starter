using RepoStarter.Utilities;
using System.Management.Automation;
using System.Security;

namespace RepoStarter.ChangeLog
{
    [Cmdlet(VerbsCommon.New, AttributeConstants.ChangeLog)]
    public sealed class NewChangeLogCmdlet : Cmdlet
    {
        [Parameter]
        [Alias(AttributeConstants.Abbreviations.Directory)]
        public string Directory { get; set; } = System.IO.Directory.GetCurrentDirectory();

        [Parameter]
        public string Title { get; set; } = Resources.Defaults.ChangeLogTitle;

        [Parameter]
        public string[] Versions { get; set; } = [];

        public void Initiate()
        {
            try
            {
                RepositoryPath.EnsureDirectoryExists(Directory);

                ChangeLogFile _changeLog = new(Directory, Title, Versions);
                _changeLog.Write();
            }
            catch (ArgumentNullException exception)
            {
                this.ReportException(exception, ErrorCategory.InvalidData);
            }
            catch (InvalidOperationException exception)
            {
                this.ReportException(exception, ErrorCategory.InvalidOperation);
            }
            catch (FileNotFoundException exception)
            {
                this.ReportException(exception, ErrorCategory.ReadError);
            }
            catch (SecurityException exception)
            {
                this.ReportException(exception, ErrorCategory.SecurityError);
            }
            catch (PathTooLongException exception)
            {
                this.ReportException(exception, ErrorCategory.LimitsExceeded);
            }
            catch (Exception exception)
            {
                this.ReportException(exception, ErrorCategory.SyntaxError);
            }
        }

        protected override void ProcessRecord() => Initiate();
    }
}
