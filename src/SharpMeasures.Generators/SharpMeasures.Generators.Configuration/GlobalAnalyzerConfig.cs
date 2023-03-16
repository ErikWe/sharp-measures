namespace SharpMeasures.Generators.Configuration;

public readonly record struct GlobalAnalyzerConfig
{
    public static GlobalAnalyzerConfig Default { get; } = new GlobalAnalyzerConfig() with
    {
        DocumentationFileExtension = ".doc.txt",
        PrintDocumentationTags = false,
        GenerateDocumentation = true,
        LimitOneErrorPerDocumentationFile = true,
        GeneratedFileHeaderContent = GeneratedFileHeaderContent.All,
        AllowAttributeAliases = false
    };

    public string DocumentationFileExtension { get; init; }
    public bool PrintDocumentationTags { get; init; }
    public bool GenerateDocumentation { get; init; }
    public bool LimitOneErrorPerDocumentationFile { get; init; }

    public GeneratedFileHeaderContent GeneratedFileHeaderContent { get; init; }

    public bool AllowAttributeAliases { get; init; }
}
