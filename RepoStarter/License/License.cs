using RepoStarter.Resources.LicenseTemplates;

namespace RepoStarter.License
{
    internal sealed class License
    {
        internal string Name { get; }
        internal string Template { get; }

        internal License(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                throw new ArgumentNullException();
            }

            (SupportedLicense license, string name) = MatchName(type);
            Name = name;
            Template = MatchTemplate(license);
        }

        internal License(SupportedLicense type)
        {
            Name = Enum.GetName(typeof(SupportedLicense), type);
            Template = MatchTemplate(type);
        }

        private static string MatchTemplate(SupportedLicense type)
        {
            return type switch
            {
                SupportedLicense.Apache => LicenseTemplates.Apache,
                SupportedLicense.Bds3 => LicenseTemplates.Bsd3,
                SupportedLicense.Gpl3 => LicenseTemplates.Gpl3,
                SupportedLicense.Mit => LicenseTemplates.Mit,
                SupportedLicense.Mpl => LicenseTemplates.Mpl,
                SupportedLicense.PublicDomain => LicenseTemplates.PublicDomain,
                SupportedLicense.Zlib => LicenseTemplates.Zlib,
                _ => throw new ArgumentException($"Unknown license type (value: {(int)type})")
            };
        }

        internal static (SupportedLicense SupportedType, string Name) MatchName(string value)
        {
            foreach (string type in Enum.GetNames(typeof(SupportedLicense)))
            {
                if (value.Equals(type, StringComparison.OrdinalIgnoreCase))
                {
                    return ((SupportedLicense)Enum.Parse(typeof(SupportedLicense), type), type);
                }
            }

            throw new ArgumentException($"Value \"{value}\" is an invalid or unsupported license type.");
        }
    }
}
