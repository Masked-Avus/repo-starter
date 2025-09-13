using RepoStarter.ChangeLog;
using RepoStarter.License;
using RepoStarter.ReadMe;
using RepoStarter.Utilities;
using System.Management.Automation;

namespace RepoStarter.GitRepository
{
    [Cmdlet(VerbsCommon.New, AttributeConstants.GitRepository)]
    public sealed class NewGitRepositoryCmdlet : Cmdlet
    {
        [Parameter(Mandatory = true)]
        [Alias(AttributeConstants.Project, AttributeConstants.Abbreviations.Project)]
        public string ProjectName { get; set; } = string.Empty;

        [Parameter(Mandatory = true)]
        [Alias(AttributeConstants.Abbreviations.Organization)]
        public string Organization { get; set; } = string.Empty;

        [Parameter(Mandatory = true)]
        [Alias(AttributeConstants.License, AttributeConstants.Abbreviations.License)]
        public string LicenseType { get; set; } = string.Empty;

        [Parameter]
        [Alias(AttributeConstants.Abbreviations.Directory)]
        public string Directory { get; set; } = System.IO.Directory.GetCurrentDirectory();

        [Parameter]
        [Alias(
            AttributeConstants.InitialBranch,
            AttributeConstants.Branch,
            AttributeConstants.Abbreviations.Branch
            )]
        public string DefaultBranch { get; set; } = Resources.Defaults.InitialBranch;

        [Parameter]
        public int Year { get; set; } = DateTime.Now.Year;

        protected override void ProcessRecord()
        {
            if (!FullNamesProvided())
            {
                this.ReportException(new ArgumentNullException(), ErrorCategory.InvalidArgument);
                return;
            }
            else if (!RepositoryPath.IsValid(Directory))
            {
                this.ReportException(new FormatException(), ErrorCategory.SyntaxError);
                return;
            }

            RepositoryPath.EnsureDirectoryExists(Directory);

            /*
             * For some reason, calling the cmdlets for creating the README, CHANGELOG, and LICENSE
             * via a PowerShell instance would not result in all of the being created (just one or two,
             * usually the CHANGELOG and maybe the README). No clue what the issue was.
             * 
             * In contrast, creating cmdlet objects and calling them manually does the trick.
             */
            CreateReadMe();
            CreateChangeLog();
            CreateLicense();
            CreateGitRepository();
        }

        private bool FullNamesProvided()
            => !string.IsNullOrWhiteSpace(ProjectName) &&
               !string.IsNullOrWhiteSpace(Organization) &&
               !string.IsNullOrWhiteSpace(LicenseType);

        private void CreateLicense()
        {
            NewLicenseCmdlet newLicense = new()
            {
                LicenseType = LicenseType,
                ProjectName = ProjectName,
                Organization = Organization,
                Year = Year,
                Directory = Directory
            };

            newLicense.Initiate();
        }

        private void CreateReadMe()
        {
            NewReadMeCmdlet newReadMe = new()
            {
                ProjectName = ProjectName,
                Directory = Directory
            };

            newReadMe.Initiate();
        }

        private void CreateChangeLog()
        {
            NewChangeLogCmdlet newChangeLog = new()
            {
                Directory = Directory
            };

            newChangeLog.Initiate();
        }

        private void CreateGitRepository()
        {
            using PowerShell instance = PowerShell.Create();

            instance.AddScript(
                $"Set-Location {Directory}"
                );
            instance.AddScript(
                $"git init --initial-branch {DefaultBranch}"
                );
            instance.AddScript(
                $"New-Item -ItemType Directory -Name {Resources.ItemNames.SourceFolder} -Path ."
                );
            instance.AddScript(
                $"New-Item -ItemType Directory -Name {Resources.ItemNames.TestsFolder} -Path ."
                );
            instance.AddScript(
                $"New-Item -ItemType File -Name {Resources.ItemNames.GitIgnore} -Path ."
                );

            instance.Invoke();
        }
    }
}
