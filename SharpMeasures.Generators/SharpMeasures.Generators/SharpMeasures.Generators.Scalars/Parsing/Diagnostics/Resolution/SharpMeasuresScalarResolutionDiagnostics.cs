namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Resolution;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnit;
using SharpMeasures.Generators.Raw.Units;
using SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;

internal class SharpMeasuresScalarResolutionDiagnostics : ISharpMeasuresScalarResolutionDiagnostics
{
    public static SharpMeasuresScalarResolutionDiagnostics Instance { get; } = new();

    private SharpMeasuresScalarResolutionDiagnostics() { }

    public Diagnostic TypeAlreadyUnit(ISharpMeasuresScalarResolutionContext context, RawSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.ScalarTypeAlreadyDefinedAsUnit(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeNotUnit(ISharpMeasuresScalarResolutionContext context, RawSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotUnit(definition.Locations.Unit?.AsRoslynLocation(), definition.Unit.Name);
    }

    public Diagnostic UnitNotIncludingBiasTerm(ISharpMeasuresScalarResolutionContext context, RawSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.UnitNotIncludingBiasTerm(definition.Locations.UseUnitBias?.AsRoslynLocation(), definition.Unit.Name, context.Type.Name);
    }

    public Diagnostic TypeNotVector(ISharpMeasuresScalarResolutionContext context, RawSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotVector(definition.Locations.Vector?.AsRoslynLocation(), definition.Vector!.Value.Name);
    }

    public Diagnostic DifferenceNotScalar(ISharpMeasuresScalarResolutionContext context, RawSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Difference?.AsRoslynLocation(), definition.Difference.Name);
    }

    public Diagnostic UnrecognizedDefaultUnit(IProcessingContext context, IRawDefaultUnitDefinition definition, IRawUnitType unit)
    {
        return DiagnosticConstruction.UnrecognizedUnitName(definition.DefaultUnitLocations.DefaultUnitName?.AsRoslynLocation(), definition.DefaultUnitName!, unit.Type.Name);
    }

    public Diagnostic ReciprocalNotScalar(ISharpMeasuresScalarResolutionContext context, RawSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Reciprocal?.AsRoslynLocation(), definition.Reciprocal!.Value.Name);
    }

    public Diagnostic SquareNotScalar(ISharpMeasuresScalarResolutionContext context, RawSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Square?.AsRoslynLocation(), definition.Square!.Value.Name);
    }

    public Diagnostic CubeNotScalar(ISharpMeasuresScalarResolutionContext context, RawSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Cube?.AsRoslynLocation(), definition.Cube!.Value.Name);
    }

    public Diagnostic SquareRootNotScalar(ISharpMeasuresScalarResolutionContext context, RawSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.SquareRoot?.AsRoslynLocation(), definition.SquareRoot!.Value.Name);
    }

    public Diagnostic CubeRootNotScalar(ISharpMeasuresScalarResolutionContext context, RawSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.CubeRoot?.AsRoslynLocation(), definition.CubeRoot!.Value.Name);
    }
}
