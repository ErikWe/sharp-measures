namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;

public static partial class RoslynUtilityExtensions
{
    public static void ReportDiagnostics(this IncrementalGeneratorInitializationContext context, IncrementalValueProvider<Diagnostic> diagnostics) => context.RegisterSourceOutput(diagnostics, ReportDiagnostics);
    public static void ReportDiagnostics(this IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Diagnostic> diagnostics) => context.RegisterSourceOutput(diagnostics, ReportDiagnostics);

    public static void ReportDiagnostics(this IncrementalGeneratorInitializationContext context, IncrementalValueProvider<Optional<Diagnostic>> diagnostics) => context.RegisterSourceOutput(diagnostics, ReportDiagnostics);
    public static void ReportDiagnostics(this IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Optional<Diagnostic>> diagnostics) => context.RegisterSourceOutput(diagnostics, ReportDiagnostics);

    public static void ReportDiagnostics<T>(this IncrementalGeneratorInitializationContext context, IncrementalValueProvider<T> diagnostics) where T : IEnumerable<Diagnostic> => context.RegisterSourceOutput(diagnostics, ReportDiagnostics);
    public static void ReportDiagnostics<T>(this IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<T> diagnostics) where T : IEnumerable<Diagnostic> => context.RegisterSourceOutput(diagnostics, ReportDiagnostics);

    public static void ReportDiagnostics<T>(this IncrementalGeneratorInitializationContext context, IncrementalValueProvider<Optional<T>> diagnostics) where T : IEnumerable<Diagnostic> => context.RegisterSourceOutput(diagnostics, ReportDiagnostics);
    public static void ReportDiagnostics<T>(this IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Optional<T>> diagnostics) where T : IEnumerable<Diagnostic> => context.RegisterSourceOutput(diagnostics, ReportDiagnostics);

    private static void ReportDiagnostics(SourceProductionContext context, Diagnostic diagnostics) => context.ReportDiagnostic(diagnostics);
    private static void ReportDiagnostics(SourceProductionContext context, Optional<Diagnostic> diagnostics)
    {
        if (diagnostics.HasValue is false)
        {
            return;
        }

        context.ReportDiagnostic(diagnostics.Value);
    }

    private static void ReportDiagnostics<T>(SourceProductionContext context, Optional<T> diagnostics) where T : IEnumerable<Diagnostic>
    {
        if (diagnostics.HasValue is false)
        {
            return;
        }

        context.ReportDiagnostics(diagnostics.Value);
    }
}
