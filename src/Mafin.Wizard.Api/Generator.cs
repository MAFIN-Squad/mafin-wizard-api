using Mafin.Wizard.Api.Models;
using Scriban;
using Scriban.Runtime;

namespace Mafin.Wizard.Api;

public class Generator
{
    private readonly WizardSettings _settings;
    private readonly TemplateContext _templateContext;
    private readonly ResourceTemplateProvider _templateProvider;
    private readonly TemplateReader _templateReader;

    public Generator(WizardSettings settings)
    {
        _settings = settings;
        _templateProvider = new ResourceTemplateProvider(new string[] { string.Empty, _settings.Language }, _settings.Features.ToArray());
        _templateReader = new TemplateReader(_templateProvider);
        _templateContext = CreateTemplateContext();
    }

    public SolutionModel Create()
    {
        return new SolutionModel(_settings.AppName, RenderTemplates());
    }

    private TemplateContext CreateTemplateContext()
    {
        var templateContext = new TemplateContext();
        var scriptObject = new ScriptObject
        {
            { "appName", _settings.AppName },
            { "projGuid", Guid.NewGuid().ToString() },
            { "slnGuid", Guid.NewGuid().ToString() },
        };

        foreach(var param in _settings.Parameters)
        {
            scriptObject.Add(param.Key, param.Value);
        }

        templateContext.PushGlobal(scriptObject);

        var loader = new ResourceTemplateLoader(_templateReader, _templateProvider);
        templateContext.TemplateLoader = loader;

        return templateContext;
    }

    private List<FileInfoRecord> RenderTemplates()
    {
        var factory = new ResultFileFactory();
        return _templateReader.ReadAll()
            .Select(template => factory.CreateResultFileRecord(template, _templateContext))
            .Where(x => !string.IsNullOrWhiteSpace(x.Data))
            .ToList();
    }
}
