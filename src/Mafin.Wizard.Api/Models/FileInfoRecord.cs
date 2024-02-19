namespace Mafin.Wizard.Api.Models;

public record FileInfoRecord(string Name, string Directory, string Data)
{
    public string FullPath => Path.Combine(Directory, Name);
}
