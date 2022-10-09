namespace SharpMeasures.Generators.Configuration;

public readonly record struct GlobalAnalyzerConfig
{
    public static GlobalAnalyzerConfig Default { get; } = new GlobalAnalyzerConfig() with
    {
        DocumentationFileExtension = ".doc.txt",
        GenerateDocumentationByDefault = true,
        LimitOneErrorPerDocumentationFile = true,
        GeneratedFileHeaderContent = GeneratedFileHeaderContent.All
    };

    public string DocumentationFileExtension { get; init; }
    public bool GenerateDocumentationByDefault { get; init; }
    public bool LimitOneErrorPerDocumentationFile { get; init; }

    public GeneratedFileHeaderContent GeneratedFileHeaderContent { get; init; }
}
