using System.Management.Automation;

namespace RepoStarter.GitRepository
{
    [Cmdlet(VerbsCommon.New, "GitRepository")]
    public sealed class NewGitRepositoryCmdlet : Cmdlet
    {
        [Parameter(Mandatory = true)]
        [Alias("Project", "Proj")]
        public string? ProjectName { get; set; }

        [Parameter]
        [Alias("Dir")]
        public string? Directory { get; set; }

        [Parameter]
        [Alias("InitialBranch", "Branch", "Br")]
        public string? DefaultBranch { get; set; }

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
            instance.AddScript($"New-ReadMe -ProjectName {ProjectName} -Directory {Directory}");
            instance.AddScript($"New-ChangeLog -Directory {Directory}");

            instance.Invoke();
        }
    }
}
