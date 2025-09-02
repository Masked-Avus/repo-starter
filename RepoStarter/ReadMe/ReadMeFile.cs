using RepoStarter.Markdown;

namespace RepoStarter.ReadMe
{
    internal sealed class ReadMeFile
    {
        internal const string SectionDefaultContents = "TODO";
        internal const string DefaultProjectName = "Project";

        private readonly string _projectName;
        private readonly FileInfo _fileInfo;

        internal string Name => "README.md";
        internal string FullPath => _fileInfo.FullName;
        internal DirectoryInfo? Directory => _fileInfo.Directory;
        internal Logo? Logo { get; set; }
        internal Heading[] Headings { get; }
        internal Heading Title => Headings[0];

        internal bool HasLogo => Logo is not null;

        internal ReadMeFile(string projectName, string? directory)
        {
            _projectName = !string.IsNullOrWhiteSpace(projectName)
                ? projectName
                : DefaultProjectName;

            directory = !string.IsNullOrWhiteSpace(directory)
                ? directory
                : System.IO.Directory.GetCurrentDirectory();

            _fileInfo = new($"{directory}{Path.DirectorySeparatorChar}{Name}");
            
            Headings =
            [
                new(1, _projectName),
                new(2, "Description"),
                new(2, "Features"),
                new(2, "Usage"),
                new(2, "Installation"),
                new(2, "Plans"),
                new(2, "License"),
                new(2, "Contributors")
            ];
        }
    }
}
