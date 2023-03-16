namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Empty.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;

internal sealed class EmptySharpMeasuresScalarProcessingDiagnostics : ISharpMeasuresScalarProcessingDiagnostics
{
    public static EmptySharpMeasuresScalarProcessingDiagnostics Instance { get; } = new();

    private EmptySharpMeasuresScalarProcessingDiagnostics() { }

    public Diagnostic? DifferenceDisabledButQuantitySpecified(IProcessingContext context, RawSharpMeasuresScalarDefinition definition) => null;
    public Diagnostic? EmptyDefaultUnitInstanceName(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    public Diagnostic? NullDefaultUnitInstanceName(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    public Diagnostic? NullDifferenceQuantity(IProcessingContext context, RawSharpMeasuresScalarDefinition definition) => null;
    public Diagnostic? NullUnit(IProcessingContext context, RawSharpMeasuresScalarDefinition definition) => null;
    public Diagnostic? NullVector(IProcessingContext context, RawSharpMeasuresScalarDefinition definition) => null;
    public Diagnostic? SetDefaultUnitInstanceNameButNotSymbol(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
    public Diagnostic? SetDefaultUnitInstanceSymbolButNotName(IProcessingContext context, IDefaultUnitInstanceDefinition definition) => null;
}
