namespace SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using System.Globalization;

public static partial class DiagnosticConstruction
{
    public static Diagnostic InvalidVectorDimension(Location? location, int dimension)
    {
        return Diagnostic.Create(DiagnosticRules.InvalidVectorDimension, location, dimension.ToString(CultureInfo.InvariantCulture));
    }

    public static Diagnostic InvalidInterpretedVectorDimension(Location? location, string vectorType, int interpretedDimension)
    {
        return Diagnostic.Create(DiagnosticRules.InvalidInterpretedVectorDimension, location, vectorType, interpretedDimension.ToString(CultureInfo.InvariantCulture));
    }

    public static Diagnostic MissingVectorDimension(Location? location, string vectorType)
    {
        return Diagnostic.Create(DiagnosticRules.MissingVectorDimension, location, vectorType);
    }

    public static Diagnostic VectorUnexpectedDimension(Location? location, string vectorType, int expectedDimension, int actualDimension)
    {
        return Diagnostic.Create(DiagnosticRules.VectorUnexpectedDimension, location, expectedDimension.ToString(CultureInfo.InvariantCulture), vectorType, actualDimension.ToString(CultureInfo.InvariantCulture));
    }

    public static Diagnostic DuplicateVectorDimension(Location? location, string vectorGroupType, int dimension)
    {
        return Diagnostic.Create(DiagnosticRules.DuplicateVectorDimension, location, vectorGroupType, dimension.ToString(CultureInfo.InvariantCulture));
    }

    public static Diagnostic VectorGroupsLacksMemberOfDimension(Location? location, string vectorGroupType, int expectedDimension)
    {
        return Diagnostic.Create(DiagnosticRules.VectorGroupLacksMemberOfDimension, location, vectorGroupType, expectedDimension.ToString(CultureInfo.InvariantCulture));
    }

    public static Diagnostic VectorNameAndDimensionConflict(Location? location, string vectorType, int interpretedDimension, int specifiedDimension)
    {
        return Diagnostic.Create(DiagnosticRules.VectorNameAndDimensionConflict, location, vectorType, interpretedDimension.ToString(CultureInfo.InvariantCulture), specifiedDimension.ToString(CultureInfo.InvariantCulture));
    }

    public static Diagnostic VectorGroupNameSuggestsDimension(Location? location, string vectorGroupType, int interpretedDimension)
    {
        return Diagnostic.Create(DiagnosticRules.VectorGroupNameSuggestsDimension, location, vectorGroupType, interpretedDimension.ToString(CultureInfo.InvariantCulture));
    }

    public static Diagnostic NonOverlappingVectorDimensions(Location? location, string firstVectorType, string secondVectorType)
    {
        return Diagnostic.Create(DiagnosticRules.NonOverlappingVectorDimensions, location, firstVectorType, secondVectorType);
    }

    public static Diagnostic VectorConstantInvalidDimension(Location? location, int expectedDimension, int constantDimension, string vectorName)
    {
        return Diagnostic.Create(DiagnosticRules.VectorConstantInvalidDimension, location, expectedDimension.ToString(CultureInfo.InvariantCulture), constantDimension, vectorName);
    }

    public static Diagnostic VectorNotSupportingCrossMultiplication(Location? location, string vectorType)
    {
        return Diagnostic.Create(DiagnosticRules.VectorNotSupportingCrossMultiplication, location, vectorType);
    }
}
