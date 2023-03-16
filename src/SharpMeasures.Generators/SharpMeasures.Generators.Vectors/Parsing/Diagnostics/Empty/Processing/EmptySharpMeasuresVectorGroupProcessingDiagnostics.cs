namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Empty.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

internal sealed class EmptySharpMeasuresVectorGroupProcessingDiagnostics : ISharpMeasuresVectorGroupProcessingDiagnostics
{
    public static EmptySharpMeasuresVectorGroupProcessingDiagnostics Instance { get; } = new();

    private EmptySharpMeasuresVectorGroupProcessingDiagnostics() { }

    public Diagnostic? DifferenceDisabledButQuantitySpecified(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition) => null;
    public Diagnostic? EmptyDefaultUnitInstanceName(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    public Diagnostic? NameSuggestsDimension(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition, int interpretedDimension) => null;
    public Diagnostic? NullDefaultUnitInstanceName(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    public Diagnostic? NullDifferenceQuantity(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition) => null;
    public Diagnostic? NullScalar(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition) => null;
    public Diagnostic? NullUnit(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition) => null;
    public Diagnostic? SetDefaultUnitInstanceNameButNotSymbol(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    public Diagnostic? SetDefaultUnitInstanceSymbolButNotName(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
}
