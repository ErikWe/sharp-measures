namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Processing;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;

internal sealed class SpecializedSharpMeasuresVectorProcessingDiagnostics : ISpecializedSharpMeasuresVectorProcessingDiagnostics
{
    public static SpecializedSharpMeasuresVectorProcessingDiagnostics Instance { get; } = new();

    private SpecializedSharpMeasuresVectorProcessingDiagnostics() { }

    public Diagnostic NullOriginalVector(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotVector(definition.Locations.OriginalQuantity?.AsRoslynLocation());
    }

    public Diagnostic NullScalar(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotScalar(definition.Locations.Scalar?.AsRoslynLocation());
    }

    public Diagnostic NullDifferenceQuantity(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotVector(definition.Locations.Difference?.AsRoslynLocation());
    }

    public Diagnostic DifferenceDisabledButQuantitySpecified(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition)
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
