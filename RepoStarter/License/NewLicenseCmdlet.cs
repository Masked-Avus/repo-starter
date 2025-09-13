using RepoStarter.Utilities;
using System.Management.Automation;
using System.Security;

namespace RepoStarter.License
{
    [Cmdlet(VerbsCommon.New, AttributeConstants.License)]
    public sealed class NewLicenseCmdlet : Cmdlet
    {
        [Parameter(Mandatory = true)]
        [Alias(AttributeConstants.License, AttributeConstants.Abbreviations.License)]
        public string LicenseType { get; set; } = string.Empty;

        [Parameter]
        [Alias(AttributeConstants.Project, AttributeConstants.Abbreviations.Project)]
        public string ProjectName { get; set; } = string.Empty;

        [Parameter]
        public int Year { get; set; } = DateTime.Now.Year;

        [Parameter]
        [Alias(AttributeConstants.Abbreviations.Organization)]
        public string Organization { get; set; } = string.Empty;

        [Parameter]
        [Alias(AttributeConstants.Abbreviations.Directory)]
        public string Directory { get; set; } = System.IO.Directory.GetCurrentDirectory();

        public void Initiate()
        {
            try
            {
                RepositoryPath.EnsureDirectoryExists(Directory);

                Project project = new(ProjectName, Organization, Year);
                License license = new(LicenseType);

                LicenseFile licenseFile = new(license, project, Directory);
                licenseFile.Write();
            }
            catch (ArgumentNullException exception)
            {
                this.ReportException(exception, ErrorCategory.InvalidData);
            }
            catch (ArgumentException exception)
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
