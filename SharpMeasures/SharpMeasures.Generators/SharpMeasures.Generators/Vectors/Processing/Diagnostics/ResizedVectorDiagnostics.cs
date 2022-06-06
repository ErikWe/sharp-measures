namespace SharpMeasures.Generators.Vectors.Processing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Vectors;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Vectors;

internal static class ResizedVectorDiagnostics
{
    public static Diagnostic TypeAlreadyUnit(MinimalLocation? location, NamedType type)
    {
        return GeneratedVectorDiagnostics.TypeAlreadyUnit(location, type);
    }

    public static Diagnostic TypeAlreadyScalar(MinimalLocation? location, NamedType type)
    {
        return GeneratedVectorDiagnostics.TypeAlreadyScalar(location, type);
    }

    public static Diagnostic TypeNotVector(ResizedVector definition)
    {
        return DiagnosticConstruction.TypeNotVector(definition.Locations.AssociatedVector?.AsRoslynLocation(), definition.AssociatedVector.Name);
    }

    public static Diagnostic DuplicateDimension(ResizedVector definition)
    {
        return DiagnosticConstruction.DuplicateVectorDimension(definition.Locations.AssociatedVector?.AsRoslynLocation(), definition.Dimension);
    }

    public static Diagnostic UnresolvedVectorGroup(ResizedVector definition)
    {
        return DiagnosticConstruction.QuantityGroupMissingRoot<GeneratedVectorAttribute>(definition.Locations.AssociatedVector?.AsRoslynLocation());
    }
}
