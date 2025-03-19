using System.Text.RegularExpressions;

namespace SharedFramework.Extensions;

public static class StringExtensions
{
    private static readonly Regex EmailRegex = new Regex(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$", 
        RegexOptions.Compiled | RegexOptions.IgnoreCase
    );

    public static bool IsValidEmailForm(this string form)
    {
        if (string.IsNullOrWhiteSpace(form))
            return false;

        return EmailRegex.IsMatch(form);
    }
}
