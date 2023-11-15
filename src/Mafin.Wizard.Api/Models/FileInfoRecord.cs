namespace Mafin.Wizard.Api.Models;

public record FileInfoRecord
{
    public FileInfoRecord(string name, string directory, string data)
    {
        Name = name;
        Directory = directory;
        Data = data;
    }

    public string Directory { get; private set; }

    public string Name { get; private set; }

    public string Data { get; private set; }

    public string FullPath => Path.Combine(Directory, Name);
}