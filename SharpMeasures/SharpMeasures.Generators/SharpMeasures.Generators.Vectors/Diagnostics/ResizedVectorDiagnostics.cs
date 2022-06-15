namespace SharpMeasures.Generators.Vectors.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing.ResizedVector;
using SharpMeasures.Generators.Vectors.Refinement.ResizedVector;

internal class ResizedVectorDiagnostics : Parsing.ResizedVector.IResizedVectorProcessingDiagnostics, IResizedVectorRefinementDiagnostics
{
    public static ResizedVectorDiagnostics Instance { get; } = new();

    private ResizedVectorDiagnostics() { }

    public Diagnostic NullAssociatedVector(IProcessingContext context, RawResizedVectorDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotVector(definition.Locations.AssociatedVector?.AsRoslynLocation());
    }

    public Diagnostic InvalidDimension(IProcessingContext context, RawResizedVectorDefinition definition)
    {
        return DiagnosticConstruction.InvalidVectorDimension(definition.Locations.Dimension?.AsRoslynLocation(), definition.Dimension);
    }

    public Diagnostic MissingDimension(IProcessingContext context, RawResizedVectorDefinition definition)
    {
        return DiagnosticConstruction.MissingVectorDimension(definition.Locations.Attribute.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeAlreadyUnit(IResizedVectorRefinementContext context, ResizedVectorDefinition definition)
    {
        return DiagnosticConstruction.VectorTypeAlreadyDefinedAsUnit(definition.Locations.Attribute.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeAlreadyScalar(IResizedVectorRefinementContext context, ResizedVectorDefinition definition)
    {
        return DiagnosticConstruction.VectorTypeAlreadyDefinedAsScalar(definition.Locations.Attribute.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeAlreadyVector(IResizedVectorRefinementContext context, ResizedVectorDefinition definition)
    {
        return DiagnosticConstruction.VectorTypeAlreadyDefinedAsVector(definition.Locations.Attribute.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeNotVector(IResizedVectorRefinementContext context, ResizedVectorDefinition definition)
    {
        return DiagnosticConstruction.TypeNotVector(definition.Locations.Attribute.AsRoslynLocation(), definition.AssociatedVector.Name);
    }

    public Diagnostic DuplicateDimension(IResizedVectorRefinementContext context, ResizedVectorDefinition definition)
    {
        return DiagnosticConstruction.DuplicateVectorDimension(definition.Locations.Dimension?.AsRoslynLocation(), definition.Dimension);
    }

    public Diagnostic UnresolvedVectorGroup(IResizedVectorRefinementContext context, ResizedVectorDefinition definition)
    {
        return DiagnosticConstruction.QuantityGroupMissingRoot<GeneratedVectorAttribute>(definition.Locations.AssociatedVector?.AsRoslynLocation());
    }
}
