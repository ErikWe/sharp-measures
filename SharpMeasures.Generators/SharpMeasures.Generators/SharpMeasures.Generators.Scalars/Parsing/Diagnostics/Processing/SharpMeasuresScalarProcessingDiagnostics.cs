namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;

internal class SharpMeasuresScalarProcessingDiagnostics : ISharpMeasuresScalarProcessingDiagnostics
{
    public static SharpMeasuresScalarProcessingDiagnostics Instance { get; } = new();

    private SharpMeasuresScalarProcessingDiagnostics() { }

    public Diagnostic NullUnit(IProcessingContext context, RawSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotUnit(definition.Locations.Unit?.AsRoslynLocation());
    }

    public Diagnostic NullVector(IProcessingContext context, RawSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotVector(definition.Locations.Vector?.AsRoslynLocation());
    }

    public Diagnostic NullDifferenceQuantity(IProcessingContext context, RawSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotScalar(definition.Locations.Difference?.AsRoslynLocation());
    }

    public Diagnostic DifferenceDisabledButQuantitySpecified(IProcessingContext context, RawSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.DifferenceDisabledButQuantitySpecified(definition.Locations.Difference?.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic NullDefaultUnit(IProcessingContext context, RawSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.NullUnrecognizedUnitName(definition.Locations.DefaultUnitName?.AsRoslynLocation(), definition.Unit!.Value.Name);
    }

    public Diagnostic EmptyDefaultUnit(IProcessingContext context, RawSharpMeasuresScalarDefinition definition) => NullDefaultUnit(context, definition);

    public Diagnostic SetDefaultSymbolButNotUnit(IProcessingContext context, RawSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.DefineQuantityDefaultUnit(definition.Locations.DefaultUnitSymbol?.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic SetDefaultUnitButNotSymbol(IProcessingContext context, RawSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.DefineQuantityDefaultSymbol(definition.Locations.DefaultUnitName?.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic NullReciprocalQuantity(IProcessingContext context, RawSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotScalar(definition.Locations.Reciprocal?.AsRoslynLocation());
    }

    public Diagnostic NullSquareQuantity(IProcessingContext context, RawSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotScalar(definition.Locations.Square?.AsRoslynLocation());
    }

    public Diagnostic NullCubeQuantity(IProcessingContext context, RawSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotScalar(definition.Locations.Cube?.AsRoslynLocation());
    }

    public Diagnostic NullSquareRootQuantity(IProcessingContext context, RawSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotScalar(definition.Locations.SquareRoot?.AsRoslynLocation());
    }

    public Diagnostic NullCubeRootQuantity(IProcessingContext context, RawSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotScalar(definition.Locations.CubeRoot?.AsRoslynLocation());
    }
}
