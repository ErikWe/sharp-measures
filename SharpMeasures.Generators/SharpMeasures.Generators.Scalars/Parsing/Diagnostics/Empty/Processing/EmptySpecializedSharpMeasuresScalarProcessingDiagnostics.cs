namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Empty.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

internal sealed class EmptySpecializedSharpMeasuresScalarProcessingDiagnostics : ISpecializedSharpMeasuresScalarProcessingDiagnostics
{
    public static EmptySpecializedSharpMeasuresScalarProcessingDiagnostics Instance { get; } = new();

    private EmptySpecializedSharpMeasuresScalarProcessingDiagnostics() { }

    public Diagnostic? DifferenceDisabledButQuantitySpecified(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition) => null;
    public Diagnostic? EmptyDefaultUnitInstanceName(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    public Diagnostic? NullDefaultUnitInstanceName(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    public Diagnostic? NullDifferenceQuantity(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition) => null;
    public Diagnostic? NullOriginalScalar(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition) => null;
    public Diagnostic? NullVector(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition) => null;
    public Diagnostic? SetDefaultUnitInstanceNameButNotSymbol(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    public Diagnostic? SetDefaultUnitInstanceSymbolButNotName(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    public Diagnostic? UnrecognizedForwardsCastOperatorBehaviour(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition) => null;
    public Diagnostic? UnrecognizedBackwardsCastOperatorBehaviour(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition) => null;
}
