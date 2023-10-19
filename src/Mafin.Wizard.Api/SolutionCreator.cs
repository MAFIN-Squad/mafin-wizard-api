using Mafin.Wizard.Api.Models;
using SharpCompress;

namespace Mafin.Wizard.Cli;

public class SolutionCreator
{
    private SolutionModel _model;
    private string _baseDirectory;

    public void Create(SolutionModel model, string baseDirectory = "")
    {
        _model = model;
        _baseDirectory = baseDirectory;
        CreateFileSystem();
        CreateFiles();
    }

    private string BaseDirectory => Path.Combine(_model.Name, _baseDirectory);

    private void CreateFileSystem()
    {
        Directory.CreateDirectory(_model.Name);
        foreach (var file in _model.Files)
        {
            Directory.CreateDirectory(Path.Combine(BaseDirectory, file.Directory));
        }
    }

    private void CreateFiles() => _model.Files.ForEach(CreateFile);

    private void CreateFile(FileInfoRecord file)
    {
        var path = Path.Combine(BaseDirectory, file.FullPath);
        using StreamWriter sw = File.CreateText(path);
        sw.Write(file.Data);
    }
}
