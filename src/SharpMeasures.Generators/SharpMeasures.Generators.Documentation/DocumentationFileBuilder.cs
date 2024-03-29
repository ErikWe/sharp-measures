﻿namespace SharpMeasures.Generators.Documentation;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Configuration;
using SharpMeasures.Generators.Diagnostics;

using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

internal sealed class DocumentationFileBuilder
{
    public static IResultWithDiagnostics<DocumentationDictionary> Build(IEnumerable<Optional<AdditionalText>> relevantFiles, IDiagnosticsStrategy diagnosticsStrategy, GlobalAnalyzerConfig configuration)
    {
        Dictionary<string, DocumentationFileBuilder> builders = new();

        foreach (var builder in createBuilders())
        {
            builders.TryAdd(builder.Name, builder);
        }

        foreach (var builder in builders.Values)
        {
            if (builder.IsUtility is false)
            {
                builder.ResolveDependencies(builders);
            }
        }

        foreach (var builder in builders.Values)
        {
            if (builder.IsUtility is false)
            {
                builder.AddTagsFromDependencies();
            }
        }

        foreach (var builder in builders.Values)
        {
            if (builder.IsUtility is false)
            {
                builder.ResolveAllTagContents();
            }
        }

        foreach (var builder in builders.Values)
        {
            if (builder.IsUtility is false)
            {
                builder.CommentAllTags();
            }
        }

        DocumentationDictionary dictionary = new(builders.Transform(static (builder) => builder.Finalize()));
        var diagnostics = builders.Values.SelectMany(static (file) => file.Diagnostics);

        return ResultWithDiagnostics.Construct(dictionary, diagnostics);

        IEnumerable<DocumentationFileBuilder> createBuilders()
        {
            foreach (var additionalText in relevantFiles)
            {
                if (additionalText.HasValue && additionalText.Value.GetText() is SourceText fileText)
                {
                    yield return new DocumentationFileBuilder(additionalText.Value, fileText, diagnosticsStrategy, configuration);
                }
            }
        }
    }

    private const string SelfTypeTag = "SelfType";

    private AdditionalText File { get; }
    private SourceText FileText { get; }

    public string Name { get; }
    private bool IsUtility { get; }
    private Dictionary<string, string> Content { get; set; }
    private HashSet<string> ResolvedTags { get; } = new();

    private List<DocumentationFileBuilder> ResolvedDependencies { get; } = new();
    private IReadOnlyCollection<string> Dependencies { get; }

    private IDiagnosticsStrategy DiagnosticsStrategy { get; }
    private List<Diagnostic> Diagnostics { get; } = new();

    private GlobalAnalyzerConfig Configuration { get; }
    private bool HasReportedOneUnresolvedDependency { get; set; }
    private bool HasReportedOneDuplicateTag { get; set; }
    private bool HasReportedOneMissingTag { get; set; }

    private DocumentationFileBuilder(AdditionalText file, SourceText fileText, IDiagnosticsStrategy diagnosticsStrategy, GlobalAnalyzerConfig configuration)
    {
        File = file;
        FileText = fileText;

        var text = fileText.ToString();

        DiagnosticsStrategy = diagnosticsStrategy;
        Configuration = configuration;

        Name = DocumentationParsing.ReadName(file, configuration.DocumentationFileExtension);
        IsUtility = DocumentationParsing.ReadUtilityState(text);
        Dependencies = DocumentationParsing.GetDependencies(text);

        var contentWithDiagnostics = DocumentationParsing.GetParsedTagDefinitions(text, CreateDuplicateTagDiagnostics);

        Content = contentWithDiagnostics.Result;
        Diagnostics.AddRange(contentWithDiagnostics);
    }

    public DocumentationFile Finalize() => new(Name, File, Content.AsReadOnlyEquatable(), DiagnosticsStrategy, Configuration, HasReportedOneMissingTag);

    private void ResolveDependencies(Dictionary<string, DocumentationFileBuilder> documentationFiles)
    {
        foreach (var dependency in Dependencies)
        {
            if (documentationFiles.TryGetValue(dependency, out var file))
            {
                ResolvedDependencies.Add(file);
                continue;
            }

            if (CreateUnresolvedDependencyDiagnostics(dependency) is Diagnostic diagnostics)
            {
                Diagnostics.Add(diagnostics);
            }
        }
    }

