using System.Management.Automation;
using System.Security;

namespace RepoStarter.ChangeLog
{
    [Cmdlet(VerbsCommon.New, AttributeConstants.ChangeLog)]
    public sealed class NewChangeLogCmdlet : Cmdlet
    {
        ChangeLogFile? _changeLog;

        [Parameter]
        [Alias(AttributeConstants.Abbreviations.Directory)]
        public string? Directory { get; set; }

        [Parameter]
        public string? Title { get; set; }

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

        private void SetChangeLogInfo()
        {
            Directory ??= System.IO.Directory.GetCurrentDirectory();
            Title ??= ChangeLogFile.DefaultTitle;
            Versions ??= [];

            _changeLog = new(Directory, Title, Versions);
        }

        private void WriteChangeLog()
        {
            if (_changeLog is null)
            {
                throw new InvalidOperationException();
            }

            using StreamWriter writer = new(_changeLog.FullPath);

            writer.WriteLine(_changeLog.Title.FormattedText);
            writer.WriteLine();

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
