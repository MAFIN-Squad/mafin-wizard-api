namespace Mafin.Wizard.Api.Extensions;

internal static class StringExtensions
{
    public static string TrimEnd(this string input, string stringToRemove)
    {
        if (string.IsNullOrEmpty(stringToRemove) || string.IsNullOrEmpty(input))
        {
            return input;
        }

        while (input != string.Empty && input.EndsWith(stringToRemove))
        {
            input = input[..^stringToRemove.Length];
        }

        return input;
    }
}