namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;

using System;
using System.Collections.Generic;

public static partial class RoslynUtilityExtensions
{
    public static void ReportDiagnostics(this SourceProductionContext context, IEnumerable<Diagnostic> diagnostics)
    {
        if (diagnostics is null)
        {
            throw new ArgumentNullException(nameof(diagnostics));
        }

        foreach (Diagnostic diagnostic in diagnostics)
        {
            context.ReportDiagnostic(diagnostic);
        }
    }
    
    public static void ReportDiagnostics(this SourceProductionContext context, params Diagnostic[] diagnostics)
    {
        context.ReportDiagnostics(diagnostics as IEnumerable<Diagnostic>);
    }

    public static void ReportDiagnostics<T>(this SourceProductionContext context, T diagnostics)
        where T : IEnumerable<Diagnostic>
    {
        context.ReportDiagnostics(diagnostics as IEnumerable<Diagnostic>);
    }

    public static void ReportDiagnostics(this SourceProductionContext context, IValidityWithDiagnostics validity)
    {
        if (validity is null)
        {
            throw new ArgumentNullException(nameof(validity));
        }

        context.ReportDiagnostics(validity.Diagnostics);
    }

    public static void ReportDiagnostics(this SourceProductionContext context, IOptionalWithDiagnostics validity)
    {
        if (validity is null)
        {
            throw new ArgumentNullException(nameof(validity));
        }

        context.ReportDiagnostics(validity.Diagnostics);
    }
}
