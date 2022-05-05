namespace SharpMeasures.Generators.Documentation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics.Documentation;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

internal readonly record struct DocumentationFile
{
    public static DocumentationFile Empty => new(string.Empty, new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()));

    private AdditionalText? File { get; }

    public string Name { get; }
    
    private ReadOnlyDictionary<string, string> Content { get; }

    private DocumentationFile(string name, ReadOnlyDictionary<string, string> content)
    {
        File = null;
        Name = name;
        Content = content;
    }

    public DocumentationFile(AdditionalText file, string name, ReadOnlyDictionary<string, string> content)
    {
        File = file;
        Name = name;
        Content = content;
    }

    public ResultWithDiagnostics<string> ResolveText(string text)
    {
        if (Content.Count is 0)
        {
            text = DocumentationParsing.RemoveAllInvokations(text);

            return ResultWithDiagnostics<string>.WithoutDiagnostics(text);
        }

        MatchCollection matches = DocumentationParsing.MatchInvokations(text);
        List<Diagnostic> diagnostics = new();

        foreach (Match match in matches)
        {
            string tag = match.Groups["tag"].Value;

            if (Content.TryGetValue(tag, out string tagText))
            {
                text = DocumentationParsing.ResolveInvokation(match.Groups["indent"].Value, tag, text, tagText);
            }
            else
            {
                if (CreateMissingTagDiagnostics(tag) is Diagnostic missingTagDiagnostics)
                {
                    diagnostics.Add(missingTagDiagnostics);
                }
            }
        }

        text = DocumentationParsing.RemoveAllInvokations(text);

        return new ResultWithDiagnostics<string>(text, diagnostics);
    }

    public string ResolveTextAndReportDiagnostics(SourceProductionContext context, string text)
    {
        ResultWithDiagnostics<string> resultAndDiagnostics = ResolveText(text);

        context.ReportDiagnostics(resultAndDiagnostics);

        return resultAndDiagnostics.Result;
    }

    private Diagnostic? CreateMissingTagDiagnostics(string tag)
    {
        if (File is null)
        {
            return null;
        }

        return DocumentationFileMissingRequestedTagDiagnostics.Create(File, tag);
    }
}
