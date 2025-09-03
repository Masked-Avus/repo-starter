using System.Management.Automation;
using System.Security;

namespace RepoStarter.ChangeLog
{
    [Cmdlet(VerbsCommon.New, "ChangeLog")]
    public sealed class NewChangeLogCmdlet : Cmdlet
    {
        ChangeLogFile? _changeLog;

        [Parameter]
        [Alias("Dir")]
        public string? Directory { get; set; }

        [Parameter]
        public string[]? Versions { get; set; }

        protected override void ProcessRecord()
        {
            try
            {
                SetChangeLogInfo();
                WriteChangeLog();
            }
            catch (ArgumentNullException exception)
            {
                ErrorHandler.ReportException(this, exception, ErrorCategory.InvalidData);
            }
            catch (InvalidOperationException exception)
            {
                ErrorHandler.ReportException(this, exception, ErrorCategory.InvalidOperation);
            }
            catch (FileNotFoundException exception)
            {
                ErrorHandler.ReportException(this, exception, ErrorCategory.ReadError);
            }
            catch (SecurityException exception)
            {
                ErrorHandler.ReportException(this, exception, ErrorCategory.SecurityError);
            }
            catch (PathTooLongException exception)
            {
                ErrorHandler.ReportException(this, exception, ErrorCategory.LimitsExceeded);
            }
            catch (Exception exception)
            {
                ErrorHandler.ReportException(this, exception, ErrorCategory.SyntaxError);
            }
        }

        private void SetChangeLogInfo()
        {
            if (Directory is null)
            {
                Directory = System.IO.Directory.GetCurrentDirectory();
            }

            _changeLog = (Versions is not null) ? new(Directory, Versions) : new(Directory);
        }

        private void WriteChangeLog()
        {
            if (_changeLog is null)
            {
                throw new InvalidOperationException();
            }

            using StreamWriter writer = new(_changeLog.FullPath);

            for (int i = 0; i < _changeLog.VersionHeadings.Count; i++)
            {
                writer.WriteLine(_changeLog.VersionHeadings[i].FormattedText);
                writer.WriteLine(ChangeLogFile.VersionBody);

                if (i < (_changeLog.VersionHeadings.Count - 1))
                {
                    writer.WriteLine();
                }
            }
        }
    }
}
