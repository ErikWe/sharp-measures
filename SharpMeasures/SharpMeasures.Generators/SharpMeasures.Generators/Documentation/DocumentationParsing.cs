namespace SharpMeasures.Generators.Documentation;

using System.Collections.Generic;
using System.Text.RegularExpressions;

internal static class DocumentationParsing
{
    public static Match MatchName(string text)
    {
        Regex regex = new(@"^#Name:(?:[ ]*)(?<name>[^ ]+?)(?:#?)$", RegexOptions.ExplicitCapture);
        return regex.Match(text);
    }

    public static MatchCollection MatchDependencies(string text)
    {
        Regex regex = new(@"^#Requires:(?:[ ]*)(?<dependency>[^ ]+?)(?:#?)$", RegexOptions.ExplicitCapture);
        return regex.Matches(text);
    }

    public static IReadOnlyCollection<string> GetDependencies(string text)
    {
        MatchCollection matches = MatchDependencies(text);
        List<string> dependencies = new();

        foreach (Match match in matches)
        {
            dependencies.Add(match.Groups["dependency"].Value);
        }

        return dependencies;
    }

    public static MatchCollection MatchDefinitions(string text)
    {
        Regex regex = new(@"^#Define:(?:[ ]*)(?<tag>.+?)(?:\r\n|\r|\n)+(?<content>[\S\s]+?)(?:\r\n|\r|\n)*/#$", RegexOptions.ExplicitCapture);
        return regex.Matches(text);
    }

    public static Dictionary<string, string> GetParsedDefinitions(string text)
    {
        MatchCollection matches = MatchDefinitions(text);

        Dictionary<string, string> content = new(matches.Count);

        foreach (Match match in matches)
        {
            content[match.Groups["tag"].Value] = match.Groups["content"].Value;
        }

        return content;
    }

    public static MatchCollection MatchInvokations(string text)
    {
        Regex regex = new(@"#(?<tag>[^#]+?)/#", RegexOptions.ExplicitCapture);
        return regex.Matches(text);
    }

    public static string ResolveInvokation(string tag, string text, string tagText)
    {
        Regex regex = new($@"#{tag}/#", RegexOptions.ExplicitCapture);
        return regex.Replace(text, tagText);
    }
}
