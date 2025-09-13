using System.Management.Automation;
using System.Security;

namespace RepoStarter.License
{
    [Cmdlet(VerbsCommon.New, AttributeConstants.License)]
    public sealed class NewLicenseCmdlet : Cmdlet
    {
        private LicenseFile? _licenseFile;

        [Parameter(Mandatory = true)]
        [Alias(AttributeConstants.License, AttributeConstants.Abbreviations.License)]
        public string? LicenseType { get; set; }

        [Parameter]
        [Alias(AttributeConstants.Project, AttributeConstants.Abbreviations.Project)]
        public string? ProjectName { get; set; }

        [Parameter]
        public int Year { get; set; } = DateTime.Now.Year;

        [Parameter]
        [Alias(AttributeConstants.Abbreviations.Organization)]
        public string? Organization { get; set; }

        [Parameter]
        [Alias(AttributeConstants.Abbreviations.Directory)]
        public string? Directory { get; set; }

        protected override void ProcessRecord()
        {
            try
            {
                SetLicenseInfo();
                _licenseFile?.Write();
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

        private void SetLicenseInfo()
        {
            ProjectName ??= string.Empty;
            Organization ??= string.Empty;
            Project project = new(ProjectName, Organization, Year);

            LicenseType ??= string.Empty;
            License license = new(LicenseType);

            Directory ??= System.IO.Directory.GetCurrentDirectory();

            _licenseFile = new(license, project, Directory);
        }
    }
}
