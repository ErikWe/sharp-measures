namespace SharpMeasures.Generators.Scalars.ForeignScalarParsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnitInstance;
using SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

internal class EmptySpecializedSharpMeasuresScalarProcessingDiagnostics : ISpecializedSharpMeasuresScalarProcessingDiagnostics
{
    public static EmptySpecializedSharpMeasuresScalarProcessingDiagnostics Instance { get; } = new();

    Diagnostic? ISpecializedSharpMeasuresScalarProcessingDiagnostics.DifferenceDisabledButQuantitySpecified(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition) => null;
    Diagnostic? IDefaultUnitInstanceProcessingDiagnostics.EmptyDefaultUnitInstanceName(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    Diagnostic? ISpecializedSharpMeasuresScalarProcessingDiagnostics.NullCubeQuantity(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition) => null;
    Diagnostic? ISpecializedSharpMeasuresScalarProcessingDiagnostics.NullCubeRootQuantity(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition) => null;
    Diagnostic? IDefaultUnitInstanceProcessingDiagnostics.NullDefaultUnitInstanceName(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    Diagnostic? ISpecializedSharpMeasuresScalarProcessingDiagnostics.NullDifferenceQuantity(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition) => null;
    Diagnostic? ISpecializedSharpMeasuresScalarProcessingDiagnostics.NullOriginalScalar(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition) => null;
    Diagnostic? ISpecializedSharpMeasuresScalarProcessingDiagnostics.NullReciprocalQuantity(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition) => null;
    Diagnostic? ISpecializedSharpMeasuresScalarProcessingDiagnostics.NullSquareQuantity(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition) => null;
    Diagnostic? ISpecializedSharpMeasuresScalarProcessingDiagnostics.NullSquareRootQuantity(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition) => null;
    Diagnostic? ISpecializedSharpMeasuresScalarProcessingDiagnostics.NullVector(IProcessingContext context, RawSpecializedSharpMeasuresScalarDefinition definition) => null;
    Diagnostic? IDefaultUnitInstanceProcessingDiagnostics.SetDefaultUnitInstanceNameButNotSymbol(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    Diagnostic? IDefaultUnitInstanceProcessingDiagnostics.SetDefaultUnitInstanceSymbolButNotName(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
}
