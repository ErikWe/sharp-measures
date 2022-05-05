namespace SharpMeasures.Generators.Documentation;

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

internal static class DocumentationParsing
{
    private static Regex DependencyRegex { get; }
        = new(@"^#Requires:(?:[ ]*)(?<dependency>[^ ]+?)(?:\r\n|\r|\n|$)", RegexOptions.ExplicitCapture | RegexOptions.Multiline | RegexOptions.Compiled);

    private static Regex TagDefinitionRegex { get; }
        = new(@"^#(?:[ ]*)(?<tag>.+?)(?:\r\n|\r|\n)+(?<content>[\S\s]+?)(?:\r\n|\r|\n)*/#(?:\r\n|\r|\n|$)",
            RegexOptions.ExplicitCapture | RegexOptions.Multiline | RegexOptions.Compiled);

    private static Regex InvokationIdentifierRegex { get; }
        = new(@"^(?<indent>[^\S\r\n]*)#(?<tag>[^#]+?)/#(?:\r\n|\r|\n|$)", RegexOptions.ExplicitCapture | RegexOptions.Multiline | RegexOptions.Compiled);

    private static Regex AnonymousInvokationRegex { get; }
        = new (@"^(?:[^\S\r\n]*)#(?:[^#]+?)/#(?:\r\n|\r|\n|$)", RegexOptions.Multiline | RegexOptions.Compiled);

    private static Regex IndentationAndCommentRegex { get; }
        = new(@"^", RegexOptions.Multiline | RegexOptions.Compiled);

    private static Regex TagInvokationRegex(string tag)
        => new($@"^(?:[^\S\r\n]*)#{tag}/#(?:\r\n|\r|\n|$)", RegexOptions.Multiline);

    public static MatchCollection MatchDependencies(string text)
    {
        return DependencyRegex.Matches(text);
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

    public static MatchCollection MatchTagDefinitions(string text)
    {
        return TagDefinitionRegex.Matches(text);
    }

    public static Dictionary<string, string> GetParsedTagDefinitions(string text)
    {
        MatchCollection matches = MatchTagDefinitions(text);

        Dictionary<string, string> content = new(matches.Count);

        foreach (Match match in matches)
        {
            content[match.Groups["tag"].Value] = match.Groups["content"].Value;
        }

        return content;
    }

    public static MatchCollection MatchInvokations(string text)
    {
        return InvokationIdentifierRegex.Matches(text);
    }

    public static string ResolveInvokation(string indentation, string tag, string text, string tagText)
    {
        string indentedAndCommentedTagText = IndentationAndCommentRegex.Replace(tagText, $"{indentation}/// $0");

        return ResolveInvokation(tag, text, indentedAndCommentedTagText);
    }

    public static string ResolveInvokation(string tag, string text, string tagText)
    {
        return TagInvokationRegex(tag).Replace(text, tagText + Environment.NewLine);
    }

    public static string RemoveAllInvokations(string text)
    {
        return AnonymousInvokationRegex.Replace(text, string.Empty);
    }
}
