namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnitInstance;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

internal sealed class EmptySpecializedSharpMeasuresVectorGroupProcessingDiagnostics : ISpecializedSharpMeasuresVectorGroupProcessingDiagnostics
{
    public static EmptySpecializedSharpMeasuresVectorGroupProcessingDiagnostics Instance { get; } = new();

    private EmptySpecializedSharpMeasuresVectorGroupProcessingDiagnostics() { }

    Diagnostic? ISpecializedSharpMeasuresVectorGroupProcessingDiagnostics.DifferenceDisabledButQuantitySpecified(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition) => null;
    Diagnostic? IDefaultUnitInstanceProcessingDiagnostics.EmptyDefaultUnitInstanceName(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    Diagnostic? ISpecializedSharpMeasuresVectorGroupProcessingDiagnostics.NameSuggestsDimension(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition, int interpretedDimension) => null;
    Diagnostic? IDefaultUnitInstanceProcessingDiagnostics.NullDefaultUnitInstanceName(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    Diagnostic? ISpecializedSharpMeasuresVectorGroupProcessingDiagnostics.NullDifferenceQuantity(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition) => null;
    Diagnostic? ISpecializedSharpMeasuresVectorGroupProcessingDiagnostics.NullOriginalVectorGroup(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition) => null;
    Diagnostic? ISpecializedSharpMeasuresVectorGroupProcessingDiagnostics.NullScalar(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition) => null;
    Diagnostic? IDefaultUnitInstanceProcessingDiagnostics.SetDefaultUnitInstanceNameButNotSymbol(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    Diagnostic? IDefaultUnitInstanceProcessingDiagnostics.SetDefaultUnitInstanceSymbolButNotName(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    Diagnostic? ISpecializedSharpMeasuresVectorGroupProcessingDiagnostics.UnrecognizedForwardsCastOperatorBehaviour(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition) => null;
    Diagnostic? ISpecializedSharpMeasuresVectorGroupProcessingDiagnostics.UnrecognizedBackwardsCastOperatorBehaviour(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition) => null;
}
