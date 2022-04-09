namespace SharpMeasures.SourceGenerators.Providers.Documentation;

using Microsoft.CodeAnalysis;

using System.IO;
using System.Linq;

internal static class DocumentationCandidateProvider
{
    public static IncrementalValuesProvider<AdditionalText> Attach(IncrementalValuesProvider<AdditionalText> provider, string directoryName)
        => provider.Where((file) => IsFileInCorrectDirectoryAndCorrectExtension(file, directoryName));

    private static bool IsFileInCorrectDirectoryAndCorrectExtension(AdditionalText file, string directoryName)
        => Path.GetExtension(file.Path) is ".txt"
        && Directory.GetParent(file.Path) is DirectoryInfo directory
        && directory.Name == directoryName
        && directory.Parent.Name is "Documentation";
}
