namespace RepoStarter.ReadMe
{
    internal sealed class Logo
    {
        private readonly FileInfo _fileInfo;
        private readonly string _text;

        internal bool Exists => _fileInfo.Exists;
        internal string Markdown => $"![{_text}]({_fileInfo.FullName})";

        internal Logo(string path) : this(path, Resources.Defaults.LogoText) { }

        internal Logo(string path, string text)
        {
            _fileInfo = new FileInfo(path);

            if (!_fileInfo.Exists)
            {
                throw new FileNotFoundException();
            }

            _text = !string.IsNullOrWhiteSpace(text) ? text : Resources.Defaults.LogoText;
        }
    }
}
