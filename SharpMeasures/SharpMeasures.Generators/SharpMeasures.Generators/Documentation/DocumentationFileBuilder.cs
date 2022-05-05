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
    public static ResultWithDiagnostics<IReadOnlyDictionary<string, DocumentationFile>> Build(IEnumerable<AdditionalText> relevantFiles)
    {
        if (relevantFiles is null)
        {
            throw new ArgumentNullException(nameof(relevantFiles));
        }

        Dictionary<string, DocumentationFileBuilder> builders = createBuilders().ToDictionary(static (builder) => builder.Name);

        foreach (DocumentationFileBuilder builder in builders.Values)
        {
            builder.ResolveDependencies(builders);
        }

        foreach (DocumentationFileBuilder builder in builders.Values)
        {
            builder.AddTagsFromDependencies();
        }

        foreach (DocumentationFileBuilder builder in builders.Values)
        {
            builder.ResolveTags();
        }

        foreach (DocumentationFileBuilder builder in builders.Values)
        {
            builder.CommentTags();
        }

        IReadOnlyDictionary<string, DocumentationFile> documentationFiles = builders.Values.Select(finalizeBuilders).ToDictionary(static (file) => file.Name);
        IEnumerable<Diagnostic> diagnostics = builders.Values.SelectMany(static (file) => file.Diagnostics);

        return new ResultWithDiagnostics<IReadOnlyDictionary<string, DocumentationFile>>(documentationFiles, diagnostics);

        IEnumerable<DocumentationFileBuilder> createBuilders()
        {
            foreach (AdditionalText additionalText in relevantFiles)
            {
                if (additionalText.GetText() is SourceText text)
                {
                    yield return new DocumentationFileBuilder(additionalText, text.ToString());
                }
            }
        }

        DocumentationFile finalizeBuilders(DocumentationFileBuilder builder)
        {
            return new DocumentationFile(builder.File, builder.Name, new ReadOnlyDictionary<string, string>(builder.Content));
        }
    }

    private AdditionalText File { get; }

    private string Name { get; }
    private bool IsUtility { get; }
    private Dictionary<string, string> Content { get; set; }
    private HashSet<string> ResolvedTags { get; } = new();

    private List<DocumentationFileBuilder> ResolvedDependencies { get; } = new();
    private IReadOnlyCollection<string> Dependencies { get; }

    private List<Diagnostic> Diagnostics { get; } = new();

    private DocumentationFileBuilder(AdditionalText file, string text)
    {
        File = file;
        Name = DocumentationParsing.ReadName(file);
        IsUtility = DocumentationParsing.ReadUtilityState(text);
        Dependencies = DocumentationParsing.GetDependencies(text);
        Content = DocumentationParsing.GetParsedTagDefinitions(text);
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

    private void ResolveTags()
    {
        foreach (string tag in Content.Keys)
        {
            if (!ResolvedTags.Contains(tag))
            {
                ResolveTag(tag);
            }
        }
    }

    private void CommentTags()
    {
        foreach (KeyValuePair<string, string> tag in Content)
        {
            Content[tag.Key] = DocumentationParsing.CommentText(tag.Value);
        }
    }

    private void ResolveTag(string tag)
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

            if (ReadTag(tag) is string tagText)
            {
                text = DocumentationParsing.ResolveInvokation(tag, text, tagText);
            }
            else if (!IsUtility)
            {
                CreateMissingTagDiagnostics(tag);
            }
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

            ResolveTag(tag);
            return Content[tag];
        }

        return null;
    }

    private void CreateUnresolvedDependencyDiagnostics(string dependency)
    {
        if (File.GetText() is not SourceText sourceText)
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