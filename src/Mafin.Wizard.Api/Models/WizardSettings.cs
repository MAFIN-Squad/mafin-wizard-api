namespace Mafin.Wizard.Api.Models;

public record WizardSettings
{
    public WizardSettings(string appName, string language, string[] features)
    {
        AppName = appName;
        Language = language;
        Features = FormatFeatures(language, features);
        Parameters = new Dictionary<string, object>();
    }

    public WizardSettings(string appName, string language, string[] features, IReadOnlyDictionary<string, object> parameters)
        : this(appName, language, features)
    {
        Parameters = parameters;
    }

    public string AppName { get; private set; }

    public string Language { get; private set; }

    public IReadOnlyList<string> Features { get; private set; }

    public IReadOnlyDictionary<string, object> Parameters { get; private set; }

    private static IReadOnlyList<string> FormatFeatures(string language, string[] features) =>
        features.Select(x => x.StartsWith($"{language}.", StringComparison.OrdinalIgnoreCase) ? x : $"{language}.{x}").ToArray();
}
