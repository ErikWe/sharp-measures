namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

internal class SpecializedSharpMeasuresVectorGroupProcessingDiagnostics : ISpecializedSharpMeasuresVectorGroupProcessingDiagnostics
{
    public static SpecializedSharpMeasuresVectorGroupProcessingDiagnostics Instance { get; } = new();

    private SpecializedSharpMeasuresVectorGroupProcessingDiagnostics() { }

    public Diagnostic NullOriginalVectorGroup(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotUnit(definition.Locations.OriginalVectorGroup?.AsRoslynLocation());
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

    public Diagnostic NullDefaultUnit(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.NullUnrecognizedUnitNameUnknownType(definition.Locations.DefaultUnitName?.AsRoslynLocation());
    }

    public Diagnostic EmptyDefaultUnit(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition) => NullDefaultUnit(context, definition);

    public Diagnostic SetDefaultSymbolButNotUnit(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.DefineQuantityDefaultUnit(definition.Locations.DefaultUnitSymbol?.AsRoslynLocation());
    }

    public Diagnostic SetDefaultUnitButNotSymbol(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.DefineQuantityDefaultSymbol(definition.Locations.DefaultUnitName?.AsRoslynLocation());
    }
}
