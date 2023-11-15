using Mafin.Wizard.Api.Constants;

namespace Mafin.Wizard.Api.Models;

internal record ScribanTemplate
{
    public ScribanTemplate(string name, string content)
    {
        Name = name;
        Content = content;
    }

    public string Name { get; private set; }

    public string Content { get; private set; }

    // public string GetFileExtension()
    // {
    //     var rawExtension = Path.GetExtension(Name);
    //     var prefix = ScribanFileExtensions.ScriptExtensions.FirstOrDefault(x => rawExtension.Equals(x, StringComparison.OrdinalIgnoreCase));

    //     if (string.IsNullOrEmpty(prefix))
    //     {
    //         throw new ArgumentException($"{Name} file is not scriban template", nameof(Name));
    //     }

    //     var result = rawExtension.Replace(prefix, string.Empty, StringComparison.OrdinalIgnoreCase);

    //     // if (string.IsNullOrEmpty(result))
    //     // {
    //     //     throw new ArgumentException($"{Name} file is not a template with the extension", nameof(Name));
    //     // }

    //     return result;
    // }

    // public bool IsScriptTemplate() => ScribanFileExtensions.ScriptExtensions.Any(Name.EndsWith);

    // public bool IsFileTemplate() => ScribanFileExtensions.FileExtensions.Any(x =>
    //     {
    //         var extension = Path.GetExtension(Name);
    //         return extension.StartsWith(x) && !extension.Equals(x);
    //     });
}