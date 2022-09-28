namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Empty.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

internal sealed class EmptySpecializedSharpMeasuresVectorGroupProcessingDiagnostics : ISpecializedSharpMeasuresVectorGroupProcessingDiagnostics
{
    public static EmptySpecializedSharpMeasuresVectorGroupProcessingDiagnostics Instance { get; } = new();

    private EmptySpecializedSharpMeasuresVectorGroupProcessingDiagnostics() { }

    public Diagnostic? DifferenceDisabledButQuantitySpecified(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition) => null;
    public Diagnostic? EmptyDefaultUnitInstanceName(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    public Diagnostic? NameSuggestsDimension(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition, int interpretedDimension) => null;
    public Diagnostic? NullDefaultUnitInstanceName(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    public Diagnostic? NullDifferenceQuantity(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition) => null;
    public Diagnostic? NullOriginalVectorGroup(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition) => null;
    public Diagnostic? NullScalar(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition) => null;
    public Diagnostic? SetDefaultUnitInstanceNameButNotSymbol(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    public Diagnostic? SetDefaultUnitInstanceSymbolButNotName(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    public Diagnostic? UnrecognizedForwardsCastOperatorBehaviour(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition) => null;
    public Diagnostic? UnrecognizedBackwardsCastOperatorBehaviour(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition) => null;
}
