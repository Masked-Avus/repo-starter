using System.Text;

namespace RepoStarter.Markdown
{
    internal sealed class Heading
    {
        public enum Level
        {
            One = 1,
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5,
            Six = 6
        }

        private readonly Level _level;

        internal string Text { get; }
        internal string FormattedText { get; }

        internal Heading(Level level, string text)
        {
            _level = level;
            Text = !string.IsNullOrEmpty(text) ? text : Resources.Defaults.HeadingText;
            FormattedText = CreateFormattedText(_level, Text);
        }

        private static string CreateFormattedText(Level level, string text)
        {
            StringBuilder formattedText = new(100);
            const char Delimiter = '#';

            int count = (int)level;

            for (int i = 0; i < count; i++)
            {
                formattedText.Append(Delimiter);
            }

            formattedText.Append(' ');
            formattedText.Append(text);

            return formattedText.ToString();
        }

        public override string ToString() => FormattedText;
    }
}
