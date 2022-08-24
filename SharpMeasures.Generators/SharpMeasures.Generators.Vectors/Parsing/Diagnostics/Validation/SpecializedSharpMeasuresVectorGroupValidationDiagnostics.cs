namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnit;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Validation;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

internal class SpecializedSharpMeasuresVectorGroupValidationDiagnostics : ISpecializedSharpMeasuresVectorGroupValidationDiagnostics
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

    public Diagnostic TypeAlreadyVectorGroup(ISpecializedSharpMeasuresVectorGroupValidationContext context, SpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.VectorGroupTypeAlreadyDefinedAsVectorGroup(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic OriginalNotVectorGroup(ISpecializedSharpMeasuresVectorGroupValidationContext context, SpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.TypeNotVectorGroup(definition.Locations.OriginalVectorGroup?.AsRoslynLocation(), definition.OriginalVectorGroup.Name);
    }

    public Diagnostic RootVectorGroupNotResolved(ISpecializedSharpMeasuresVectorGroupValidationContext context, SpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.QuantityGroupMissingRoot<SharpMeasuresVectorGroupAttribute>(definition.Locations.AttributeName.AsRoslynLocation());
    }

    public Diagnostic TypeNotScalar(ISpecializedSharpMeasuresVectorGroupValidationContext context, SpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Scalar?.AsRoslynLocation(), definition.Scalar!.Value.Name);
    }

    public Diagnostic DifferenceNotVectorGroup(ISpecializedSharpMeasuresVectorGroupValidationContext context, SpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.TypeNotVectorGroup(definition.Locations.Difference?.AsRoslynLocation(), definition.Difference!.Value.Name);
    }

    public Diagnostic UnrecognizedDefaultUnit(IProcessingContext context, IDefaultUnitDefinition definition, IUnitType unit)
    {
        return DefaultUnitValidationDiagnostics.Instance.UnrecognizedDefaultUnit(context, definition, unit);
    }
}
