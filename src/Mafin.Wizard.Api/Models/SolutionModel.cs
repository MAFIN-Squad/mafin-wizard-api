namespace Mafin.Wizard.Api.Models;

public record SolutionModel(string Name, IReadOnlyList<FileInfoRecord> Files);
