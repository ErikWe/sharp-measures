namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
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

    public Diagnostic NullDefaultUnit(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.NullUnrecognizedUnitNameUnknownType(definition.Locations.DefaultUnitName?.AsRoslynLocation());
    }

    public Diagnostic EmptyDefaultUnit(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition) => NullDefaultUnit(context, definition);

    public Diagnostic SetDefaultSymbolButNotUnit(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.DefineQuantityDefaultUnit(definition.Locations.DefaultUnitSymbol?.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic SetDefaultUnitButNotSymbol(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.DefineQuantityDefaultSymbol(definition.Locations.DefaultUnitName?.AsRoslynLocation(), context.Type.Name);
    }
}
