namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Validation;
using SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;
using SharpMeasures.Generators.Units;

internal class SharpMeasuresScalarValidationDiagnostics : ISharpMeasuresScalarValidationDiagnostics
{
    public static SharpMeasuresScalarValidationDiagnostics Instance { get; } = new();

    private SharpMeasuresScalarValidationDiagnostics() { }

    public Diagnostic TypeAlreadyUnit(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.ScalarTypeAlreadyDefinedAsUnit(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeNotUnit(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotUnit(definition.Locations.Unit?.AsRoslynLocation(), definition.Unit.Name);
    }

    public Diagnostic UnitNotIncludingBiasTerm(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.UnitNotIncludingBiasTerm(definition.Locations.UseUnitBias?.AsRoslynLocation(), definition.Unit.Name, context.Type.Name);
    }

    public Diagnostic TypeNotVector(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotVector(definition.Locations.Vector?.AsRoslynLocation(), definition.Vector!.Value.Name);
    }

    public Diagnostic DifferenceNotScalar(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Difference?.AsRoslynLocation(), definition.Difference!.Value.Name);
    }

    public Diagnostic UnrecognizedDefaultUnitInstance(IProcessingContext context, IDefaultUnitInstanceDefinition definition, IUnitType unit)
    {
        return DefaultUnitInstanceValidationDiagnostics.Instance.UnrecognizedDefaultUnitInstance(context, definition, unit);
    }

    public Diagnostic ReciprocalNotScalar(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Reciprocal?.AsRoslynLocation(), definition.Reciprocal!.Value.Name);
    }

    public Diagnostic SquareNotScalar(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Square?.AsRoslynLocation(), definition.Square!.Value.Name);
    }

    public Diagnostic CubeNotScalar(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Cube?.AsRoslynLocation(), definition.Cube!.Value.Name);
    }

    public Diagnostic SquareRootNotScalar(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.SquareRoot?.AsRoslynLocation(), definition.SquareRoot!.Value.Name);
    }

    public Diagnostic CubeRootNotScalar(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.CubeRoot?.AsRoslynLocation(), definition.CubeRoot!.Value.Name);
    }
}
