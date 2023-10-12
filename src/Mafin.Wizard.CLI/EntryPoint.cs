using CommandLine;
using Mafin.Wizard.Api;
using Mafin.Wizard.Api.Models;

namespace Mafin.Wizard.Cli;

internal static class EntryPoint
{
    internal static void Main(string[] args)
    {
        var parserResult = CommandLine.Parser.Default.ParseArguments<WizardParameters>(args);
        parserResult.WithParsed<WizardParameters>(HandleCreateCommand);
        parserResult.WithNotParsed(HandleParseError);
    }

    private static void HandleCreateCommand(WizardParameters parameters)
    {
        var features = new List<string>
        {
            $"TestRunners.{parameters.TestRunner}",
            $"Features.Configuration"
        };
        features.AddRange(parameters.TestTypes.Select(x => $"TestTypes.{x}"));

        var wizardSettings = new WizardSettings(parameters.Name, parameters.Language, features.ToArray());
        new Generator(wizardSettings).Create();
    }

    private static void HandleParseError(IEnumerable<Error> errs)
    {
        if (!errs.IsHelp())
        {
            throw new ArgumentException(string.Join(",", errs.Select(e => e.ToString())));
        }
    }
}
