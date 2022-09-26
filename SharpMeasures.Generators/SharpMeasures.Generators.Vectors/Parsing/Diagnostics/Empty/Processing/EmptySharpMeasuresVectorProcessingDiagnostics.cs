namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Empty.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnitInstance;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;

internal sealed class EmptySharpMeasuresVectorProcessingDiagnostics : ISharpMeasuresVectorProcessingDiagnostics
{
    public static EmptySharpMeasuresVectorProcessingDiagnostics Instance { get; } = new();

    private EmptySharpMeasuresVectorProcessingDiagnostics() { }

    Diagnostic? ISharpMeasuresVectorProcessingDiagnostics.DifferenceDisabledButQuantitySpecified(IProcessingContext context, RawSharpMeasuresVectorDefinition definition) => null;
    Diagnostic? IDefaultUnitInstanceProcessingDiagnostics.EmptyDefaultUnitInstanceName(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    Diagnostic? ISharpMeasuresVectorProcessingDiagnostics.InvalidDimension(IProcessingContext context, RawSharpMeasuresVectorDefinition definition) => null;
    Diagnostic? ISharpMeasuresVectorProcessingDiagnostics.InvalidInterpretedDimension(IProcessingContext context, RawSharpMeasuresVectorDefinition definition, int dimension) => null;
    Diagnostic? ISharpMeasuresVectorProcessingDiagnostics.MissingDimension(IProcessingContext context, RawSharpMeasuresVectorDefinition definition) => null;
    Diagnostic? IDefaultUnitInstanceProcessingDiagnostics.NullDefaultUnitInstanceName(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    Diagnostic? ISharpMeasuresVectorProcessingDiagnostics.NullDifferenceQuantity(IProcessingContext context, RawSharpMeasuresVectorDefinition definition) => null;
    Diagnostic? ISharpMeasuresVectorProcessingDiagnostics.NullScalar(IProcessingContext context, RawSharpMeasuresVectorDefinition definition) => null;
    Diagnostic? ISharpMeasuresVectorProcessingDiagnostics.NullUnit(IProcessingContext context, RawSharpMeasuresVectorDefinition definition) => null;
    Diagnostic? IDefaultUnitInstanceProcessingDiagnostics.SetDefaultUnitInstanceNameButNotSymbol(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    Diagnostic? IDefaultUnitInstanceProcessingDiagnostics.SetDefaultUnitInstanceSymbolButNotName(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    Diagnostic? ISharpMeasuresVectorProcessingDiagnostics.VectorNameAndDimensionConflict(IProcessingContext context, RawSharpMeasuresVectorDefinition definition, int interpretedDimension) => null;
}
