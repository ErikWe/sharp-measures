namespace Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Utility;

using System.Collections.Generic;
using System.Threading;

internal static partial class Extensions
{
    public static void ReportDiagnostics<T>(this IncrementalGeneratorInitializationContext context, IncrementalValueProvider<ResultWithDiagnostics<T>> diagnostics)
    {
        context.ReportDiagnostics(diagnostics.Select(ExtractDiagnostics));
    }

    public static void ReportDiagnostics<T>(this IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<ResultWithDiagnostics<T>> diagnostics)
    {
        context.ReportDiagnostics(diagnostics.Select(ExtractDiagnostics));
    }

    private static IEnumerable<Diagnostic> ExtractDiagnostics<T>(ResultWithDiagnostics<T> result, CancellationToken _) => result.Diagnostics;
}
