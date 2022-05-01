namespace Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Utility;

using System.Collections.Generic;
using System.Threading;

internal static partial class Extensions
{
    public static void ReportDiagnostics(this IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Diagnostic> diagnostics)
    {
        context.RegisterSourceOutput(diagnostics, report);

        static void report(SourceProductionContext context, Diagnostic diagnostics)
        {
            context.ReportDiagnostic(diagnostics);
        }
    }

    public static void ReportDiagnostics(this IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<IEnumerable<Diagnostic>> diagnostics)
    {
        context.RegisterSourceOutput(diagnostics, report);

        static void report(SourceProductionContext context, IEnumerable<Diagnostic> diagnostics)
        {
            foreach (Diagnostic diagnostic in diagnostics)
            {
                context.ReportDiagnostic(diagnostic);
            }
        }
    }

    public static void ReportDiagnostics<T>(this IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<ResultWithDiagnostics<T>> diagnostics)
    {
        context.ReportDiagnostics(diagnostics.Select(extractDiagnostics));

        static IEnumerable<Diagnostic> extractDiagnostics(ResultWithDiagnostics<T> result, CancellationToken _) => result.Diagnostics;
    }
}
