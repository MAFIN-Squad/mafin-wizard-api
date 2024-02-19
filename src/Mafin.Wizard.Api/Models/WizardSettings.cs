namespace Mafin.Wizard.Api.Models;

public record WizardSettings(string AppName, string Language, IReadOnlyList<string> Features)
{
    public IReadOnlyDictionary<string, object> Parameters { get; private set; } = new Dictionary<string, object>();
}
