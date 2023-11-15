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
}
