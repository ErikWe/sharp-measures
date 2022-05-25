namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;

public static partial class RoslynUtilityExtensions
{
    public static IncrementalValueProvider<T> ExtractResult<T>(this IncrementalValueProvider<IResultWithDiagnostics<T>> provider)
        => provider.Select(static (result, _) => result.Result);

    public static IncrementalValueProvider<T> ReportDiagnostics<T>(this IncrementalValueProvider<IResultWithDiagnostics<T>> provider,
        IncrementalGeneratorInitializationContext context)
    {
        context.ReportDiagnostics(provider);

        return provider.ExtractResult();
    }
}
