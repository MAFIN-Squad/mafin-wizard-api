namespace Mafin.Wizard.Api.Models;

public record FileInfoRecord
{
    public FileInfoRecord(
        string fileName,
        string extension,
        string filePath,
        string data)
    {
        FileName = fileName;
        Extension = extension;
        FilePath = filePath;
        Data = data;
    }

    public string Extension { get; private set; }

    public string FileName { get; private set; }

    public string FilePath { get; private set; }

    public string Data { get; private set; }
}
