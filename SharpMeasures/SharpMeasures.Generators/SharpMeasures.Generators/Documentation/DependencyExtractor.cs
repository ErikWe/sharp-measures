namespace SharpMeasures.Generators.Documentation;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

internal class DependencyExtractor
{
    public static DependencyExtractor Run(string root, IEnumerable<AdditionalText> availableFiles)
        => Run(root, availableFiles, CancellationToken.None);

    public static DependencyExtractor Run(string root, IEnumerable<AdditionalText> availableFiles, CancellationToken token)
    {
        DependencyExtractor extractor = new(root, availableFiles);
        extractor.Run(token);
        return extractor;
    }

    public ReadOnlyCollection<DocumentationFile> Dependencies => DependencyList.AsReadOnly();
    public ReadOnlyCollection<string> UnresolvedDependencies => UnresolvedDependencyList.AsReadOnly();

    private List<DocumentationFile> DependencyList { get; }
    private List<string> UnresolvedDependencyList { get; }

    private Dictionary<string, AdditionalText> AvailableFiles { get; }

    private DependencyExtractor(string root, IEnumerable<AdditionalText> availableFiles)
    {
        UnresolvedDependencyList = new List<string> { root };
        DependencyList = new List<DocumentationFile>();

        AvailableFiles = availableFiles.ToDictionary(static (additionalFile) => Path.GetFileNameWithoutExtension(additionalFile.Path));
    }

    private void Run(CancellationToken token)
    {
        while (UnresolvedDependencyList.Count > 0 && !token.IsCancellationRequested)
        {
            int initialLength = UnresolvedDependencyList.Count;

            foreach (string dependency in UnresolvedDependencyList)
            {
                AttemptResolveDependency(dependency, token);

                if (token.IsCancellationRequested)
                {
                    return;
                }
            }

            if (UnresolvedDependencyList.Count == initialLength)
            {
                break;
            }
        }
    }

    private void AttemptResolveDependency(string dependency, CancellationToken token)
    {
        if (AvailableFiles.ContainsKey(dependency) && AvailableFiles[dependency].GetText(token)?.ToString() is string text)
        {
            DependencyList.Add(new DocumentationFile(dependency, text));
            UnresolvedDependencyList.Remove(text);
            AddDependencies(text, token);
        }
    }

    private void AddDependencies(string text, CancellationToken token)
    {
        foreach (string dependency in GetDependencies(text))
        {
            if (!isDependencyListed(dependency))
            {
                UnresolvedDependencyList.Add(dependency);
            }

            if (token.IsCancellationRequested)
            {
                return;
            }
        }

        bool isDependencyListed(string dependency) => isDependencyResolved(dependency) || isDependencyUnresolved(dependency);

        bool isDependencyResolved(string dependency)
        {
            foreach (DocumentationFile resolvedDependency in DependencyList)
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
            foreach (string unresolvedDependency in UnresolvedDependencyList)
            {
                if (dependency == unresolvedDependency)
                {
                    return true;
                }
            }

            return false;
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
