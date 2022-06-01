namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using System.Globalization;

internal static partial class DiagnosticConstruction
{
    public static Diagnostic InvalidVectorDimension(Location? location, int dimension)
    {
        return Diagnostic.Create(DiagnosticRules.InvalidVectorDimension, location, dimension.ToString(CultureInfo.InvariantCulture));
    }

    public static Diagnostic MissingVectorDimension(Location? location, string quantityName)
    {
        return Diagnostic.Create(DiagnosticRules.MissingVectorDimension, location, quantityName);
    }
}
