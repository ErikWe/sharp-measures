namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnit;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Validation;
using SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;
using SharpMeasures.Generators.Units;

internal class SpecializedSharpMeasuresScalarValidationDiagnostics : ISpecializedSharpMeasuresScalarValidationDiagnostics
{
    public static SpecializedSharpMeasuresScalarValidationDiagnostics Instance { get; } = new();

    private SpecializedSharpMeasuresScalarValidationDiagnostics() { }

    public Diagnostic TypeAlreadyUnit(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.ScalarTypeAlreadyDefinedAsUnit(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeAlreadyScalar(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.ScalarTypeAlreadyDefinedAsScalar(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic OriginalNotScalar(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.OriginalScalar?.AsRoslynLocation(), definition.OriginalScalar.Name);
    }

    public Diagnostic RootScalarNotResolved(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.QuantityGroupMissingRoot<SharpMeasuresScalarAttribute>(definition.Locations.AttributeName.AsRoslynLocation());
    }

    public Diagnostic TypeNotVector(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotVector(definition.Locations.Vector?.AsRoslynLocation(), definition.Vector!.Value.Name);
    }

    public Diagnostic DifferenceNotScalar(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Difference?.AsRoslynLocation(), definition.Difference!.Value.Name);
    }

    public Diagnostic UnrecognizedDefaultUnit(IProcessingContext context, IDefaultUnitDefinition definition, IUnitType unit)
    {
        return DefaultUnitValidationDiagnostics.Instance.UnrecognizedDefaultUnit(context, definition, unit);
    }

    public Diagnostic ReciprocalNotScalar(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Reciprocal?.AsRoslynLocation(), definition.Reciprocal!.Value.Name);
    }

    public Diagnostic SquareNotScalar(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Square?.AsRoslynLocation(), definition.Square!.Value.Name);
    }

    public Diagnostic CubeNotScalar(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Cube?.AsRoslynLocation(), definition.Cube!.Value.Name);
    }

    public Diagnostic SquareRootNotScalar(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.SquareRoot?.AsRoslynLocation(), definition.SquareRoot!.Value.Name);
    }

    public Diagnostic CubeRootNotScalar(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.CubeRoot?.AsRoslynLocation(), definition.CubeRoot!.Value.Name);
    }
}
