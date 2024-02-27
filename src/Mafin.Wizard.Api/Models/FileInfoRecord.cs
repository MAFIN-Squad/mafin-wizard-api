namespace Mafin.Wizard.Api.Models;

public record FileInfoRecord(string Name, string Directory, string Data)
{
    public string FullPath { get; } = Path.Combine(Directory, Name);
}
