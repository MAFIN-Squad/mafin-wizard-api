using CommandLine;
using Mafin.Wizard.Api;
using Mafin.Wizard.Api.Models;

namespace Mafin.Wizard.Cli;

internal static class EntryPoint
{
    internal static void Main(string[] args)
    {
        var parserResult = Parser.Default.ParseArguments<WizardParameters>(args);
        parserResult.WithParsed(HandleCreateCommand);
        parserResult.WithNotParsed(HandleParseError);
    }

    private static void HandleCreateCommand(WizardParameters parameters)
    {
        var features = new List<string> { $"CSharp.TestRunners.{parameters.TestRunner}" };
        features.AddRange(parameters.TestTypes.Select(x => $"CSharp.TestTypes.{x}"));

        if (parameters.WithConfigModule)
        {
            features.Add("CSharp.Features.Configuration");
        }

        var wizardSettings = new WizardSettings(parameters.Name, parameters.Language, features.ToArray());
        var solutionModel = new Generator(wizardSettings).Create();
        new SolutionCreator().Create(solutionModel);
    }

    private static void HandleParseError(IEnumerable<Error> errs)
    {
        if (!errs.IsHelp())
        {
            throw new ArgumentException(string.Join(",", errs.Select(e => e.ToString())));
        }
    }
}
