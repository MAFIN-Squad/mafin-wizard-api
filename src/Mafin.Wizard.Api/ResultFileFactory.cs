using System.Text.RegularExpressions;
using Mafin.Wizard.Api.Models;

namespace Mafin.Wizard.Api;

public class ResultFileFactory
{
    private const string PathPattern = "#### path: (.*?) ####";
    private const string FileNamePattern = "#### name: (.*?) ####";

    private static readonly string[] ScribanFileExtensions = new[]
    {
        ".sbn-",
        ".sbn",
        ".scriban-",
        ".scriban"
    };

    public FileInfoRecord CreateResultFileRecord(string templateName, string data)
    {
        var filename = GetFileName(templateName, ref data);
        var extension = GetExtension(templateName);
        var path = GetFilePath(ref data);

        return new FileInfoRecord(filename, extension, path, data);
    }

    private static string GetExtension(string filePath)
    {
        var rawExtension = Path.GetExtension(filePath);
        var prefix = ScribanFileExtensions.FirstOrDefault(x => rawExtension.StartsWith(x, StringComparison.OrdinalIgnoreCase));

        if (string.IsNullOrEmpty(prefix))
        {
            throw new ArgumentException($"{filePath} file is not scriban template", nameof(filePath));
        }

        var result = rawExtension.Replace(prefix, string.Empty, StringComparison.OrdinalIgnoreCase);

        if (string.IsNullOrEmpty(result))
        {
            throw new ArgumentException($"{filePath} file is not a template with the extension", nameof(filePath));
        }

        return result;
    }

    private string GetFilePath(ref string fileData)
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
        }

        var nameParts = name.Split(".");
        return nameParts[nameParts.Length - 1].Replace("_", ".", StringComparison.Ordinal);
    }
}
