namespace SharpMeasures.Generators.Documentation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics.Documentation;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;
using System.Collections.ObjectModel;

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

    public ResultWithDiagnostics<string> ResolveTag(string tag)
    {
        if (Content.TryGetValue(tag, out string tagText))
        {
            return ResultWithDiagnostics<string>.WithoutDiagnostics(tagText);
        }
        else
        {
            if (CreateMissingTagDiagnostics(tag) is Diagnostic missingTagDiagnostics)
            {
                return new ResultWithDiagnostics<string>(string.Empty, missingTagDiagnostics);
            }
        }

        return ResultWithDiagnostics<string>.WithoutDiagnostics(string.Empty);
    }

    public string ResolveTagAndReportDiagnostics(SourceProductionContext context, string tag)
    {
        ResultWithDiagnostics<string> resultAndDiagnostics = ResolveTag(tag);

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
