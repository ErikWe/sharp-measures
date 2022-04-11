namespace SharpMeasures.Generators.Documentation;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

internal static class DependencyExtractor
{
    public static List<DocumentationFile> Extract(string root, IEnumerable<AdditionalText> availableFiles, CancellationToken token)
    {
        List<string> unresolvedDependencies = new() { root };
        List<DocumentationFile> resolvedDependencies = new();
        bool dependenciesModifiedInIteration = false;

        while (unresolvedDependencies.Count > 0 && !token.IsCancellationRequested)
        {
            foreach (AdditionalText file in availableFiles)
            {
                string fileName = Path.GetFileNameWithoutExtension(file.Path);

                foreach (string requiredFile in unresolvedDependencies)
                {
                    if (requiredFile == fileName && file.GetText(token)?.ToString() is string text)
                    {
                        resolvedDependencies.Add(new DocumentationFile(fileName, text));
                        unresolvedDependencies.Remove(text);
                        AddDependenciesToListOfUnresolvedDependencies(text, resolvedDependencies, unresolvedDependencies, token);
                        dependenciesModifiedInIteration = true;
                        break;
                    }
                }

                if (token.IsCancellationRequested)
                {
                    return resolvedDependencies;
                }
            }

            if (!dependenciesModifiedInIteration)
            {
                break;
            }
        }

        return resolvedDependencies;
    }

    private static void AddDependenciesToListOfUnresolvedDependencies(string text, IEnumerable<DocumentationFile> resolvedDependencies,
        List<string> unresolvedDependencies, CancellationToken token)
    {
        bool isDependencyListed(string dependency) => isDependencyResolved(dependency) || isDependencyUnresolved(dependency);

        bool isDependencyResolved(string dependency)
        {
            foreach (DocumentationFile resolvedDependency in resolvedDependencies)
            {
                if (dependency == resolvedDependency.Name)
                {
                    return true;
                }
            }

            return false;
        }

        bool isDependencyUnresolved(string dependency)
        {
            foreach (string unresolvedDependency in unresolvedDependencies)
            {
                if (dependency == unresolvedDependency)
                {
                    return true;
                }
            }

            return false;
        }

        foreach (string dependency in GetDependencies(text))
        {
            if (!isDependencyListed(dependency))
            {
                unresolvedDependencies.Add(dependency);
            }

            if (token.IsCancellationRequested)
            {
                return;
            }
        }
    }

    private static IEnumerable<string> GetDependencies(string text)
    {
        Regex regex = new(@"#Extends:(?<dependency>[A-Za-z\d_\-.]+?)(?:\r\n|\n|#)\s");
        MatchCollection matches = regex.Matches(text);

        foreach (Match match in matches)
        {
            yield return match.Groups["dependency"].Value;
        }
    }
}
