using RepoStarter.Markdown;
using RepoStarter.Utilities;

namespace RepoStarter.ChangeLog
{
    internal sealed class ChangeLogFile
    {
        private const int HeadingLevel = 2;

        private readonly FileInfo _fileInfo;
        private readonly Heading _title = new(1, Resources.Defaults.ChangeLogTitle);
        private readonly List<Heading> _versionHeadings = [];

        internal ChangeLogFile(string directory, string title, string[] versions)
        {
            _fileInfo = new(RepositoryPath.Create(directory, Resources.ItemNames.ChangeLog));
            _title = new(1, (!string.IsNullOrWhiteSpace(title))
                ? title
                : Resources.Defaults.ChangeLogTitle);
            _versionHeadings = CreateHeadings(versions);
        }

        internal void Write()
        {
            using StreamWriter writer = new(_fileInfo.FullName);

            writer.WriteLine(_title.FormattedText);
            writer.WriteLine();

            for (int i = 0; i < _versionHeadings.Count; i++)
            {
                writer.WriteLine(_versionHeadings[i].FormattedText);
                writer.WriteLine(Resources.Defaults.ChangeLogBody);

                if (i < (_versionHeadings.Count - 1))
                {
                    writer.WriteLine();
                }
            }
        }

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
