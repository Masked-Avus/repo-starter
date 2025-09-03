using RepoStarter.Markdown;

namespace RepoStarter.ChangeLog
{
    internal sealed class ChangeLogFile
    {
        internal const string VersionBody = "- ";
        private const int HeadingLevel = 2;

        private readonly FileInfo _fileInfo;

        internal string Name => "CHANGELOG.md";
        internal string FullPath => _fileInfo.FullName;
        internal List<Heading> Headings { get; } = [];

        internal ChangeLogFile(string directory)
            => _fileInfo = new(CreatePath(directory, Name));

        internal ChangeLogFile(string directory, string[] versions) : this(directory)
            => Headings = CreateHeadings(versions);

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
