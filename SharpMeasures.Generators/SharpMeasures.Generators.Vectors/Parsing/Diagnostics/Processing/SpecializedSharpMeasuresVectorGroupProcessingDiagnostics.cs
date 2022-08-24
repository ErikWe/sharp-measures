namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnit;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Processing;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

internal class SpecializedSharpMeasuresVectorGroupProcessingDiagnostics : ISpecializedSharpMeasuresVectorGroupProcessingDiagnostics
{
    public static SpecializedSharpMeasuresVectorGroupProcessingDiagnostics Instance { get; } = new();

    private SpecializedSharpMeasuresVectorGroupProcessingDiagnostics() { }

    public Diagnostic NameSuggestsDimension(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition, int interpretedDImension)
    {
        return DiagnosticConstruction.VectorGroupNameSuggestsDimension(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name, interpretedDImension);
    }

    public Diagnostic NullOriginalVectorGroup(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotVectorGroup(definition.Locations.OriginalVectorGroup?.AsRoslynLocation());
    }

    public Diagnostic NullScalar(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotScalar(definition.Locations.Scalar?.AsRoslynLocation());
    }

    public Diagnostic NullDifferenceQuantity(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotVectorGroup(definition.Locations.Difference?.AsRoslynLocation());
    }

    public Diagnostic DifferenceDisabledButQuantitySpecified(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition)
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
