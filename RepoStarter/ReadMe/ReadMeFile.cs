using RepoStarter.Markdown;
using RepoStarter.Resources;

namespace RepoStarter.ReadMe
{
    internal sealed class ReadMeFile
    {
        private readonly string _projectName;
        private readonly FileInfo _fileInfo;

        internal string FullPath => _fileInfo.FullName;
        internal DirectoryInfo? Directory => _fileInfo.Directory;
        internal Logo? Logo { get; set; }
        internal List<Heading> Headings { get; }
        internal Heading Title => Headings[0];
        internal bool HasLogo => Logo is not null;

        internal ReadMeFile(string projectName, string directory)
        {
            _projectName = !string.IsNullOrWhiteSpace(projectName)
                ? projectName
                : Defaults.ProjectName;

            directory = !string.IsNullOrWhiteSpace(directory)
                ? directory
                : System.IO.Directory.GetCurrentDirectory();

            _fileInfo = new($"{directory}{Path.DirectorySeparatorChar}{ItemNames.ReadMe}");
            
            Headings =
            [
                new(1, _projectName),
                new(2, ReadMeHeadings.Description),
                new(2, ReadMeHeadings.Features),
                new(2, ReadMeHeadings.Usage),
                new(2, ReadMeHeadings.Installation),
                new(2, ReadMeHeadings.DevelopmentRoadmap),
                new(2, ReadMeHeadings.Contributors)
            ];
        }
    }
}
