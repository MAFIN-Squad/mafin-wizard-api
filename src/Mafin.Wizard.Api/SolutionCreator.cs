using Mafin.Wizard.Api.Models;
using SharpCompress;

namespace Mafin.Wizard.Api;

public class SolutionCreator
{
    private readonly SolutionModel _model;

    public SolutionCreator(SolutionModel model)
    {
        _model = model;
    }

    public void CreateSolution()
    {
        CreateFileSystem();
        CreateFiles();
    }

    private void CreateFileSystem()
    {
        Directory.CreateDirectory(_model.Settings.TafName);
        foreach (var file in _model.Files)
        {
            Directory.CreateDirectory(Path.Combine(_model.Settings.TafName, file.FilePath));
        }
    }

    private void CreateFiles() => _model.Files.ForEach(CreateFile);

    private void CreateFile(FileInfoRecord file)
    {
        var path = FormatPath(file);
        using StreamWriter sw = File.CreateText(path);
        sw.Write(file.Data);
    }

    private string FormatPath(FileInfoRecord file) =>
        Path.Combine(_model.Settings.TafName, file.FilePath, $"{file.FileName}.{file.Extension}");
}
