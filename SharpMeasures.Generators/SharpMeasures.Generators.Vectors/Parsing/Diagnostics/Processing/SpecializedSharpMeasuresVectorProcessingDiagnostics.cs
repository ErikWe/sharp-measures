namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnit;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Processing;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;

internal class SpecializedSharpMeasuresVectorProcessingDiagnostics : ISpecializedSharpMeasuresVectorProcessingDiagnostics
{
    public static SpecializedSharpMeasuresVectorProcessingDiagnostics Instance { get; } = new();

    private SpecializedSharpMeasuresVectorProcessingDiagnostics() { }

    public Diagnostic NullOriginalVector(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotVector(definition.Locations.OriginalVector?.AsRoslynLocation());
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

    public Diagnostic NullDefaultUnit(IProcessingContext context, IDefaultUnitDefinition definition)
    {
        return DefaultUnitProcessingDiagnostics.Instance.NullDefaultUnit(context, definition);
    }

    public Diagnostic EmptyDefaultUnit(IProcessingContext context, IDefaultUnitDefinition definition)
    {
        return DefaultUnitProcessingDiagnostics.Instance.EmptyDefaultUnit(context, definition);
    }

    public Diagnostic SetDefaultSymbolButNotUnit(IProcessingContext context, IDefaultUnitDefinition definition)
    {
        return DefaultUnitProcessingDiagnostics.Instance.SetDefaultSymbolButNotUnit(context, definition);
    }

    public Diagnostic SetDefaultUnitButNotSymbol(IProcessingContext context, IDefaultUnitDefinition definition)
    {
        return DefaultUnitProcessingDiagnostics.Instance.SetDefaultUnitButNotSymbol(context, definition);
    }
}
