using System.Reflection;

namespace Mafin.Wizard.Api;

internal class ResourceTemplateProvider
{
    private readonly string[] _directories;
    private readonly string[] _directoryWithSubdirectories;
    private readonly Assembly _assembly;

    public ResourceTemplateProvider(string[] directories, string[] directoryWithSubdirectories)
    {
        _assembly = typeof(TemplateReader).Assembly;
        _directories = directories;
        _directoryWithSubdirectories = directoryWithSubdirectories;

        if (_directories is null || _directories.Length is 0)
        {
            _directories = [string.Empty];
        }
    }

    private string BaseTemplatePath => _assembly.GetName().Name!;

    public bool IsFullTemplateName(string value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value), "Template name cannot be null");
        }

        return value.StartsWith($"{BaseTemplatePath}.", StringComparison.OrdinalIgnoreCase)
            && Path.HasExtension(value);
    }

    public string GetFullTemplateName(string name)
    {
        if (IsFullTemplateName(name))
        {
            return name;
        }

        var names = GetResourceNames()
            .Where(x => Path.GetFileNameWithoutExtension(x).EndsWith(name, StringComparison.OrdinalIgnoreCase)).ToList();

        if (names.Count is 0)
        {
            throw new FileNotFoundException($"Template {name} is not found");
        }

        if (names.Count is > 1)
        {
            var fileInRoot = names.Find(x =>
                Path.GetFileNameWithoutExtension(x).Equals($"{BaseTemplatePath}.{name}", StringComparison.OrdinalIgnoreCase));
            return fileInRoot is not null
                ? fileInRoot
                : throw new FileLoadException($"Failed to find a unique template with {name} name");
        }

        return names.Single();
    }

    public IEnumerable<string> GetResourceNames() => _directories
        .SelectMany(GetResourceNamesInFolder)
        .Union(_directoryWithSubdirectories.SelectMany(GetResourceNamesInFolderAndSubfolders));

    private IEnumerable<string> GetResourceNamesInFolderAndSubfolders(string? directory = null)
    {
        directory = string.IsNullOrEmpty(directory) ? $"{BaseTemplatePath}." : $"{BaseTemplatePath}.{directory}.";
        return _assembly.GetManifestResourceNames()
            .Where(x => x.StartsWith(directory, StringComparison.OrdinalIgnoreCase));
    }

    private IEnumerable<string> GetResourceNamesInFolder(string? directory = null)
    {
        directory = string.IsNullOrEmpty(directory) ? $"{BaseTemplatePath}." : $"{BaseTemplatePath}.{directory}.";
        return _assembly.GetManifestResourceNames()
            .Where(x => x.Replace(directory, string.Empty, StringComparison.OrdinalIgnoreCase).Count(c => c is '.') < 2)
            .ToList();
    }
}
