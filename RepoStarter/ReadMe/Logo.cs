namespace RepoStarter.ReadMe
{
    internal sealed class Logo
    {
        private const string DefaultText = "logo";

        internal FileInfo File { get; }
        internal bool Exists => File is not null;
        internal string Text { get; }
        internal string Markdown => $"![{Text}]({File.FullName})";

        internal Logo(string path) : this(path, DefaultText) { }

        internal Logo(string path, string text)
        {
            File = new FileInfo(path);

            if (!File.Exists)
            {
                throw new FileNotFoundException();
            }

            Text = !string.IsNullOrWhiteSpace(text) ? text : DefaultText;
        }

        internal static string CreateRelativePath(ReadMeFile readme, string relativePath)
        {
            if (readme.Directory is null)
            {
                throw new ArgumentNullException(nameof(readme.Directory));
            }

            return $"{readme.Directory.FullName}{Path.DirectorySeparatorChar}{relativePath}";
        }
    }
}
