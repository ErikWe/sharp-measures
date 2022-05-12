namespace SharpMeasures.Generators.Documentation;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.Diagnostics.Documentation;
using SharpMeasures.Generators.Utility;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;

internal class DocumentationFileBuilder
{
    public static ResultWithDiagnostics<DocumentationDictionary> Build(IEnumerable<AdditionalText> relevantFiles, bool produceDiagnostics = true)
    {
        if (relevantFiles is null)
        {
            throw new ArgumentNullException(nameof(relevantFiles));
        }

        Dictionary<string, DocumentationFileBuilder> builders = createBuilders().ToDictionary(static (builder) => builder.Name);

        foreach (DocumentationFileBuilder builder in builders.Values)
        {
            if (!builder.IsUtility)
            {
                builder.ResolveDependencies(builders);
            }
        }

        foreach (DocumentationFileBuilder builder in builders.Values)
        {
            if (!builder.IsUtility)
            {
                builder.AddTagsFromDependencies();
            }
        }

        foreach (DocumentationFileBuilder builder in builders.Values)
        {
            if (!builder.IsUtility)
            {
                builder.ResolveAllTagContents();
            }
        }

        foreach (DocumentationFileBuilder builder in builders.Values)
        {
            if (!builder.IsUtility)
            {
                builder.CommentAllTags();
            }
        }

        DocumentationDictionary dictionary = new(builders.Values.ToDictionary(static (file) => file.Name, static (file) => file.Finalize()));
        IEnumerable<Diagnostic> diagnostics = builders.Values.SelectMany(static (file) => file.Diagnostics);

        return new ResultWithDiagnostics<DocumentationDictionary>(dictionary, diagnostics);

        IEnumerable<DocumentationFileBuilder> createBuilders()
        {
            foreach (AdditionalText additionalText in relevantFiles)
            {
                if (additionalText.GetText() is SourceText text)
                {
                    yield return new DocumentationFileBuilder(additionalText, text.ToString(), produceDiagnostics);
                }
            }
        }
    }

    private AdditionalText File { get; }

    public string Name { get; }
    private bool IsUtility { get; }
    private Dictionary<string, string> Content { get; set; }
    private HashSet<string> ResolvedTags { get; } = new();

    private List<DocumentationFileBuilder> ResolvedDependencies { get; } = new();
    private IReadOnlyCollection<string> Dependencies { get; }

    private bool ProduceDiagnostics { get; }
    private List<Diagnostic> Diagnostics { get; } = new();

    private DocumentationFileBuilder(AdditionalText file, string text, bool produceDiagnostics)
    {
        File = file;
        Name = DocumentationParsing.ReadName(file);
        IsUtility = DocumentationParsing.ReadUtilityState(text);
        Dependencies = DocumentationParsing.GetDependencies(text);
        Content = DocumentationParsing.GetParsedTagDefinitions(text);

        ProduceDiagnostics = produceDiagnostics;
    }

    public DocumentationFile Finalize()
    {
        return new(File, Name, new ReadOnlyDictionary<string, string>(Content));
    }

    private void ResolveDependencies(Dictionary<string, DocumentationFileBuilder> documentationFiles)
    {
        foreach (string dependency in Dependencies)
        {
            if (documentationFiles.TryGetValue(dependency, out DocumentationFileBuilder file))
            {
                ResolvedDependencies.Add(file);
            }
            else
            {
                CreateUnresolvedDependencyDiagnostics(dependency);
            }
        }
    }

    private void AddTagsFromDependencies()
    {
        foreach (DocumentationFileBuilder dependency in ResolvedDependencies)
        {
            foreach (KeyValuePair<string, string> tag in dependency.Content)
            {
                if (!Content.ContainsKey(tag.Key))
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
            if (!ResolvedTags.Contains(tag))
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
                CreateMissingTagDiagnostics(tag);
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

    private void CreateUnresolvedDependencyDiagnostics(string dependency)
    {
        if (!ProduceDiagnostics || File.GetText() is not SourceText sourceText)
        {
            return;
        }

        MatchCollection matches = DocumentationParsing.MatchDependencies(sourceText.ToString());

        foreach (Match match in matches)
        {
            if (match.Groups["dependency"].Value == dependency)
            {
                int line = sourceText.ToString().Take(match.Index).Count(static (character) => character is '\n');

                LinePositionSpan span = new(new LinePosition(line, 0), new LinePosition(line, sourceText.Lines[line].Span.Length));
                Location location = Location.Create(File.Path, new TextSpan(match.Index, match.Length), span);

                Diagnostics.Add(UnresolvedDocumentationDependencyDiagnostics.Create(File, location, dependency));

                break;
            }
        }
    }

    private void CreateMissingTagDiagnostics(string tag)
    {
        Diagnostics.Add(DocumentationFileMissingRequestedTagDiagnostics.Create(File, tag));
    }
}