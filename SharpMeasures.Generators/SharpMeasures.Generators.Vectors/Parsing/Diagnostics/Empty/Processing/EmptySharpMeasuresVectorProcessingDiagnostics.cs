namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Empty.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;

internal sealed class EmptySharpMeasuresVectorProcessingDiagnostics : ISharpMeasuresVectorProcessingDiagnostics
{
    public static EmptySharpMeasuresVectorProcessingDiagnostics Instance { get; } = new();

    private EmptySharpMeasuresVectorProcessingDiagnostics() { }

    public Diagnostic? DifferenceDisabledButQuantitySpecified(IProcessingContext context, RawSharpMeasuresVectorDefinition definition) => null;
    public Diagnostic? EmptyDefaultUnitInstanceName(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    public Diagnostic? InvalidDimension(IProcessingContext context, RawSharpMeasuresVectorDefinition definition) => null;
    public Diagnostic? InvalidInterpretedDimension(IProcessingContext context, RawSharpMeasuresVectorDefinition definition, int dimension) => null;
    public Diagnostic? MissingDimension(IProcessingContext context, RawSharpMeasuresVectorDefinition definition) => null;
    public Diagnostic? NullDefaultUnitInstanceName(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    public Diagnostic? NullDifferenceQuantity(IProcessingContext context, RawSharpMeasuresVectorDefinition definition) => null;
    public Diagnostic? NullScalar(IProcessingContext context, RawSharpMeasuresVectorDefinition definition) => null;
    public Diagnostic? NullUnit(IProcessingContext context, RawSharpMeasuresVectorDefinition definition) => null;
    public Diagnostic? SetDefaultUnitInstanceNameButNotSymbol(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    public Diagnostic? SetDefaultUnitInstanceSymbolButNotName(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    public Diagnostic? VectorNameAndDimensionConflict(IProcessingContext context, RawSharpMeasuresVectorDefinition definition, int interpretedDimension) => null;
}
