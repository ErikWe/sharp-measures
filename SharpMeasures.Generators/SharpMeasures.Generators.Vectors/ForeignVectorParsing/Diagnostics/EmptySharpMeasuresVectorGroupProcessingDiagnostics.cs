namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnitInstance;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

internal class EmptySharpMeasuresVectorGroupProcessingDiagnostics : ISharpMeasuresVectorGroupProcessingDiagnostics
{
    public static EmptySharpMeasuresVectorGroupProcessingDiagnostics Instance { get; } = new();

    Diagnostic? ISharpMeasuresVectorGroupProcessingDiagnostics.DifferenceDisabledButQuantitySpecified(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition) => null;
    Diagnostic? IDefaultUnitInstanceProcessingDiagnostics.EmptyDefaultUnitInstanceName(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    Diagnostic? ISharpMeasuresVectorGroupProcessingDiagnostics.NameSuggestsDimension(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition, int interpretedDimension) => null;
    Diagnostic? IDefaultUnitInstanceProcessingDiagnostics.NullDefaultUnitInstanceName(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    Diagnostic? ISharpMeasuresVectorGroupProcessingDiagnostics.NullDifferenceQuantity(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition) => null;
    Diagnostic? ISharpMeasuresVectorGroupProcessingDiagnostics.NullScalar(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition) => null;
    Diagnostic? ISharpMeasuresVectorGroupProcessingDiagnostics.NullUnit(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition) => null;
    Diagnostic? IDefaultUnitInstanceProcessingDiagnostics.SetDefaultUnitInstanceNameButNotSymbol(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    Diagnostic? IDefaultUnitInstanceProcessingDiagnostics.SetDefaultUnitInstanceSymbolButNotName(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
}
