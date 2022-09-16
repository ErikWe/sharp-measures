namespace SharpMeasures.Generators.Scalars.ForeignScalarParsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnitInstance;
using SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;

internal class EmptySharpMeasuresScalarProcessingDiagnostics : ISharpMeasuresScalarProcessingDiagnostics
{
    public static EmptySharpMeasuresScalarProcessingDiagnostics Instance { get; } = new();

    Diagnostic? ISharpMeasuresScalarProcessingDiagnostics.DifferenceDisabledButQuantitySpecified(IProcessingContext context, RawSharpMeasuresScalarDefinition definition) => null;
    Diagnostic? IDefaultUnitInstanceProcessingDiagnostics.EmptyDefaultUnitInstanceName(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    Diagnostic? ISharpMeasuresScalarProcessingDiagnostics.NullCubeQuantity(IProcessingContext context, RawSharpMeasuresScalarDefinition definition) => null;
    Diagnostic? ISharpMeasuresScalarProcessingDiagnostics.NullCubeRootQuantity(IProcessingContext context, RawSharpMeasuresScalarDefinition definition) => null;
    Diagnostic? IDefaultUnitInstanceProcessingDiagnostics.NullDefaultUnitInstanceName(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    Diagnostic? ISharpMeasuresScalarProcessingDiagnostics.NullDifferenceQuantity(IProcessingContext context, RawSharpMeasuresScalarDefinition definition) => null;
    Diagnostic? ISharpMeasuresScalarProcessingDiagnostics.NullReciprocalQuantity(IProcessingContext context, RawSharpMeasuresScalarDefinition definition) => null;
    Diagnostic? ISharpMeasuresScalarProcessingDiagnostics.NullSquareQuantity(IProcessingContext context, RawSharpMeasuresScalarDefinition definition) => null;
    Diagnostic? ISharpMeasuresScalarProcessingDiagnostics.NullSquareRootQuantity(IProcessingContext context, RawSharpMeasuresScalarDefinition definition) => null;
    Diagnostic? ISharpMeasuresScalarProcessingDiagnostics.NullUnit(IProcessingContext context, RawSharpMeasuresScalarDefinition definition) => null;
    Diagnostic? ISharpMeasuresScalarProcessingDiagnostics.NullVector(IProcessingContext context, RawSharpMeasuresScalarDefinition definition) => null;
    Diagnostic? IDefaultUnitInstanceProcessingDiagnostics.SetDefaultUnitInstanceNameButNotSymbol(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    Diagnostic? IDefaultUnitInstanceProcessingDiagnostics.SetDefaultUnitInstanceSymbolButNotName(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
}
