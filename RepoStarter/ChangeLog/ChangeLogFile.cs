using RepoStarter.Markdown;
using System.Management.Automation.Language;

namespace RepoStarter.ChangeLog
{
    internal sealed class ChangeLogFile
    {
        internal const string DefaultTitle = "Version History";
        internal const string VersionBody = "- ";
        private const int HeadingLevel = 2;

        private readonly FileInfo _fileInfo;
        private readonly Heading _title = new(1, DefaultTitle);

        internal string Name => "CHANGELOG.md";
        internal Heading Title => _title;
        internal string FullPath => _fileInfo.FullName;
        internal List<Heading> VersionHeadings { get; } = [];

        internal ChangeLogFile(string directory, string title, string[] versions)
        {
            _fileInfo = new(CreatePath(directory, Name));
            _title = new(1, (!string.IsNullOrWhiteSpace(title)) ? title : DefaultTitle);
            VersionHeadings = CreateHeadings(versions);
        }

        private static string CreatePath(string? directory, string fileName)
            => $"{directory}{Path.DirectorySeparatorChar}{fileName}";

        private static List<Heading> CreateHeadings(string[] versions)
        {
            List<Heading> headings = [];

            foreach (string version in versions)
            {
                if (string.IsNullOrWhiteSpace(version))
                {
                    continue;
                }

                headings.Add(new(HeadingLevel, version));
            }

            return headings;
        }
    }
}
