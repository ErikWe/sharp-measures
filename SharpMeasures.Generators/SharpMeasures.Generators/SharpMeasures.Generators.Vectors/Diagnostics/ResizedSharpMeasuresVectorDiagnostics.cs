namespace SharpMeasures.Generators.Vectors.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing.ResizedSharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Refinement.ResizedSharpMeasuresVector;

internal class ResizedSharpMeasuresVectorDiagnostics : IResizedSharpMeasuresVectorProcessingDiagnostics, IResizedSharpMeasuresVectorRefinementDiagnostics
{
    public static ResizedSharpMeasuresVectorDiagnostics Instance { get; } = new();

    private ResizedSharpMeasuresVectorDiagnostics() { }

    public Diagnostic NullAssociatedVector(IProcessingContext context, RawResizedSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotVector(definition.Locations.AssociatedVector?.AsRoslynLocation());
    }

    public Diagnostic InvalidDimension(IProcessingContext context, RawResizedSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.InvalidVectorDimension(definition.Locations.Dimension?.AsRoslynLocation(), definition.Dimension);
    }

    public Diagnostic MissingDimension(IProcessingContext context, RawResizedSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.MissingVectorDimension(definition.Locations.Attribute.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeAlreadyUnit(IResizedSharpMeasuresVectorRefinementContext context, ResizedSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.VectorTypeAlreadyDefinedAsUnit(definition.Locations.Attribute.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeAlreadyScalar(IResizedSharpMeasuresVectorRefinementContext context, ResizedSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.VectorTypeAlreadyDefinedAsScalar(definition.Locations.Attribute.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeAlreadyVector(IResizedSharpMeasuresVectorRefinementContext context, ResizedSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.VectorTypeAlreadyDefinedAsVector(definition.Locations.Attribute.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeNotVector(IResizedSharpMeasuresVectorRefinementContext context, ResizedSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.TypeNotVector(definition.Locations.Attribute.AsRoslynLocation(), definition.AssociatedVector.Name);
    }

    public Diagnostic DuplicateDimension(IResizedSharpMeasuresVectorRefinementContext context, ResizedSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.DuplicateVectorDimension(definition.Locations.Dimension?.AsRoslynLocation(), definition.Dimension);
    }

    public Diagnostic UnresolvedVectorGroup(IResizedSharpMeasuresVectorRefinementContext context, ResizedSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.QuantityGroupMissingRoot<SharpMeasuresVectorAttribute>(definition.Locations.AssociatedVector?.AsRoslynLocation());
    }
}
