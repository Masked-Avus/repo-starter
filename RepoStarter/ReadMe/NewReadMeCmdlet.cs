using RepoStarter.Utilities;
using System.Management.Automation;
using System.Security;

namespace RepoStarter.ReadMe
{
    [Cmdlet(VerbsCommon.New, AttributeConstants.ReadMe)]
    public sealed class NewReadMeCmdlet : Cmdlet
    {
        [Parameter(Mandatory = true)]
        [Alias(AttributeConstants.Project, AttributeConstants.Abbreviations.Project)]
        public string ProjectName { get; set; } = string.Empty;

        [Parameter]
        [Alias(AttributeConstants.Abbreviations.Directory)]
        public string Directory { get; set; } = System.IO.Directory.GetCurrentDirectory();

        [Parameter]
        [Alias(AttributeConstants.Abbreviations.LogoPath)]
        public string LogoPath { get; set; } = string.Empty;

        [Parameter]
        public string LogoText { get; set; } = Resources.Defaults.LogoText;

        public void Initiate()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ProjectName))
                {
                    throw new ArgumentException();
                }
                else if (!string.IsNullOrWhiteSpace(LogoPath) && !File.Exists(LogoPath))
                {
                    throw new ArgumentException();
                }

                RepositoryPath.EnsureDirectoryExists(Directory);

                ReadMeFile readMeFile = new(ProjectName, Directory);

                if (!string.IsNullOrWhiteSpace(LogoPath))
                {
                    readMeFile.Logo = (LogoText is not null)
                        ? new(LogoPath, LogoText)
                        : new(LogoPath);
                }

                readMeFile.Write();
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
