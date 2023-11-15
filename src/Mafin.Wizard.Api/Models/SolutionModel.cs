namespace Mafin.Wizard.Api.Models;

public record SolutionModel
{
    public SolutionModel(string name, IReadOnlyList<FileInfoRecord> files)
    {
        Name = name;
        Files = files;
    }

    public string Name { get; private set; }

    public IReadOnlyList<FileInfoRecord> Files { get; private set; }
}
