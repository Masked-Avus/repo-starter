namespace RepoStarter.ReadMe
{
    internal sealed class Logo
    {
        internal FileInfo File { get; }
        internal bool Exists => File is not null;
        internal string Text { get; }
        internal string Markdown => $"![{Text}]({File.FullName})";

        internal Logo(string path) : this(path, Resources.Defaults.LogoText) { }

        internal Logo(string path, string text)
        {
            File = new FileInfo(path);

            if (!File.Exists)
            {
                throw new FileNotFoundException();
            }

            Text = !string.IsNullOrWhiteSpace(text) ? text : Resources.Defaults.LogoText;
        }
    }
}
