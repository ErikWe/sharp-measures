namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnit;
using SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;

internal class SharpMeasuresScalarProcessingDiagnostics : ISharpMeasuresScalarProcessingDiagnostics
{
    public static SharpMeasuresScalarProcessingDiagnostics Instance { get; } = new();

    private SharpMeasuresScalarProcessingDiagnostics() { }

    public Diagnostic NullUnit(IProcessingContext context, UnprocessedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotUnit(definition.Locations.Unit?.AsRoslynLocation());
    }

    public Diagnostic NullVector(IProcessingContext context, UnprocessedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotVector(definition.Locations.Vector?.AsRoslynLocation());
    }

    public Diagnostic NullDifferenceQuantity(IProcessingContext context, UnprocessedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotScalar(definition.Locations.Difference?.AsRoslynLocation());
    }

    public Diagnostic DifferenceDisabledButQuantitySpecified(IProcessingContext context, UnprocessedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.DifferenceDisabledButQuantitySpecified(definition.Locations.Difference?.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic NullDefaultUnit(IProcessingContext context, IRawDefaultUnitDefinition definition)
    {
        return DiagnosticConstruction.NullUnrecognizedUnitNameUnknownType(definition.DefaultUnitLocations.DefaultUnitName?.AsRoslynLocation());
    }

    public Diagnostic EmptyDefaultUnit(IProcessingContext context, IRawDefaultUnitDefinition definition) => NullDefaultUnit(context, definition);

    public Diagnostic SetDefaultSymbolButNotUnit(IProcessingContext context, IRawDefaultUnitDefinition definition)
    {
        return DiagnosticConstruction.DefineQuantityDefaultUnit(definition.DefaultUnitLocations.DefaultUnitSymbol?.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic SetDefaultUnitButNotSymbol(IProcessingContext context, IRawDefaultUnitDefinition definition)
    {
        return DiagnosticConstruction.DefineQuantityDefaultSymbol(definition.DefaultUnitLocations.DefaultUnitName?.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic NullReciprocalQuantity(IProcessingContext context, UnprocessedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotScalar(definition.Locations.Reciprocal?.AsRoslynLocation());
    }

    public Diagnostic NullSquareQuantity(IProcessingContext context, UnprocessedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotScalar(definition.Locations.Square?.AsRoslynLocation());
    }

    public Diagnostic NullCubeQuantity(IProcessingContext context, UnprocessedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotScalar(definition.Locations.Cube?.AsRoslynLocation());
    }

    public Diagnostic NullSquareRootQuantity(IProcessingContext context, UnprocessedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotScalar(definition.Locations.SquareRoot?.AsRoslynLocation());
    }

    public Diagnostic NullCubeRootQuantity(IProcessingContext context, UnprocessedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotScalar(definition.Locations.CubeRoot?.AsRoslynLocation());
    }
}
