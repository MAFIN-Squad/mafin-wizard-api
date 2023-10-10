using CommandLine;

namespace Mafin.Wizard.Cli;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
[Verb("create")]
public class WizardParameters
{
    [Option('n', "name", Required = true)]
    public string Name { get; set; }

    [Option('l', "language", Required = true)]
    public string Language { get; set; }

    [Option('r', "testRunner", Required = true)]
    public string TestRunner { get; set; }

    [Option('t', "testTypes", Required = true)]
    public IEnumerable<string> TestTypes { get; set; }
}
#pragma warning restore CS8618
