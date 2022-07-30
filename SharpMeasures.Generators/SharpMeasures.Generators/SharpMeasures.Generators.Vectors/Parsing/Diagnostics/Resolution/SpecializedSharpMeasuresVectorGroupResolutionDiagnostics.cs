namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Resolution;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Unresolved.Units;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

internal class SpecializedSharpMeasuresVectorGroupResolutionDiagnostics : ISpecializedSharpMeasuresVectorGroupResolutionDiagnostics
{
    public static SpecializedSharpMeasuresVectorGroupResolutionDiagnostics Instance { get; } = new();

    private SpecializedSharpMeasuresVectorGroupResolutionDiagnostics() { }

    public Diagnostic TypeAlreadyUnit(ISpecializedSharpMeasuresVectorGroupResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.VectorGroupTypeAlreadyDefinedAsUnit(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeAlreadyScalar(ISpecializedSharpMeasuresVectorGroupResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.VectorGroupTypeAlreadyDefinedAsScalar(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeAlreadyVector(ISpecializedSharpMeasuresVectorGroupResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.VectorGroupTypeAlreadyDefinedAsVectorGroup(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic OriginalNotVectorGroup(ISpecializedSharpMeasuresVectorGroupResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.TypeNotVectorGroup(definition.Locations.OriginalVectorGroup?.AsRoslynLocation(), definition.OriginalVectorGroup.Name);
    }

    public Diagnostic TypeNotScalar(ISpecializedSharpMeasuresVectorGroupResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Scalar?.AsRoslynLocation(), definition.Scalar!.Value.Name);
    }

    public Diagnostic DifferenceNotVectorGroup(ISpecializedSharpMeasuresVectorGroupResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.TypeNotVectorGroup(definition.Locations.Difference?.AsRoslynLocation(), definition.Difference!.Value.Name);
    }

    public Diagnostic UnrecognizedDefaultUnit(ISpecializedSharpMeasuresVectorGroupResolutionContext context, UnresolvedSpecializedSharpMeasuresVectorGroupDefinition definition,
        IUnresolvedUnitType unit)
    {
        return DiagnosticConstruction.UnrecognizedUnitName(definition.Locations.DefaultUnitName?.AsRoslynLocation(), definition.DefaultUnitName!, unit.Type.Name);
    }
}