    private void AddTagsFromDependencies()
    {
        foreach (var dependency in ResolvedDependencies)
        {
            foreach (var tag in dependency.Content)
            {
                if (Content.ContainsKey(tag.Key) is false)
                {
                    Content[tag.Key] = tag.Value;
                }
            }
        }

        if (Content.ContainsKey(SelfTypeTag) is false)
        {
            Content[SelfTypeTag] = Name;
        }
    }

    private void ResolveAllTagContents()
    {
        List<string> keys = new(Content.Keys);

        foreach (var tag in keys)
        {
            if (ResolvedTags.Contains(tag) is false)
            {
                ResolveTagContent(tag);
            }
        }
    }

    private void CommentAllTags()
    {
        List<string> keys = new(Content.Keys);

        foreach (var tag in keys)
        {
            Content[tag] = DocumentationParsing.CommentText(Content[tag]);
        }
    }

    private void ResolveTagContent(string tag)
    {
        ResolvedTags.Add(tag); // This is done before actually resolving, otherwise recursive tags loop indefinitely

        Content[tag] = ResolveText(Content[tag]);
    }

    private string ResolveText(string text)
    {
        var matches = DocumentationParsing.MatchInvokations(text);

        foreach (Match match in matches)
        {
            var tag = match.Groups["tag"].Value;

            if (ReadTag(tag) is not string tagText)
            {
                if (CreateMissingTagDiagnostics(tag) is Diagnostic diagnostics)
                {
                    Diagnostics.Add(diagnostics);
                }

                continue;
            }

            text = DocumentationParsing.ResolveInvokation(tag, text, tagText);
        }

        return text;
    }

    private string? ReadTag(string tag)
    {
        if (Content.TryGetValue(tag, out var text))
        {
            if (ResolvedTags.Contains(tag))
            {
                return text;
            }

            ResolveTagContent(tag);
            return Content[tag];
        }

        return null;
    }

    private Diagnostic? CreateUnresolvedDependencyDiagnostics(string dependency)
    {
        if (DiagnosticsStrategy.GenerateDiagnostics is false || (Configuration.LimitOneErrorPerDocumentationFile && HasReportedOneUnresolvedDependency))
        {
            return null;
        }

        var matches = DocumentationParsing.MatchDependencies(FileText.ToString());

        foreach (Match match in matches)
        {
            if (match.Groups["dependency"].Value == dependency)
            {
                var line = FileText.ToString().Substring(0, match.Index).Count(static (character) => character is '\n');

                if (line is 0)
                {
                    line = FileText.ToString().Substring(0, match.Index).Count(static (character) => character is '\r');
                }

                HasReportedOneUnresolvedDependency = true;

                var textSpan = FileText.Lines[line].Span;
                LinePositionSpan lineSpan = new(new LinePosition(line, 0), new LinePosition(line, textSpan.Length - 1));
                var location = Location.Create(File.Path, new TextSpan(match.Index, match.Length), lineSpan);

                return DiagnosticsStrategy.UnresolvedDocumentationDependency(location, Name, dependency);
            }
        }

        return null;
    }

    private Diagnostic? CreateDuplicateTagDiagnostics(string tag)
    {
        if (DiagnosticsStrategy.GenerateDiagnostics is false || (Configuration.LimitOneErrorPerDocumentationFile && HasReportedOneDuplicateTag))
        {
            return null;
        }

        HasReportedOneDuplicateTag = true;

        if (FileText.Lines.Count is 0)
        {
            return DiagnosticsStrategy.DocumentationFileDuplicateTag(Location.None, Name, tag);
        }

        return DiagnosticsStrategy.DocumentationFileDuplicateTag(GetLocation(), Name, tag);
    }

    private Diagnostic? CreateMissingTagDiagnostics(string tag)
    {
        if (DiagnosticsStrategy.GenerateDiagnostics is false || (Configuration.LimitOneErrorPerDocumentationFile && HasReportedOneMissingTag))
        {
            return null;
        }

        HasReportedOneMissingTag = true;

        if (FileText.Lines.Count is 0)
        {
            return DiagnosticsStrategy.DocumentationFileMissingRequestedTag(Location.None, Name, tag);
        }

        return DiagnosticsStrategy.DocumentationFileMissingRequestedTag(GetLocation(), Name, tag);
    }

    private Location GetLocation() => Location.Create(File.Path, new TextSpan(), new LinePositionSpan());
}
