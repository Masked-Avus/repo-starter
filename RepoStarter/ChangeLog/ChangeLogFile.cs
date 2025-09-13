using RepoStarter.Markdown;

namespace RepoStarter.ChangeLog
{
    internal sealed class ChangeLogFile
    {
        private const int HeadingLevel = 2;

        private readonly FileInfo _fileInfo;
        private readonly Heading _title = new(1, Resources.Defaults.ChangeLogTitle);

        internal Heading Title => _title;
        internal string FullPath => _fileInfo.FullName;
        internal List<Heading> VersionHeadings { get; } = [];

        internal ChangeLogFile(string directory, string title, string[] versions)
        {
            _fileInfo = new(CreatePath(directory, Resources.ItemNames.ChangeLog));
            _title = new(1, (!string.IsNullOrWhiteSpace(title))
                ? title
                : Resources.Defaults.ChangeLogTitle);
            VersionHeadings = CreateHeadings(versions);
        }

        private static string CreatePath(string? directory, string fileName)
            => $"{directory}{Path.DirectorySeparatorChar}{fileName}";

        private static List<Heading> CreateHeadings(string[] versions)
        {
            if (versions is null)
            {
                return [];
            }

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
