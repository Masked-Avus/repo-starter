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
                WriteError(new(
                    exception,
                    typeof(ArgumentNullException).Name,
                    ErrorCategory.InvalidData,
                    null
                    ));
            }
            catch (FileNotFoundException exception)
            {
                WriteError(new(
                    exception,
                    typeof(FileNotFoundException).Name,
                    ErrorCategory.ReadError,
                    null
                    ));
            }
            catch (SecurityException exception)
            {
                WriteError(new(
                    exception,
                    typeof(SecurityException).Name,
                    ErrorCategory.SecurityError,
                    null
                    ));
            }
            catch (PathTooLongException exception)
            {
                WriteError(new(
                    exception,
                    typeof(PathTooLongException).Name,
                    ErrorCategory.LimitsExceeded,
                    null
                    ));
            }
            catch (Exception exception)
            {
                WriteError(new(
                    exception,
                    typeof(Exception).Name,
                    ErrorCategory.SyntaxError,
                    null
                    ));
            }
        }

        private void SetReadMeInfo()
        {
            if (ProjectName is null)
            {
                throw new ArgumentNullException();
            }

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

            for (int i = 1; i < _readMeFile.Headings.Length; i++)
            {
                writer.WriteLine(_readMeFile.Headings[i].FormattedText);
                writer.WriteLine();
                writer.WriteLine(ReadMeFile.SectionDefaultContents);

                if (i < _readMeFile.Headings.Length - 1)
                {
                    writer.WriteLine();
                }
            }
        }

        private void SetLogoPath()
        {
            if (_readMeFile is null)
            {
                return;
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
