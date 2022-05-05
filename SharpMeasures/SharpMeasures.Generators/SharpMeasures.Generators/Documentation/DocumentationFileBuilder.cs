namespace SharpMeasures.Generators.Documentation;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.Diagnostics.Documentation;
using SharpMeasures.Generators.Utility;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
            builder.ResolveTags();
        }

        foreach (DocumentationFileBuilder builder in builders.Values)
        {
            builder.AddTagsFromDependencies();
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
    private Dictionary<string, string> Content { get; set; }
    private HashSet<string> ResolvedTags { get; } = new();

    private List<DocumentationFileBuilder> ResolvedDependencies { get; } = new();
    private IReadOnlyCollection<string> Dependencies { get; }

    private List<Diagnostic> Diagnostics { get; } = new();

    private DocumentationFileBuilder(AdditionalText file, string text)
    {
        File = file;
        Name = ReadName(file);
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

    private void ResolveTag(string tag)
    {
        ResolvedTags.Add(tag); // This should be done before actually resolving, otherwise recursive tags loop indefinitely
        Content[tag] = ResolveText(Content[tag]);
    }

    private string ResolveText(string text)
    {
        string originalText = text;

        MatchCollection matches = DocumentationParsing.MatchInvokations(text);

        foreach (Match match in matches)
        {
            string tag = match.Groups["tag"].Value;

            if (ReadTag(tag) is string tagText)
            {
                text = DocumentationParsing.ResolveInvokation(tag, text, tagText);
            }
            else
            {
                CreateMissingTagDiagnostics(tag);
            }
        }

        if (text == originalText)
        {
            return text;
        }
        else
        {
            return ResolveText(text);
        }
    }

    private string? ReadTag(string tag)
        => ReadTag(tag, new HashSet<string>());

    private string? ReadTag(string tag, HashSet<string> searchedFiles)
    {
        if (searchedFiles.Contains(Name))
        {
            return null;
        }

        if (Content.TryGetValue(tag, out string text))
        {
            if (ResolvedTags.Contains(tag))
            {
                return text;
            }

            ResolveTag(tag);
            return Content[tag];
        }

        searchedFiles.Add(Name);

        return ReadTagInDependencies(tag, searchedFiles);
    }

    private string? ReadTagInDependencies(string tag, HashSet<string> searchedFiles)
    {
        if (Dependencies.Count is not 0)
        {
            throw new NotSupportedException("Documentation dependencies has not yet been resolved.");
        }

        foreach (DocumentationFileBuilder dependency in ResolvedDependencies)
        {
            if (dependency.ReadTag(tag, searchedFiles) is string text)
            {
                return text;
            }
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

    private static string ReadName(AdditionalText file)
    {
        string fileName = Path.GetFileName(file.Path);

        if (fileName.Split('.') is string[] { Length: > 0 } components)
        {
            return components[0];
        }

        return fileName;
    }
}