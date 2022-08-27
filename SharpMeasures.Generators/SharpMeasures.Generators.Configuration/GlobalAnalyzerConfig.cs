namespace SharpMeasures.Generators.Configuration;

public readonly record struct GlobalAnalyzerConfig(bool GenerateDocumentationByDefault, bool LimitOneErrorPerDocumentationFile)
{
    public static GlobalAnalyzerConfig Default => new(true, true);
}
