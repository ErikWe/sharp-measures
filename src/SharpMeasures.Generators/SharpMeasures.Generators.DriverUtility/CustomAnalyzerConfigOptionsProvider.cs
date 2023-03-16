namespace SharpMeasures.Generators.DriverUtility;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

public sealed class CustomAnalyzerConfigOptionsProvider : AnalyzerConfigOptionsProvider
{
    public static CustomAnalyzerConfigOptionsProvider Empty { get; } = new CustomAnalyzerConfigOptionsProvider(CustomAnalyzerConfigOptions.Empty);

    public CustomAnalyzerConfigOptionsProvider(AnalyzerConfigOptions globalOptions)
    {
        GlobalOptions = globalOptions;
    }

    public override AnalyzerConfigOptions GlobalOptions { get; }

    public override AnalyzerConfigOptions GetOptions(SyntaxTree tree) => CustomAnalyzerConfigOptions.Empty;

    public override AnalyzerConfigOptions GetOptions(AdditionalText textFile) => CustomAnalyzerConfigOptions.Empty;
}
