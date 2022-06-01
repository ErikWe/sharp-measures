namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.Vectors;
using SharpMeasures.Generators.Diagnostics;

internal class ResizedVectorDiagnostics : IResizedVectorDiagnostics
{
    public static ResizedVectorDiagnostics Instance { get; } = new();

    private ResizedVectorDiagnostics() { }

    public Diagnostic NullAssociatedVector(IProcessingContext context, RawResizedVectorDefinition definition)
    {
        return DiagnosticConstruction.TypeNotVector_Null(definition.Locations.AssociatedVector?.AsRoslynLocation());
    }

    public Diagnostic InvalidDimension(IProcessingContext context, RawResizedVectorDefinition definition)
    {
        return DiagnosticConstruction.InvalidVectorDimension(definition.Locations.Dimension?.AsRoslynLocation(), definition.Dimension);
    }

    public Diagnostic MissingDimension(IProcessingContext context, RawResizedVectorDefinition definition)
    {
        return DiagnosticConstruction.MissingVectorDimension(definition.Locations.Attribute.AsRoslynLocation(), context.Type.Name);
    }
}
