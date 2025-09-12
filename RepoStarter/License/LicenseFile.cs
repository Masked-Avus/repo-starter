using System.Text;

namespace RepoStarter.License
{
    internal sealed class LicenseFile
    {
        internal const string DefaultProjectName = "Project";

        private readonly FileInfo _fileInfo;
        private readonly License _license;
        private readonly Project _project;
        private readonly string _contents;

        internal LicenseFile(License license, Project project, string directory)
        {
            _project = project;
            _license = license;
            _contents = GetContents();

            directory = !string.IsNullOrWhiteSpace(directory)
                ? directory
                : Directory.GetCurrentDirectory();

            _fileInfo = new($"{directory}{Path.DirectorySeparatorChar}{Resources.ItemNames.License}");
        }

        public void Write() => File.WriteAllText(_fileInfo.FullName, _contents);

        private string GetContents()
        {
            StringBuilder builder = new(_license.Template);

            if (_license.Template.Contains(Resources.Placeholders.Year))
            {
                builder.Replace(Resources.Placeholders.Year, _project.Year.ToString());
            }
            
            if (_license.Template.Contains(Resources.Placeholders.Organization))
            {
                if (!_project.HasOrganization)
                {
                    throw new InvalidOperationException("Organization name missing.");
                }

                builder.Replace(Resources.Placeholders.Organization, _project.Organization);
            }

            if (_license.Template.Contains(Resources.Placeholders.Project))
            {
                if (!_project.HasName)
                {
                    throw new InvalidOperationException("Project name missing.");
                }

                builder.Replace(Resources.Placeholders.Project, _project.Name);
            }

            return builder.ToString();
        }
    }
}
