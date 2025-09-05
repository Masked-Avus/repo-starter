using System.Text;

namespace RepoStarter.Markdown
{
    internal sealed class Heading
    {
        private const char Marker = '#';
        private const string DefaultText = "Heading";
        private const int MinLevel = 1;
        private const int MaxLevel = 6;

        private int _level;

        internal int Level
        {
            get => _level;

            set
            {
                if (value > MaxLevel)
                {
                    _level = MaxLevel;
                }
                else if (value < MinLevel)
                {
                    _level = MinLevel;
                }
                else
                {
                    _level = value;
                }
            }
        }

        internal string Text { get; }

        internal string FormattedText { get; }

        internal Heading(int level, string text)
        {
            Level = level;
            Text = !string.IsNullOrEmpty(text) ? text : DefaultText;
            FormattedText = CreateFormattedText(Level, Text);
        }

        private static string CreateFormattedText(int level, string text)
        {
            StringBuilder formattedText = new(100);

            for (int i = 0; i < level; i++)
            {
                formattedText.Append(Marker);
            }

            formattedText.Append(' ');
            formattedText.Append(text);

            return formattedText.ToString();
        }

        public override string ToString() => FormattedText;
    }
}
