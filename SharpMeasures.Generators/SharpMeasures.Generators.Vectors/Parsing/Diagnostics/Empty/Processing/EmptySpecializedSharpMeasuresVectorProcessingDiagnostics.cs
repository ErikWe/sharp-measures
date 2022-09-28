namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Empty.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;

internal sealed class EmptySpecializedSharpMeasuresVectorProcessingDiagnostics : ISpecializedSharpMeasuresVectorProcessingDiagnostics
{
    public static EmptySpecializedSharpMeasuresVectorProcessingDiagnostics Instance { get; } = new();

    private EmptySpecializedSharpMeasuresVectorProcessingDiagnostics() { }

    public Diagnostic? DifferenceDisabledButQuantitySpecified(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition) => null;
    public Diagnostic? EmptyDefaultUnitInstanceName(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    public Diagnostic? NullDefaultUnitInstanceName(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    public Diagnostic? NullDifferenceQuantity(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition) => null;
    public Diagnostic? NullOriginalVector(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition) => null;
    public Diagnostic? NullScalar(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition) => null;
    public Diagnostic? SetDefaultUnitInstanceNameButNotSymbol(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    public Diagnostic? SetDefaultUnitInstanceSymbolButNotName(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    public Diagnostic? UnrecognizedForwardsCastOperatorBehaviour(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition) => null;
    public Diagnostic? UnrecognizedBackwardsCastOperatorBehaviour(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition) => null;
}
