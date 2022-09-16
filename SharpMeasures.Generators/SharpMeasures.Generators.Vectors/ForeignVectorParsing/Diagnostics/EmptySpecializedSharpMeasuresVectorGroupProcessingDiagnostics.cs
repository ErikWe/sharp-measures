namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnitInstance;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

internal class EmptySpecializedSharpMeasuresVectorGroupProcessingDiagnostics : ISpecializedSharpMeasuresVectorGroupProcessingDiagnostics
{
    public static EmptySpecializedSharpMeasuresVectorGroupProcessingDiagnostics Instance { get; } = new();

    Diagnostic? ISpecializedSharpMeasuresVectorGroupProcessingDiagnostics.DifferenceDisabledButQuantitySpecified(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition) => null;
    Diagnostic? IDefaultUnitInstanceProcessingDiagnostics.EmptyDefaultUnitInstanceName(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    Diagnostic? ISpecializedSharpMeasuresVectorGroupProcessingDiagnostics.NameSuggestsDimension(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition, int interpretedDimension) => null;
    Diagnostic? IDefaultUnitInstanceProcessingDiagnostics.NullDefaultUnitInstanceName(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    Diagnostic? ISpecializedSharpMeasuresVectorGroupProcessingDiagnostics.NullDifferenceQuantity(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition) => null;
    Diagnostic? ISpecializedSharpMeasuresVectorGroupProcessingDiagnostics.NullOriginalVectorGroup(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition) => null;
    Diagnostic? ISpecializedSharpMeasuresVectorGroupProcessingDiagnostics.NullScalar(IProcessingContext context, RawSpecializedSharpMeasuresVectorGroupDefinition definition) => null;
    Diagnostic? IDefaultUnitInstanceProcessingDiagnostics.SetDefaultUnitInstanceNameButNotSymbol(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    Diagnostic? IDefaultUnitInstanceProcessingDiagnostics.SetDefaultUnitInstanceSymbolButNotName(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
}
