namespace Mafin.Wizard.Api.Meta;

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
