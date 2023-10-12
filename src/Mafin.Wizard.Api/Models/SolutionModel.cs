namespace Mafin.Wizard.Api.Models;

public record SolutionModel
{
    public SolutionModel(WizardSettings settings, IReadOnlyList<FileInfoRecord> files)
    {
        Settings = settings;
        Files = files;
    }

    public WizardSettings Settings { get; private set; }

    public IReadOnlyList<FileInfoRecord> Files { get; private set; }
}
