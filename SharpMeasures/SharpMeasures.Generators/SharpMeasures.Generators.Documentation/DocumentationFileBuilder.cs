namespace SharpMeasures.Generators.Documentation;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Diagnostics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

internal class DocumentationFileBuilder
{
    public static IResultWithDiagnostics<DocumentationDictionary> Build(IEnumerable<AdditionalText> relevantFiles, IDiagnosticsStrategy diagnosticsStrategy)
    {
        if (relevantFiles is null)
        {
            throw new ArgumentNullException(nameof(relevantFiles));
        }

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

        DocumentationDictionary dictionary = new(builders.Values.ToDictionary(static (file) => file.Name, (file) => file.Finalize(diagnosticsStrategy)));
        IEnumerable<Diagnostic> diagnostics = builders.Values.SelectMany(static (file) => file.Diagnostics);

        return ResultWithDiagnostics.Construct(dictionary, diagnostics);

        IEnumerable<DocumentationFileBuilder> createBuilders()
        {
            foreach (AdditionalText additionalText in relevantFiles)
            {
                if (additionalText.GetText() is SourceText fileText)
                {
                    yield return new DocumentationFileBuilder(additionalText, fileText, diagnosticsStrategy);
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

    private DocumentationFileBuilder(AdditionalText file, SourceText fileText, IDiagnosticsStrategy diagnosticsStrategy)
    {
        File = file;
        FileText = fileText;

        string text = fileText.ToString();

        Name = DocumentationParsing.ReadName(file);
        IsUtility = DocumentationParsing.ReadUtilityState(text);
        Dependencies = DocumentationParsing.GetDependencies(text);
        Content = DocumentationParsing.GetParsedTagDefinitions(text);

        DiagnosticsStrategy = diagnosticsStrategy;
    }

    public DocumentationFile Finalize(IDiagnosticsStrategy diagnosticsStrategy)
    {
        return new(Name, File, new ReadOnlyEquatableDictionary<string, string>(Content), diagnosticsStrategy);
    }

    private void ResolveDependencies(Dictionary<string, DocumentationFileBuilder> documentationFiles)
    {
        foreach (string dependency in Dependencies)
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
            foreach (KeyValuePair<string, string> tag in dependency.Content)
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

        foreach (string tag in keys)
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

        foreach (string tag in keys)
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
        MatchCollection matches = DocumentationParsing.MatchInvokations(text);

        foreach (Match match in matches)
        {
            string tag = match.Groups["tag"].Value;

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
        if (DiagnosticsStrategy.GenerateDiagnostics is false)
        {
            return null;
        }

        MatchCollection matches = DocumentationParsing.MatchDependencies(FileText.ToString());

        foreach (Match match in matches)
        {
            if (match.Groups["dependency"].Value == dependency)
            {
                int line = FileText.ToString().Take(match.Index).Count(static (character) => character is '\n');

                if (line is 0)
                {
                    line = FileText.ToString().Take(match.Index).Count(static (character) => character is '\r');
                }

                TextSpan textSpan = FileText.Lines[line].Span;
                LinePositionSpan lineSpan = new(new LinePosition(line, 0), new LinePosition(line, textSpan.Length - 1));
                Location location = Location.Create(File.Path, new TextSpan(match.Index, match.Length), lineSpan);

                return DiagnosticsStrategy.UnresolvedDocumentationDependency(location, Name, dependency);
            }
        }

        return null;
    }

    private Diagnostic? CreateMissingTagDiagnostics(string tag)
    {
        if (FileText.Lines.Count is 0)
        {
            return DiagnosticsStrategy.DocumentationFileMissingRequestedTag(Location.None, Name, tag);
        }

        TextSpan textSpan = FileText.Lines[0].Span;
        LinePositionSpan lineSpan = new(new LinePosition(0, 0), new LinePosition(0, textSpan.Length - 1));
        Location location = Location.Create(File.Path, textSpan, lineSpan);

        return DiagnosticsStrategy.DocumentationFileMissingRequestedTag(location, Name, tag);
    }
}
