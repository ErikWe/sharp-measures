namespace Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Utility;

internal static partial class Extensions
{
    public static void ReportDiagnostics<T>(this SourceProductionContext context, ResultWithDiagnostics<T> result)
    {
        context.ReportDiagnostics(result.Diagnostics);
    }
}
