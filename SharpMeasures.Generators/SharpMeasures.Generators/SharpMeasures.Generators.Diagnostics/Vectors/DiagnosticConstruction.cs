namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using System.Globalization;

public static partial class DiagnosticConstruction
{
    public static Diagnostic InvalidVectorDimension(Location? location, int dimension)
    {
        return Diagnostic.Create(DiagnosticRules.InvalidVectorDimension, location, dimension.ToString(CultureInfo.InvariantCulture));
    }

    public static Diagnostic InvalidInterpretedVectorDimension(Location? location, string vectorName, int interpretedDimension)
    {
        return Diagnostic.Create(DiagnosticRules.InvalidInterpretedVectorDimension, location, vectorName, interpretedDimension.ToString(CultureInfo.InvariantCulture));
    }

    public static Diagnostic MissingVectorDimension(Location? location, string vectorName)
    {
        return Diagnostic.Create(DiagnosticRules.MissingVectorDimension, location, vectorName);
    }

    public static Diagnostic DuplicateVectorDimension(Location? location, int dimension)
    {
        return Diagnostic.Create(DiagnosticRules.DuplicateVectorDimension, location, dimension.ToString(CultureInfo.InvariantCulture));
    }

    public static Diagnostic VectorConstantInvalidDimension(Location? location, int expectedDimension, int constantDimension, string vectorName)
    {
        return Diagnostic.Create(DiagnosticRules.VectorConstantInvalidDimension, location, expectedDimension.ToString(CultureInfo.InvariantCulture),
            constantDimension, vectorName);
    }

    public static Diagnostic VectorGroupAlreadySpecified(Location? location, string vectorName)
    {
        return Diagnostic.Create(DiagnosticRules.VectorGroupAlreadySpecified, location, vectorName);
    }
}
