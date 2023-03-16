namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Processing;
using SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;

internal sealed class SharpMeasuresScalarProcessingDiagnostics : ISharpMeasuresScalarProcessingDiagnostics
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
