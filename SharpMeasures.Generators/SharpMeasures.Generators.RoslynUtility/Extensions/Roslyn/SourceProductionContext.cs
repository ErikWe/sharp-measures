namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;

using System.Collections.Generic;

public static partial class RoslynUtilityExtensions
{
    public static void ReportDiagnostics(this SourceProductionContext context, IEnumerable<Diagnostic> diagnostics)
    {
        foreach (var diagnostic in diagnostics)
        {
            context.ReportDiagnostic(diagnostic);
        }
    }

    public static void ReportDiagnostics(this SourceProductionContext context, params Diagnostic[] diagnostics)
    {
        context.ReportDiagnostics(diagnostics as IEnumerable<Diagnostic>);
    }

    public static void ReportDiagnostics<T>(this SourceProductionContext context, T diagnostics) where T : IEnumerable<Diagnostic>
    {
        context.ReportDiagnostics(diagnostics as IEnumerable<Diagnostic>);
    }

    public static void ReportDiagnostics(this SourceProductionContext context, IValidityWithDiagnostics validity)
    {
        context.ReportDiagnostics(validity.Diagnostics);
    }

    public static void ReportDiagnostics(this SourceProductionContext context, IOptionalWithDiagnostics validity)
    {
        context.ReportDiagnostics(validity.Diagnostics);
    }
}
