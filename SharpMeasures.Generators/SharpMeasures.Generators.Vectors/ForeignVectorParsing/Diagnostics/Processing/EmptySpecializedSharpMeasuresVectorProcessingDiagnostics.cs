﻿namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnitInstance;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;

internal sealed class EmptySpecializedSharpMeasuresVectorProcessingDiagnostics : ISpecializedSharpMeasuresVectorProcessingDiagnostics
{
    public static EmptySpecializedSharpMeasuresVectorProcessingDiagnostics Instance { get; } = new();

    private EmptySpecializedSharpMeasuresVectorProcessingDiagnostics() { }

    Diagnostic? ISpecializedSharpMeasuresVectorProcessingDiagnostics.DifferenceDisabledButQuantitySpecified(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition) => null;
    Diagnostic? IDefaultUnitInstanceProcessingDiagnostics.EmptyDefaultUnitInstanceName(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    Diagnostic? IDefaultUnitInstanceProcessingDiagnostics.NullDefaultUnitInstanceName(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    Diagnostic? ISpecializedSharpMeasuresVectorProcessingDiagnostics.NullDifferenceQuantity(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition) => null;
    Diagnostic? ISpecializedSharpMeasuresVectorProcessingDiagnostics.NullOriginalVector(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition) => null;
    Diagnostic? ISpecializedSharpMeasuresVectorProcessingDiagnostics.NullScalar(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition) => null;
    Diagnostic? IDefaultUnitInstanceProcessingDiagnostics.SetDefaultUnitInstanceNameButNotSymbol(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    Diagnostic? IDefaultUnitInstanceProcessingDiagnostics.SetDefaultUnitInstanceSymbolButNotName(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    Diagnostic? ISpecializedSharpMeasuresVectorProcessingDiagnostics.UnrecognizedForwardsCastOperatorBehaviour(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition) => null;
    Diagnostic? ISpecializedSharpMeasuresVectorProcessingDiagnostics.UnrecognizedBackwardsCastOperatorBehaviour(IProcessingContext context, RawSpecializedSharpMeasuresVectorDefinition definition) => null;
}