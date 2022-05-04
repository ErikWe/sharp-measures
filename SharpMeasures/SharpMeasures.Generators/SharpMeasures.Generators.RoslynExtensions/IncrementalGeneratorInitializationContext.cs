namespace Microsoft.CodeAnalysis;

using System.Collections.Generic;

public static partial class Extensions
{
    public static void ReportDiagnostics(this IncrementalGeneratorInitializationContext context, IncrementalValueProvider<Diagnostic> diagnostics)
    {
        context.RegisterSourceOutput(diagnostics, ReportDiagnostics);
    }

    public static void ReportDiagnostics(this IncrementalGeneratorInitializationContext context, IncrementalValueProvider<IEnumerable<Diagnostic>> diagnostics)
    {
        context.RegisterSourceOutput(diagnostics, ReportDiagnostics);
    }

    public static void ReportDiagnostics(this IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Diagnostic> diagnostics)
    {
        context.RegisterSourceOutput(diagnostics, ReportDiagnostics);
    }

    public static void ReportDiagnostics(this IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<IEnumerable<Diagnostic>> diagnostics)
    {
        context.RegisterSourceOutput(diagnostics, ReportDiagnostics);
    }

    private static void ReportDiagnostics(SourceProductionContext context, Diagnostic diagnostics)
    {
        context.ReportDiagnostic(diagnostics);
    }

    private static void ReportDiagnostics(SourceProductionContext context, IEnumerable<Diagnostic> diagnostics)
    {
        foreach (Diagnostic diagnostic in diagnostics)
        {
            context.ReportDiagnostic(diagnostic);
        }
    }
}
