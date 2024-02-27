namespace Mafin.Wizard.Api.Models;

public record WizardSettings(string AppName, string Language, IReadOnlyList<string> Features)
{
    public IReadOnlyDictionary<string, object> Parameters { get; } = new Dictionary<string, object>();
}
