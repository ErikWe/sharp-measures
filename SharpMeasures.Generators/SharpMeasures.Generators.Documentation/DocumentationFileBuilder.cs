namespace SharpMeasures.Generators.Documentation;

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
        Dictionary<string, DocumentationFileBuilder> builders = createBuilders().ToDictionary(static (builder) => builder.Name);

        foreach (DocumentationFileBuilder builder in builders.Values)
        {
            if (builder.IsUtility is false)
            {
                builder.ResolveDependencies(builders);
            }
        }

        foreach (DocumentationFileBuilder builder in builders.Values)
        {
            if (builder.IsUtility is false)
            {
                builder.AddTagsFromDependencies();
            }
        }

        foreach (DocumentationFileBuilder builder in builders.Values)
        {
            if (builder.IsUtility is false)
            {
                builder.ResolveAllTagContents();
            }
        }

        foreach (DocumentationFileBuilder builder in builders.Values)
        {
            if (builder.IsUtility is false)
            {
                builder.CommentAllTags();
            }
        }

        DocumentationDictionary dictionary = new(builders.Values.ToDictionary(static (file) => file.Name, (file) => file.Finalize()));
        IEnumerable<Diagnostic> diagnostics = builders.Values.SelectMany(static (file) => file.Diagnostics);

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
    private bool HasReportedOneMissingTag { get; set; }

    private DocumentationFileBuilder(AdditionalText file, SourceText fileText, IDiagnosticsStrategy diagnosticsStrategy, GlobalAnalyzerConfig configuration)
    {
        File = file;
        FileText = fileText;

        var text = fileText.ToString();

        Name = DocumentationParsing.ReadName(file, configuration.DocumentationFileExtension);
        IsUtility = DocumentationParsing.ReadUtilityState(text);
        Dependencies = DocumentationParsing.GetDependencies(text);
        Content = DocumentationParsing.GetParsedTagDefinitions(text);

        DiagnosticsStrategy = diagnosticsStrategy;
        Configuration = configuration;
    }

    public DocumentationFile Finalize() => new(Name, File, Content.AsReadOnlyEquatable(), DiagnosticsStrategy, Configuration, HasReportedOneMissingTag);

    private void ResolveDependencies(Dictionary<string, DocumentationFileBuilder> documentationFiles)
    {
        foreach (var dependency in Dependencies)
        {
            if (documentationFiles.TryGetValue(dependency, out DocumentationFileBuilder file))
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
        foreach (DocumentationFileBuilder dependency in ResolvedDependencies)
        {
            foreach (var tag in dependency.Content)
            {
                if (Content.ContainsKey(tag.Key) is false)
                {
                    Content[tag.Key] = tag.Value;
                }
            }
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
        if (Content.TryGetValue(tag, out string text))
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
        if (DiagnosticsStrategy.GenerateDiagnostics is false || Configuration.LimitOneErrorPerDocumentationFile && HasReportedOneUnresolvedDependency) 
        {
            return null;
        }

        MatchCollection matches = DocumentationParsing.MatchDependencies(FileText.ToString());

        foreach (Match match in matches)
        {
            if (match.Groups["dependency"].Value == dependency)
            {
                var line = FileText.ToString().Take(match.Index).Count(static (character) => character is '\n');

                if (line is 0)
                {
                    line = FileText.ToString().Take(match.Index).Count(static (character) => character is '\r');
                }

                var textSpan = FileText.Lines[line].Span;
                LinePositionSpan lineSpan = new(new LinePosition(line, 0), new LinePosition(line, textSpan.Length - 1));
                var location = Location.Create(File.Path, new TextSpan(match.Index, match.Length), lineSpan);

                HasReportedOneUnresolvedDependency = true;
                return DiagnosticsStrategy.UnresolvedDocumentationDependency(location, Name, dependency);
            }
        }

        return null;
    }

    private Diagnostic? CreateMissingTagDiagnostics(string tag)
    {
        if (DiagnosticsStrategy.GenerateDiagnostics is false || Configuration.LimitOneErrorPerDocumentationFile && HasReportedOneMissingTag)
        {
            return null;
        }

        HasReportedOneMissingTag = true;

        if (FileText.Lines.Count is 0)
        {
            return DiagnosticsStrategy.DocumentationFileMissingRequestedTag(Location.None, Name, tag);
        }

        var textSpan = FileText.Lines[0].Span;
        LinePositionSpan lineSpan = new(new LinePosition(0, 0), new LinePosition(0, textSpan.Length - 1));
        var location = Location.Create(File.Path, textSpan, lineSpan);

        return DiagnosticsStrategy.DocumentationFileMissingRequestedTag(location, Name, tag);
    }
}
