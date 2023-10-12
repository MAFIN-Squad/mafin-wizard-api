using Mafin.Wizard.Api.Models;
using Scriban;
using Scriban.Runtime;
using SharpCompress;

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

    public void Create()
    {
        var solution = new SolutionModel(_settings, RenderTemplates());
        new SolutionCreator(solution).CreateSolution();
    }

    private TemplateContext CreateTemplateContext()
    {
        var templateContext = new TemplateContext();
        var scriptObject = new ScriptObject { { "tafName", _settings.TafName } };
        _settings.Parameters?.ForEach(x => scriptObject.Add(x.Key, x.Value));
        templateContext.PushGlobal(scriptObject);

        var loader = new ResourceTemplateLoader(_templateReader, _templateProvider);
        templateContext.TemplateLoader = loader;

        return templateContext;
    }

    private List<FileInfoRecord> RenderTemplates()
    {
        var result = new List<FileInfoRecord>();

        var factory = new ResultFileFactory();
        foreach (var value in _templateReader.ReadAll())
        {
            #pragma warning disable CA1031 // Do not catch general exception types
            try
            {
                var fileContent = Template.Parse(value.Value).Render(_templateContext);
                result.Add(factory.CreateResultFileRecord(value.Key, fileContent));
            }
            catch
            {
                // ignore
            }
            #pragma warning restore CA1031
        }

        return result;
    }
}
