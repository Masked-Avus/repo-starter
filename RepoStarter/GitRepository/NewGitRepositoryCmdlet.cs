using System.Management.Automation;

namespace RepoStarter.GitRepository
{
    [Cmdlet(VerbsCommon.New, "GitRepository")]
    public sealed class NewGitRepositoryCmdlet : Cmdlet
    {
        [Parameter(Mandatory = true)]
        [Alias("Project", "Proj")]
        public string? ProjectName { get; set; }

        [Parameter(Mandatory = true)]
        [Alias("Org")]
        public string? Organization { get; set; }

        [Parameter(Mandatory = true)]
        [Alias("License", "Lic")]
        public string? LicenseType { get; set; }

        [Parameter]
        [Alias("Dir")]
        public string? Directory { get; set; }

        [Parameter]
        [Alias("InitialBranch", "Branch", "Br")]
        public string? DefaultBranch { get; set; }

        [Parameter]
        public int Year { get; set; } = DateTime.Now.Year;

        protected override void ProcessRecord()
        {
            if (ProjectName is null)
            {
                this.ReportException(new ArgumentNullException(), ErrorCategory.InvalidArgument);
                return;
            }

            Directory ??= ".\\";
            DefaultBranch ??= "master";

            using PowerShell instance = PowerShell.Create();

            instance.AddScript($"Set-Location {Directory}");
            instance.AddScript($"git init --initial-branch {DefaultBranch}");
            instance.CreateDirectory("src");
            instance.CreateDirectory("tests");
            instance.CreateFile(".gitignore");
            instance.AddScript($"New-ReadMe -ProjectName {ProjectName} -Directory {Directory}");
            instance.AddScript($"New-ChangeLog -Directory {Directory}");
            instance.AddScript(
                $"New-License -LicenseType {LicenseType} -Organization {Organization} -ProjectName {ProjectName} -Year {Year} -Directory {Directory}"
                );

            instance.Invoke();
        }
    }
}
