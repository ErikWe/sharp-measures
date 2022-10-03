namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Validation;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

internal sealed class SpecializedSharpMeasuresVectorGroupValidationDiagnostics : ISpecializedSharpMeasuresVectorGroupValidationDiagnostics
{
    public static SpecializedSharpMeasuresVectorGroupValidationDiagnostics Instance { get; } = new();

    private SpecializedSharpMeasuresVectorGroupValidationDiagnostics() { }

    public Diagnostic TypeAlreadyUnit(ISpecializedSharpMeasuresVectorGroupValidationContext context, SpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.VectorGroupTypeAlreadyDefinedAsUnit(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeAlreadyScalar(ISpecializedSharpMeasuresVectorGroupValidationContext context, SpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.VectorGroupTypeAlreadyDefinedAsScalar(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeAlreadyVector(ISpecializedSharpMeasuresVectorGroupValidationContext context, SpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.VectorGroupTypeAlreadyDefinedAsVector(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeAlreadyVectorGroupBase(ISpecializedSharpMeasuresVectorGroupValidationContext context, SpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.VectorGroupTypeAlreadyDefinedAsVectorGroup(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic OriginalNotVectorGroup(ISpecializedSharpMeasuresVectorGroupValidationContext context, SpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.TypeNotVectorGroup(definition.Locations.OriginalQuantity?.AsRoslynLocation(), definition.OriginalQuantity.Name);
    }

    public Diagnostic RootVectorGroupNotResolved(ISpecializedSharpMeasuresVectorGroupValidationContext context, SpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.QuantityGroupMissingRoot<VectorGroupAttribute>(definition.Locations.AttributeName.AsRoslynLocation());
    }

    public Diagnostic TypeNotScalar(ISpecializedSharpMeasuresVectorGroupValidationContext context, SpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Scalar?.AsRoslynLocation(), definition.Scalar!.Value.Name);
    }

    public Diagnostic DifferenceNotVectorGroup(ISpecializedSharpMeasuresVectorGroupValidationContext context, SpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.TypeNotVectorGroup(definition.Locations.Difference?.AsRoslynLocation(), definition.Difference!.Value.Name);
    }

    public Diagnostic UnrecognizedDefaultUnitInstance(IProcessingContext context, IDefaultUnitInstanceDefinition definition, IUnitType unit)
    {
        return DefaultUnitInstanceValidationDiagnostics.Instance.UnrecognizedDefaultUnitInstance(context, definition, unit);
    }
}
