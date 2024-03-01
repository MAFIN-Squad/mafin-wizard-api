namespace Mafin.Wizard.Api.Extensions;

internal static class StringExtensions
{
    public static string TrimEnd(this string input, string stringToRemove)
    {
        if (string.IsNullOrEmpty(stringToRemove) || string.IsNullOrEmpty(input))
        {
            return input;
        }

        while (!string.IsNullOrEmpty(input) && input.EndsWith(stringToRemove, StringComparison.Ordinal))
        {
            input = input[..^stringToRemove.Length];
        }

        return input;
    }
}
