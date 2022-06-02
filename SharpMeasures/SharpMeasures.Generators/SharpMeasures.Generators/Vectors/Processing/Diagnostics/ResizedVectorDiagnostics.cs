namespace SharpMeasures.Generators.Vectors.Processing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Vectors;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Vectors;

internal static class ResizedVectorDiagnostics
{
    public static Diagnostic TypeNotVector(ResizedVectorDefinition definition)
    {
        return DiagnosticConstruction.TypeNotVector(definition.Locations.AssociatedVector?.AsRoslynLocation(), definition.AssociatedVector.Name);
    }

    public static Diagnostic DuplicateDimension(ResizedVectorDefinition definition)
    {
        return DiagnosticConstruction.DuplicateVectorDimension(definition.Locations.AssociatedVector?.AsRoslynLocation(), definition.Dimension);
    }

    public static Diagnostic UnresolvedVectorGroup(ResizedVectorDefinition definition)
    {
        return DiagnosticConstruction.QuantityGroupMissingRoot<GeneratedVectorAttribute>(definition.Locations.AssociatedVector?.AsRoslynLocation());
    }
}
