namespace RepoStarter.Utilities
{
    internal static class RepositoryPath
    {
        internal static string Create(string directory, string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName) || string.IsNullOrWhiteSpace(directory))
            {
                throw new FormatException($"\"{fileName}\" is an invalid file name.");
            }

            return $"{directory}{Path.DirectorySeparatorChar}{fileName}";
        }

        internal static bool IsValid(string path)
        {
            foreach (char badChar in Path.GetInvalidPathChars())
            {
                if (path.Contains(badChar))
                {
                    return false;
                }
            }

            return true;
        }

        internal static void EnsureDirectoryExists(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
    }
}
