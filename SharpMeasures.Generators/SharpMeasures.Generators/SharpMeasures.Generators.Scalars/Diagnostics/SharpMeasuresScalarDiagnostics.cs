namespace SharpMeasures.Generators.Scalars.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;
using SharpMeasures.Generators.Scalars.Refinement.SharpMeasuresScalar;

internal class SharpMeasuresScalarDiagnostics : ISharpMeasuresScalarProcessingDiagnostics, ISharpMeasuresScalarRefinementDiagnostics
{
    public static SharpMeasuresScalarDiagnostics Instance { get; } = new();

    private SharpMeasuresScalarDiagnostics() { }

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
        return DiagnosticConstruction.DefineQuantityDefaultUnit(definition.Locations.DefaultUnitSymbol?.AsRoslynLocation());
    }

    public Diagnostic SetDefaultUnitButNotSymbol(IProcessingContext context, RawSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.DefineQuantityDefaultSymbol(definition.Locations.DefaultUnitName?.AsRoslynLocation());
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

    public Diagnostic TypeAlreadyUnit(ISharpMeasuresScalarRefinementContext context, SharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.ScalarTypeAlreadyDefinedAsUnit(definition.Locations.Attribute.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeNotUnit(ISharpMeasuresScalarRefinementContext context, SharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotUnit(definition.Locations.Unit?.AsRoslynLocation(), definition.Unit.Name);
    }

    public Diagnostic UnitNotIncludingBiasTerm(ISharpMeasuresScalarRefinementContext context, SharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.UnitNotIncludingBiasTerm(definition.Locations.UseUnitBias?.AsRoslynLocation(), definition.Unit.Name);
    }

    public Diagnostic TypeNotVector(ISharpMeasuresScalarRefinementContext context, SharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotVector(definition.Locations.Vector?.AsRoslynLocation(), definition.Vector!.Value.Name);
    }

    public Diagnostic UnrecognizedDefaultUnit(ISharpMeasuresScalarRefinementContext context, SharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedUnitName(definition.Locations.DefaultUnitName?.AsRoslynLocation(), definition.DefaultUnitName!, definition.Unit.Name);
    }

    public Diagnostic DifferenceNotScalar(ISharpMeasuresScalarRefinementContext context, SharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Difference?.AsRoslynLocation(), definition.Reciprocal!.Value.Name);
    }

    public Diagnostic ReciprocalNotScalar(ISharpMeasuresScalarRefinementContext context, SharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Reciprocal?.AsRoslynLocation(), definition.Reciprocal!.Value.Name);
    }

    public Diagnostic SquareNotScalar(ISharpMeasuresScalarRefinementContext context, SharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Square?.AsRoslynLocation(), definition.Square!.Value.Name);
    }

    public Diagnostic CubeNotScalar(ISharpMeasuresScalarRefinementContext context, SharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Cube?.AsRoslynLocation(), definition.Cube!.Value.Name);
    }

    public Diagnostic SquareRootNotScalar(ISharpMeasuresScalarRefinementContext context, SharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.SquareRoot?.AsRoslynLocation(), definition.SquareRoot!.Value.Name);
    }

    public Diagnostic CubeRootNotScalar(ISharpMeasuresScalarRefinementContext context, SharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.CubeRoot?.AsRoslynLocation(), definition.CubeRoot!.Value.Name);
    }
}
