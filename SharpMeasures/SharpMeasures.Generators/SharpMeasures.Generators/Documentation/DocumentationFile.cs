namespace SharpMeasures.Generators.Documentation;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.Diagnostics.Documentation;
using SharpMeasures.Generators.Utility;

using System;
using System.Collections.Generic;
using System.Linq;

internal readonly struct DocumentationFile
{
    public static DocumentationFile Empty { get; } = new(string.Empty, null, new Dictionary<string, string>());

    private string Name { get; }
    private MinimalLocation? Location { get; }
    private IReadOnlyDictionary<string, string> Content { get; }
    
    public DocumentationFile(AdditionalText file, string name, IReadOnlyDictionary<string, string> content)
        : this(name, DetermineFileLocation(file), content) { }

    public DocumentationFile(string name, MinimalLocation? location, IReadOnlyDictionary<string, string> content)
    {
        Name = name;
        Location = location;
        Content = content;
    }

    public ResultWithDiagnostics<string> ResolveTag(string tag)
    {
        if (Content.TryGetValue(tag, out string tagText))
        {
            return ResultWithDiagnostics<string>.WithoutDiagnostics(tagText);
        }

        if (CreateMissingTagDiagnostics(tag) is Diagnostic missingTagDiagnostics)
        {
            return new ResultWithDiagnostics<string>(string.Empty, missingTagDiagnostics);
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
        return Name == other.Name && Location == other.Location && contentIdenticalKeyValuePairs(Content, other.Content);

        static bool contentIdenticalKeyValuePairs(IReadOnlyDictionary<string, string> content, IReadOnlyDictionary<string, string> otherContent)
        {
            foreach (string key in content.Keys)
            {
                if (!otherContent.TryGetValue(key, out string value) || value != content[key])
                {
                    return false;
                }
            }

            return true;
        }
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
