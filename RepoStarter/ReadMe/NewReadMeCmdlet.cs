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
        [Alias("AbsLogoPath", "ALP")]
        public string? AbsoluteLogoPath { get; set; }

        [Parameter]
        [Alias("RelLogoPath", "LogoPath", "Logo", "RLP")]
        public string? RelativeLogoPath { get; set; }

        [Parameter]
        public string? LogoText { get; set; }

        protected override void ProcessRecord()
        {
            try
            {
                SetReadMeInfo();
                SetLogoPath();
                WriteReadMe();
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

        private void SetReadMeInfo()
        {
            if (ProjectName is null)
            {
                throw new ArgumentNullException();
            }

            Directory ??= System.IO.Directory.GetCurrentDirectory();

            _readMeFile = new(ProjectName, Directory);
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

        private void SetLogoPath()
        {
            if (_readMeFile is null)
            {
                throw new InvalidOperationException();
            }

            bool hasAbsoluteLogoPath = AbsoluteLogoPath is not null;
            bool hasRelativeLogoPath = RelativeLogoPath is not null;

            if (hasAbsoluteLogoPath && hasRelativeLogoPath)
            {
                WriteWarning(
                    "Pathing conflict detected. " +
                    "Both relative and absolute paths for logo provided. " +
                    "Logo image pathing will default to relative path."
                    );
            }

            if (hasRelativeLogoPath)
            {
                _readMeFile.Logo = new(Logo.CreateRelativePath(_readMeFile, RelativeLogoPath), LogoText);
            }
            else if (hasAbsoluteLogoPath)
            {
                _readMeFile.Logo = new(AbsoluteLogoPath, LogoText);
            }
        }
    }
}
