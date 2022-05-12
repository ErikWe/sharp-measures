namespace SharpMeasures.Generators.Documentation;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.Diagnostics.Documentation;
using SharpMeasures.Generators.Utility;

using System;
using System.Collections.Generic;
using System.Linq;

internal readonly record struct DocumentationFile(string Name, MinimalLocation? Location, IReadOnlyDictionary<string, string> Content)
{
    public DocumentationFile(AdditionalText file, string name, IReadOnlyDictionary<string, string> content)
        : this(name, DetermineFileLocation(file), content) { }

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

    private static MinimalLocation? DetermineFileLocation(AdditionalText file)
    {
        if (file.GetText() is not SourceText text)
        {
            return null;
        }

        LinePositionSpan span = new(new LinePosition(0, 0), new LinePosition(text.Lines.Count - 1, text.Lines[text.Lines.Count - 1].End));
        return MinimalLocation.FromLocation(Microsoft.CodeAnalysis.Location.Create(file.Path, TextSpan.FromBounds(0, file.ToString().Length - 1), span));
    }

    private Diagnostic? CreateMissingTagDiagnostics(string tag)
    {
        if (Location is null)
        {
            return null;
        }

        return DocumentationFileMissingRequestedTagDiagnostics.Create(Location.Value.ToLocation(), Name, tag);
    }

    public bool Equals(DocumentationFile other)
    {
        return Name == other.Name && Location == other.Location
            && Content.OrderBy(static (x) => x.Key).SequenceEqual(other.Content.OrderBy(static (x) => x.Key));
    }

    public override int GetHashCode()
    {
        int hashCode = (Name, Location).GetHashCode();

        foreach (KeyValuePair<string, string> entry in Content)
        {
            hashCode ^= entry.GetHashCode();
        }

        return hashCode;
    }
}
