namespace RepoStarter.Utilities
{
    internal sealed class Project
    {
        internal string? Name { get; }
        internal string? Organization { get; }
        internal int Year { get; }
        internal bool HasName => Name is not null && Name?.Length > 0;
        internal bool HasOrganization => Organization is not null && Organization?.Length > 0;

        internal Project(string name, string organization, int year)
        {
            Name = name;
            Organization = organization;
            Year = year >= 0 ? year : 0;
        }

        internal Project(string organization, int year) :
            this(string.Empty, organization, year) { }

        internal Project(string organization) :
            this(string.Empty, organization, DateTime.Now.Year) { }
    }
}
