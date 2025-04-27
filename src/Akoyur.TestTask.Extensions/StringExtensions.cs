namespace Akoyur.TestTask.Extensions;

/// <summary>
/// Extension methods for the string class.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Converts the first character of a string to lowercase, making it camel case.
    /// </summary>
    /// <param name="input">The string to convert to camel case.</param>
    /// <returns>The input string with the first character converted to lowercase.</returns>
    public static string ToCamelCase(this string input)
    {
        if (string.IsNullOrEmpty(input) || char.IsLower(input[0]))
            return input;

        return char.ToLowerInvariant(input[0]) + input.Substring(1);
    }
}
