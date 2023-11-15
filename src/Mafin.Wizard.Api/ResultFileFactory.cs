using System.Text.RegularExpressions;
using Mafin.Wizard.Api.Constants;
using Mafin.Wizard.Api.Extensions;
using Mafin.Wizard.Api.Models;
using Scriban;

namespace Mafin.Wizard.Api;

internal class ResultFileFactory
{
    private const string PathPattern = "#### path: (.*?) ####";
    private const string FileNamePattern = "#### name: (.*?) ####";

    public FileInfoRecord CreateResultFileRecord(ScribanTemplate template, TemplateContext context)
    {
        var renderedTemplate = RenderTemplate(template, context);
        var filename = GetFileName(template.Name, ref renderedTemplate);
        var directory = GetDirectory(ref renderedTemplate);

        foreach (var extension in ScribanFileExtensions.ScriptExtensions)
        {
            if (filename.EndsWith($".{extension}"))
            {
                filename = filename.TrimEnd($".{extension}");
                break;
            }
        }

        return new FileInfoRecord(filename, directory, renderedTemplate);
    }

    private string RenderTemplate(ScribanTemplate template, TemplateContext context) =>
        Template.Parse(template.Content).Render(context);

    private string GetDirectory(ref string fileData)
    {
        var regex = new Regex(PathPattern);
        var parsedPath = regex.Matches(fileData).FirstOrDefault()?.Groups[1]?.Value ?? string.Empty;
        var result = string.Empty;

        if (!string.IsNullOrEmpty(parsedPath))
        {
            result = parsedPath.Replace(':', Path.DirectorySeparatorChar);
            fileData = regex.Replace(fileData, string.Empty);
        }

        return result;
    }

    private string GetFileName(string templateName, ref string fileData)
    {
        var regex = new Regex(FileNamePattern);
        var name = regex.Matches(fileData).FirstOrDefault()?.Groups[1]?.Value ?? string.Empty;

        if (!string.IsNullOrEmpty(name))
        {
            fileData = regex.Replace(fileData, string.Empty);
        }
        else
        {
            name = Path.GetFileNameWithoutExtension(templateName);
            var nameParts = name.Split(".");
            name = nameParts[nameParts.Length - 1].Replace("_", ".", StringComparison.Ordinal);
        }

        return name;
    }
}
