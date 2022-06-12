﻿namespace SharpMeasures.Generators.Documentation;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Diagnostics;

using System;
using System.Collections.Generic;

public class DocumentationFile : IEquatable<DocumentationFile>
{
    public static DocumentationFile Empty { get; } = new(string.Empty, null as MinimalLocation?, new Dictionary<string, string>(), new EmptyDiagnosticsStrategy());

    public string Name { get; }
    public MinimalLocation? Location { get; }
    private ReadOnlyEquatableDictionary<string, string> Content { get; }

    private IDiagnosticsStrategy DiagnosticsStrategy { get; }

    internal DocumentationFile(string name, AdditionalText file, IReadOnlyDictionary<string, string> content, IDiagnosticsStrategy diagnosticsStrategy)
        : this(name, DetermineFileLocation(file), content, diagnosticsStrategy) { }

    internal DocumentationFile(string name, MinimalLocation? location, IReadOnlyDictionary<string, string> content, IDiagnosticsStrategy diagnosticsStrategy)
    {
        Name = name;
        Location = location;
        Content = content.AsReadOnlyEquatable();

        DiagnosticsStrategy = diagnosticsStrategy;
    }

    public IResultWithDiagnostics<string> ResolveTag(string tag)
    {
        if (string.IsNullOrEmpty(Name))
        {
            return ResultWithDiagnostics.Construct(string.Empty);
        }

        if (Content.TryGetValue(tag, out string tagText))
        {
            return ResultWithDiagnostics.Construct(tagText);
        }

        var diagnostics = DiagnosticsStrategy.DocumentationFileMissingRequestedTag((Location ?? MinimalLocation.None).AsRoslynLocation(), Name, tag);
        return ResultWithDiagnostics.Construct(string.Empty, diagnostics);
    }

    public string ResolveTagAndReportDiagnostics(SourceProductionContext context, string tag)
    {
        IResultWithDiagnostics<string> resultAndDiagnostics = ResolveTag(tag);

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
        return Microsoft.CodeAnalysis.Location.Create(file.Path, TextSpan.FromBounds(0, file.ToString().Length - 1), span).Minimize();
    }

    public virtual bool Equals(DocumentationFile other)
    {
        return Name == other.Name && Location == other.Location && Content == other.Content;
    }

    public override bool Equals(object obj)
    {
        if (obj is DocumentationFile other)
        {
            return Equals(other);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return (Name, Location, Content).GetHashCode();
    }
}