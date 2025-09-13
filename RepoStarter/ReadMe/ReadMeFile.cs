using RepoStarter.Markdown;
using RepoStarter.Resources;
using RepoStarter.Utilities;

namespace RepoStarter.ReadMe
{
    internal sealed class ReadMeFile
    {
        private readonly string _projectName;
        private readonly FileInfo _fileInfo;
        private readonly List<Heading> _headings = [];

        internal Logo? Logo { get; set; }
        private Heading Title => _headings[0];

        internal ReadMeFile(string projectName, string directory)
        {
            _projectName = !string.IsNullOrWhiteSpace(projectName)
                ? projectName
                : Defaults.ProjectName;
            
            _fileInfo = new(RepositoryPath.Create(directory, ItemNames.ReadMe));
            
            _headings =
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

        public void Write()
        {
            using StreamWriter writer = new(_fileInfo.FullName);

            if (Logo is not null && Logo.Exists)
            {
                writer.WriteLine(Logo.Markdown);
                writer.WriteLine();
            }

            writer.WriteLine(Title.FormattedText);
            writer.WriteLine();

            for (int i = 1; i < _headings.Count; i++)
            {
                writer.WriteLine(_headings[i].FormattedText);
                writer.WriteLine();
                writer.WriteLine(Defaults.ReadMeSectionContents);

                if (i < (_headings.Count - 1))
                {
                    writer.WriteLine();
                }
            }
        }
    }
}
