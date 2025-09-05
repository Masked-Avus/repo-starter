using System.Management.Automation;
using System.Security;

namespace RepoStarter.ReadMe
{
    [Cmdlet(VerbsCommon.New, "ReadMe")]
    public sealed class NewReadMeCmdlet : Cmdlet
    {
        private ReadMeFile? _readMeFile;

        [Parameter(Mandatory = true)]
        [Alias("Project", "Proj")]
        public string? ProjectName { get; set; }

        [Parameter]
        [Alias("Dir")]
        public string? Directory { get; set; }

        [Parameter]
        [Alias("Logo")]
        public string? LogoPath { get; set; }

        [Parameter]
        public string? LogoText { get; set; }

        protected override void ProcessRecord()
        {
            try
            {
                SetReadMeInfo();
                WriteReadMe();
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

        private void SetReadMeInfo()
        {
            if (ProjectName is null)
            {
                throw new ArgumentNullException();
            }

            Directory ??= System.IO.Directory.GetCurrentDirectory();

            _readMeFile = new(ProjectName, Directory);

            if (LogoPath is not null)
            {
                _readMeFile.Logo = (LogoText is not null)
                    ? new(LogoPath, LogoText)
                    : new(LogoPath);
            }
        }

        private void WriteReadMe()
        {
            if (_readMeFile is null)
            {
                return;
            }

            using StreamWriter writer = new(_readMeFile.FullPath);

            if (_readMeFile.Logo is not null && _readMeFile.Logo.Exists)
            {
                writer.WriteLine(_readMeFile.Logo.Markdown);
                writer.WriteLine();
            }

            writer.WriteLine(_readMeFile.Title.FormattedText);
            writer.WriteLine();

            for (int i = 1; i < _readMeFile.Headings.Count; i++)
            {
                writer.WriteLine(_readMeFile.Headings[i].FormattedText);
                writer.WriteLine();
                writer.WriteLine(ReadMeFile.SectionDefaultContents);

                if (i < (_readMeFile.Headings.Count - 1))
                {
                    writer.WriteLine();
                }
            }
        }
    }
}
