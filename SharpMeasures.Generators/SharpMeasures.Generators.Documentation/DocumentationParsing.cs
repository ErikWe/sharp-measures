namespace SharpMeasures.Generators.Documentation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

internal static class DocumentationParsing
{
    private static Regex DependencyRegex { get; } = new(@"(?:\r\n|\r|\n|^)#(?:[ ]*)Requires:(?:[ ]*)(?<dependency>[^ ]+?)(?:\r\n|\r|\n|$)", RegexOptions.ExplicitCapture | RegexOptions.Multiline | RegexOptions.Compiled);
    private static Regex UtilityStateRegex { get; } = new(@"(?:\r\n|\r|\n|^)#(?:[ ]*)Utility:(?:[ ]*)(?<state>[^ ]+?)(?:\r\n|\r|\n|$)", RegexOptions.ExplicitCapture | RegexOptions.Multiline | RegexOptions.Compiled);
    private static Regex TagDefinitionRegex { get; } = new(@"(?:\r\n|\r|\n|^)#(?:[ ]*)(?<tag>[^:]+?)(?:\r\n|\r|\n)+(?<content>[\S\s]+?)(?:\r\n|\r|\n)+/#(?:\r\n|\r|\n|$)", RegexOptions.ExplicitCapture | RegexOptions.Multiline | RegexOptions.Compiled);
    private static Regex InvokationIdentifierRegex { get; } = new(@"#(?<tag>[^#\r\n]+?)/#", RegexOptions.ExplicitCapture | RegexOptions.Multiline | RegexOptions.Compiled);
    private static Regex SpecificTagInvokationRegex(string tag) => new($@"#{tag}/#", RegexOptions.Multiline);
    private static Regex NewLineRegex { get; } = new(@"(?:\r\n|\r|\n|^)", RegexOptions.Singleline | RegexOptions.Compiled);

    public static MatchCollection MatchDependencies(string text) => DependencyRegex.Matches(text);

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

    public static MatchCollection MatchTagDefinitions(string text) => TagDefinitionRegex.Matches(text);

    public static IResultWithDiagnostics<Dictionary<string, string>> GetParsedTagDefinitions(string text) => GetParsedTagDefinitions(text, (_) => null);
    public static IResultWithDiagnostics<Dictionary<string, string>> GetParsedTagDefinitions(string text, Func<string, Diagnostic?> diagnosticsDelegate)
    {
        MatchCollection matches = MatchTagDefinitions(text);

        Dictionary<string, string> content = new(matches.Count);
        List<Diagnostic> allDiagnostics = new();

        foreach (Match match in matches)
        {
            var tag = match.Groups["tag"].Value;

            if (content.ContainsKey(tag))
            {
                if (diagnosticsDelegate(tag) is Diagnostic diagnostics)
                {
                    allDiagnostics.Add(diagnostics);
                }

                continue;
            }

            content[match.Groups["tag"].Value] = match.Groups["content"].Value;
        }

        return ResultWithDiagnostics.Construct(content, allDiagnostics);
    }

    public static MatchCollection MatchInvokations(string text) => InvokationIdentifierRegex.Matches(text);
    public static string ResolveInvokation(string tag, string text, string tagText) => SpecificTagInvokationRegex(tag).Replace(text, tagText);

    public static string CommentText(string text) => NewLineRegex.Replace(text, "$0/// ");

    public static string ReadName(AdditionalText file, string extension)
    {
        var fileName = Path.GetFileName(file.Path);

        return fileName.Substring(0, fileName.Length - extension.Length);
    }

    public static bool ReadUtilityState(string text)
    {
        Match match = UtilityStateRegex.Match(text);

        if (match.Success)
        {
            return match.Groups["state"].Value.ToUpperInvariant() is "TRUE";
        }
        else
        {
            return false;
        }
    }
}
