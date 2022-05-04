namespace SharpMeasures.Generators.Diagnostics;

public static partial class DiagnosticIDs
{
    public const string DocumentationFileDoesNotContainName = $"{Prefix}{Numbering.Thousands.SourceGenerators}{Numbering.Hundreds.Documentation}00";
    public const string UnresolvedDocumentationDependency = $"{Prefix}{Numbering.Thousands.SourceGenerators}{Numbering.Hundreds.Documentation}01";
    public const string NoMatchingDocumentationFile = $"{Prefix}{Numbering.Thousands.SourceGenerators}{Numbering.Hundreds.Documentation}02";
}
