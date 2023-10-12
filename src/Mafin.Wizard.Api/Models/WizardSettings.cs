namespace Mafin.Wizard.Api.Models;

public record WizardSettings
{
    public WizardSettings(string tafName, string language, string[] features)
    {
        TafName = tafName;
        Language = language;
        Features = FormatFeatures(language, features);
        Parameters = new Dictionary<string, object>();
    }

    public WizardSettings(string tafName, string language, string[] features, IReadOnlyDictionary<string, object> parameters)
        : this(tafName, language, features)
    {
        Parameters = parameters;
    }

    public string TafName { get; private set; }

    public string Language { get; private set; }

    public IReadOnlyList<string> Features { get; private set; }

    public IReadOnlyDictionary<string, object> Parameters { get; private set; }

    private static IReadOnlyList<string> FormatFeatures(string language, string[] features) =>
        features.Select(x => x.StartsWith($"{language}.", StringComparison.OrdinalIgnoreCase) ? x : $"{language}.{x}").ToArray();
}
