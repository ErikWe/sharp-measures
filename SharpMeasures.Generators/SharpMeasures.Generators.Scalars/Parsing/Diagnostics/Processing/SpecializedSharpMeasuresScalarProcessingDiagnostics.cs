namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Processing;
using SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

internal sealed class SpecializedSharpMeasuresScalarProcessingDiagnostics : ISpecializedSharpMeasuresScalarProcessingDiagnostics
{
    public static SpecializedSharpMeasuresScalarProcessingDiagnostics Instance { get; } = new();

    private SpecializedSharpMeasuresScalarProcessingDiagnostics() { }

    public Diagnostic NullOriginalScalar(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotScalar(definition.Locations.OriginalQuantity?.AsRoslynLocation());
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

    public Diagnostic? UnrecognizedForwardsCastOperatorBehaviour(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedEnumValue(definition.Locations.ForwardsCastOperatorBehaviour?.AsRoslynLocation(), definition.ForwardsCastOperatorBehaviour);
    }

    public Diagnostic? UnrecognizedBackwardsCastOperatorBehaviour(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedEnumValue(definition.Locations.BackwardsCastOperatorBehaviour?.AsRoslynLocation(), definition.BackwardsCastOperatorBehaviour);
    }

    public Diagnostic NullDefaultUnitInstanceName(IProcessingContext context, IDefaultUnitInstanceDefinition definition)
    {
        return DefaultUnitInstanceProcessingDiagnostics.Instance.NullDefaultUnitInstanceName(context, definition);
    }

    public Diagnostic EmptyDefaultUnitInstanceName(IProcessingContext context, IDefaultUnitInstanceDefinition definition)
    {
        return DefaultUnitInstanceProcessingDiagnostics.Instance.EmptyDefaultUnitInstanceName(context, definition);
    }

    public Diagnostic SetDefaultUnitInstanceSymbolButNotName(IProcessingContext context, IDefaultUnitInstanceDefinition definition)
    {
        return DefaultUnitInstanceProcessingDiagnostics.Instance.SetDefaultUnitInstanceSymbolButNotName(context, definition);
    }

    public Diagnostic SetDefaultUnitInstanceNameButNotSymbol(IProcessingContext context, IDefaultUnitInstanceDefinition definition)
    {
        return DefaultUnitInstanceProcessingDiagnostics.Instance.SetDefaultUnitInstanceNameButNotSymbol(context, definition);
    }
}
