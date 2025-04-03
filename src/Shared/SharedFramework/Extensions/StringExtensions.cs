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
    
    public static string AddSpacesBetweenWords(this string value)
    {
        if (string.IsNullOrEmpty(value)) 
            return value;

        return Regex.Replace(value, "([a-z])([A-Z])", "$1 $2");
    }
    
    public static string RemovePrefix(this string value, string prefix)
    {
        if (!string.IsNullOrEmpty(prefix) && value.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            return value.Substring(prefix.Length);

        return value;
    }
    
    public static string RemovePostfix(this string value, string postfix)
    {
        if (!string.IsNullOrEmpty(postfix) && value.EndsWith(postfix, StringComparison.OrdinalIgnoreCase))
            return value.Substring(0, value.Length - postfix.Length);

        return value;
    }
}
