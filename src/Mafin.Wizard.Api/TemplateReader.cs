using System.Reflection;
using Mafin.Wizard.Api.Models;

namespace Mafin.Wizard.Api;

internal class TemplateReader
{
    private readonly Assembly _assembly;
    private readonly ResourceTemplateProvider _templateProvider;

    public TemplateReader(ResourceTemplateProvider templateProvider)
    {
        _assembly = typeof(TemplateReader).Assembly;
        _templateProvider = templateProvider;
    }

    public List<ScribanTemplate> ReadAll()
    {
        var result = new List<ScribanTemplate>();

        foreach (var file in _templateProvider.GetResourceNames())
        {
            result.Add(new(file, ReadManifestResource(file)));
        }

        return result;
    }

    public string Read(string templateName)
    {
        try
        {
            var name = _templateProvider.GetFullTemplateName(templateName);
            return ReadManifestResource(name);
        }
        catch (FileNotFoundException)
        {
            return string.Empty;
        }
    }

    public async Task<string> ReadAsync(string templateName)
    {
        try
        {
            var name = _templateProvider.GetFullTemplateName(templateName);
            return await ReadManifestResourceAsync(name);
        }
        catch (FileNotFoundException)
        {
            return string.Empty;
        }
    }

    private string ReadManifestResource(string resourceName)
    {
        using Stream stream = _assembly.GetManifestResourceStream(resourceName)!;
        using StreamReader reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }

    private async Task<string> ReadManifestResourceAsync(string resourceName)
    {
        using Stream stream = _assembly.GetManifestResourceStream(resourceName)!;
        using StreamReader reader = new StreamReader(stream);
        return await reader.ReadToEndAsync();
    }
}
