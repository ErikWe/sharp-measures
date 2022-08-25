namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnit;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Validation;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

internal class SharpMeasuresVectorGroupValidationDiagnostics : ISharpMeasuresVectorGroupValidationDiagnostics
{
    public static SharpMeasuresVectorGroupValidationDiagnostics Instance { get; } = new();

    private SharpMeasuresVectorGroupValidationDiagnostics() { }

    public Diagnostic TypeAlreadyUnit(ISharpMeasuresVectorGroupValidationContext context, SharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.VectorGroupTypeAlreadyDefinedAsUnit(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeAlreadyScalar(ISharpMeasuresVectorGroupValidationContext context, SharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.VectorGroupTypeAlreadyDefinedAsScalar(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeAlreadyVector(ISharpMeasuresVectorGroupValidationContext context, SharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.VectorGroupTypeAlreadyDefinedAsVector(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeNotUnit(ISharpMeasuresVectorGroupValidationContext context, SharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.TypeNotUnit(definition.Locations.Unit?.AsRoslynLocation(), definition.Unit.Name);
    }

    public Diagnostic TypeNotScalar(ISharpMeasuresVectorGroupValidationContext context, SharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Scalar?.AsRoslynLocation(), definition.Scalar!.Value.Name);
    }

    public Diagnostic DifferenceNotVectorGroup(ISharpMeasuresVectorGroupValidationContext context, SharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.TypeNotVectorGroup(definition.Locations.Difference?.AsRoslynLocation(), definition.Difference!.Value.Name);
    }

    public Diagnostic UnrecognizedDefaultUnit(IProcessingContext context, IDefaultUnitDefinition definition, IUnitType unit)
    {
        return DefaultUnitValidationDiagnostics.Instance.UnrecognizedDefaultUnit(context, definition, unit);
    }
}
