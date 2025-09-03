using System.Management.Automation;
using System.Security;

namespace RepoStarter.ChangeLog
{
    [Cmdlet(VerbsCommon.New, "ChangeLog")]
    public sealed class NewChangeLogCmdlet : Cmdlet
    {
        ChangeLogFile? _changeLog;

        [Parameter]
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
                WriteError(new(
                    exception,
                    typeof(ArgumentNullException).Name,
                    ErrorCategory.InvalidData,
                    null
                    ));
            }
            catch (InvalidOperationException exception)
            {
                WriteError(new(
                    exception,
                    typeof(InvalidOperationException).Name,
                    ErrorCategory.InvalidOperation,
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

            if (Versions is null)
            {
                return;
            }

            for (int i = 0; i < Versions.Length; i++)
            {
                string current = Versions[i];

                if (string.IsNullOrWhiteSpace(current))
                {
                    continue;
                }

                writer.WriteLine(current);
                writer.WriteLine(ChangeLogFile.VersionBody);

                if (i < (Versions.Length - 1))
                {
                    writer.WriteLine();
                }
            }
        }
    }
}
