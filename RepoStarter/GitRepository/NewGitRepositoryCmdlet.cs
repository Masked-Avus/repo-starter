using System.Management.Automation;

namespace RepoStarter.GitRepository
{
    [Cmdlet(VerbsCommon.New, AttributeConstants.GitRepository)]
    public sealed class NewGitRepositoryCmdlet : Cmdlet
    {
        [Parameter(Mandatory = true)]
        [Alias(AttributeConstants.Project, AttributeConstants.Abbreviations.Project)]
        public string? ProjectName { get; set; }

        [Parameter(Mandatory = true)]
        [Alias(AttributeConstants.Abbreviations.Organization)]
        public string? Organization { get; set; }

        [Parameter(Mandatory = true)]
        [Alias(AttributeConstants.License, AttributeConstants.Abbreviations.License)]
        public string? LicenseType { get; set; }

        [Parameter]
        [Alias(AttributeConstants.Abbreviations.Directory)]
        public string? Directory { get; set; }

        [Parameter]
        [Alias(
            AttributeConstants.InitialBranch,
            AttributeConstants.Branch,
            AttributeConstants.Abbreviations.Branch
            )]
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
