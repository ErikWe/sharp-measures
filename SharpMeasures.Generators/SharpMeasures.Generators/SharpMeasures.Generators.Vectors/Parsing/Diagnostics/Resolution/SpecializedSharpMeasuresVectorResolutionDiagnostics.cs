namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Resolution;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Unresolved.Units;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;

internal class SpecializedSharpMeasuresVectorResolutionDiagnostics : ISpecializedSharpMeasuresVectorResolutionDiagnostics
{
    public static SpecializedSharpMeasuresVectorResolutionDiagnostics Instance { get; } = new();

    private SpecializedSharpMeasuresVectorResolutionDiagnostics() { }

    public Diagnostic TypeAlreadyUnit(ISpecializedSharpMeasuresVectorResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.VectorTypeAlreadyDefinedAsUnit(definition.Locations.Attribute.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeAlreadyScalar(ISpecializedSharpMeasuresVectorResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.VectorTypeAlreadyDefinedAsScalar(definition.Locations.Attribute.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeAlreadyVector(ISpecializedSharpMeasuresVectorResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.VectorTypeAlreadyDefinedAsVector(definition.Locations.Attribute.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic OriginalNotVector(ISpecializedSharpMeasuresVectorResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.TypeNotVector(definition.Locations.OriginalVector?.AsRoslynLocation(), definition.OriginalVector.Name);
    }

    public Diagnostic TypeNotScalar(ISpecializedSharpMeasuresVectorResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Scalar?.AsRoslynLocation(), definition.Scalar!.Value.Name);
    }

    public Diagnostic DifferenceNotVector(ISpecializedSharpMeasuresVectorResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.TypeNotVector(definition.Locations.Difference?.AsRoslynLocation(), definition.Difference!.Value.Name);
    }

    public Diagnostic DifferenceVectorGroupLacksMatchingDimension(ISpecializedSharpMeasuresVectorResolutionContext context,
        UnresolvedSpecializedSharpMeasuresVectorDefinition definition, int dimension)
    {
        return DiagnosticConstruction.VectorGroupsLacksMemberOfDimension(definition.Locations.Difference?.AsRoslynLocation(), definition.Difference!.Value.Name, dimension);
    }

    public Diagnostic UnrecognizedDefaultUnit(ISpecializedSharpMeasuresVectorResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorDefinition definition,
        IUnresolvedUnitType unit)
    {
        return DiagnosticConstruction.UnrecognizedUnitName(definition.Locations.DefaultUnitName?.AsRoslynLocation(), definition.DefaultUnitName!, unit.Type.Name);
    }
}
