using Scriban;
using Scriban.Parsing;
using Scriban.Runtime;

namespace Mafin.Wizard.Api;

public class ResourceTemplateLoader : ITemplateLoader
{
    private readonly ResourceTemplateProvider _templateProvider;
    private readonly TemplateReader _templateReader;

    public ResourceTemplateLoader(TemplateReader templateReader, ResourceTemplateProvider templateProvider)
    {
        _templateReader = templateReader;
        _templateProvider = templateProvider;
    }

    public string Load(TemplateContext context, SourceSpan callerSpan, string templatePath)
    {
        return _templateReader.Read(templatePath);
    }

    public async ValueTask<string> LoadAsync(TemplateContext context, SourceSpan callerSpan, string templatePath)
    {
        return await _templateReader.ReadAsync(templatePath);
    }

    public string GetPath(TemplateContext context, SourceSpan callerSpan, string templateName)
    {
        try
        {
            return _templateProvider.GetFullTemplateName(templateName);
        }
        catch (FileNotFoundException)
        {
            return templateName;
        }
    }
}
