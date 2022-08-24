namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnit;
using SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

internal class SpecializedSharpMeasuresScalarProcessingDiagnostics : ISharpMeasuresScalarSpecializationProcessingDiagnostics
{
    public static SpecializedSharpMeasuresScalarProcessingDiagnostics Instance { get; } = new();

    private SpecializedSharpMeasuresScalarProcessingDiagnostics() { }

    public Diagnostic NullOriginalScalar(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotScalar(definition.Locations.OriginalScalar?.AsRoslynLocation());
    }

    public Diagnostic NullVector(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotVector(definition.Locations.Vector?.AsRoslynLocation());
    }

    public Diagnostic NullDifferenceQuantity(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotScalar(definition.Locations.Difference?.AsRoslynLocation());
    }

    public Diagnostic DifferenceDisabledButQuantitySpecified(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.DifferenceDisabledButQuantitySpecified(definition.Locations.Difference?.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic NullDefaultUnit(IProcessingContext context, IDefaultUnitDefinition definition)
    {
        return DiagnosticConstruction.NullUnrecognizedUnitNameUnknownType(definition.DefaultUnitLocations.DefaultUnitName?.AsRoslynLocation());
    }

    public Diagnostic EmptyDefaultUnit(IProcessingContext context, IDefaultUnitDefinition definition) => NullDefaultUnit(context, definition);

    public Diagnostic SetDefaultSymbolButNotUnit(IProcessingContext context, IDefaultUnitDefinition definition)
    {
        return DiagnosticConstruction.DefineQuantityDefaultUnit(definition.DefaultUnitLocations.DefaultUnitSymbol?.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic SetDefaultUnitButNotSymbol(IProcessingContext context, IDefaultUnitDefinition definition)
    {
        return DiagnosticConstruction.DefineQuantityDefaultSymbol(definition.DefaultUnitLocations.DefaultUnitName?.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic NullDefaultUnit(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.NullUnrecognizedUnitNameUnknownType(definition.Locations.DefaultUnitName?.AsRoslynLocation());
    }

    public Diagnostic EmptyDefaultUnit(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition) => NullDefaultUnit(context, definition);

    public Diagnostic SetDefaultSymbolButNotUnit(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.DefineQuantityDefaultUnit(definition.Locations.DefaultUnitSymbol?.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic SetDefaultUnitButNotSymbol(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.DefineQuantityDefaultSymbol(definition.Locations.DefaultUnitName?.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic NullReciprocalQuantity(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotScalar(definition.Locations.Reciprocal?.AsRoslynLocation());
    }

    public Diagnostic NullSquareQuantity(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotScalar(definition.Locations.Square?.AsRoslynLocation());
    }

    public Diagnostic NullCubeQuantity(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotScalar(definition.Locations.Cube?.AsRoslynLocation());
    }

    public Diagnostic NullSquareRootQuantity(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotScalar(definition.Locations.SquareRoot?.AsRoslynLocation());
    }

    public Diagnostic NullCubeRootQuantity(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotScalar(definition.Locations.CubeRoot?.AsRoslynLocation());
    }
}
