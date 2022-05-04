namespace Microsoft.CodeAnalysis;

using System;
using System.Collections.Generic;

public static partial class Extensions
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
}
