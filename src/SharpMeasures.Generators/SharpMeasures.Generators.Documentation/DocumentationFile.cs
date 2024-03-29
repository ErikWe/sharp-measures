﻿namespace SharpMeasures.Generators.Documentation;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Configuration;
using SharpMeasures.Generators.Diagnostics;

using System;
using System.Collections.Generic;

public sealed class DocumentationFile : IEquatable<DocumentationFile>
{
    public static DocumentationFile Empty { get; } = new(string.Empty, null as MinimalLocation?, new Dictionary<string, string>(), new EmptyDiagnosticsStrategy(), GlobalAnalyzerConfig.Default, false);

    public string Name { get; }
    public MinimalLocation? Location { get; }
    private IReadOnlyDictionary<string, string> Content { get; }

    private IDiagnosticsStrategy DiagnosticsStrategy { get; }

    private GlobalAnalyzerConfig Configuration { get; }
    private bool HasReportedOneMissingTag { get; set; }

    internal DocumentationFile(string name, AdditionalText file, IReadOnlyDictionary<string, string> content, IDiagnosticsStrategy diagnosticsStrategy, GlobalAnalyzerConfig configuration, bool hasReportedOneMissingTag)
        : this(name, DetermineFileLocation(file), content, diagnosticsStrategy, configuration, hasReportedOneMissingTag) { }

    internal DocumentationFile(string name, MinimalLocation? location, IReadOnlyDictionary<string, string> content, IDiagnosticsStrategy diagnosticsStrategy, GlobalAnalyzerConfig configuration, bool hasReportedOneMissingTag)
    {
        Name = name;
        Location = location;
        Content = content.AsReadOnlyEquatable();

        DiagnosticsStrategy = diagnosticsStrategy;

        Configuration = configuration;
        HasReportedOneMissingTag = hasReportedOneMissingTag;
    }

    public string? OptionallyResolveTag(string tag)
    {
        if (string.IsNullOrEmpty(Name))
        {
            return null;
        }

        Content.TryGetValue(tag, out var tagText);
        return tagText;
    }

    public IResultWithDiagnostics<string> ResolveTag(string tag)
    {
        if (string.IsNullOrEmpty(Name))
        {
            return ResultWithDiagnostics.Construct(string.Empty);
        }

        if (OptionallyResolveTag(tag) is not string tagText)
        {
            return ResultWithDiagnostics.Construct(string.Empty, CreateMissingTagDiagnostics(tag));
        }

        return ResultWithDiagnostics.Construct(tagText);
    }

    public string ResolveTagAndReportDiagnostics(SourceProductionContext context, string tag)
    {
        var resultAndDiagnostics = ResolveTag(tag);

        context.ReportDiagnostics(resultAndDiagnostics);

        return resultAndDiagnostics.Result;
    }

    private Diagnostic? CreateMissingTagDiagnostics(string tag)
    {
        if (DiagnosticsStrategy.GenerateDiagnostics is false || (Configuration.LimitOneErrorPerDocumentationFile && HasReportedOneMissingTag))
        {
            return null;
        }

        HasReportedOneMissingTag = true;
        return DiagnosticsStrategy.DocumentationFileMissingRequestedTag((Location ?? MinimalLocation.None).AsRoslynLocation(), Name, tag);
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

    public bool Equals(DocumentationFile? other) => other is not null && Name == other.Name && Location == other.Location && Content == other.Content;
    public override bool Equals(object? obj) => obj is DocumentationFile other && Equals(other);

    public static bool operator ==(DocumentationFile? lhs, DocumentationFile? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(DocumentationFile? lhs, DocumentationFile? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => (Name, Location, Content).GetHashCode();
}
