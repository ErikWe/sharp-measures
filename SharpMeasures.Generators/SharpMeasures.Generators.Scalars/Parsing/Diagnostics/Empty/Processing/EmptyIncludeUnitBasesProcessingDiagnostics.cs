namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Empty.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Scalars.Parsing.IncludeUnitBases;

internal sealed class EmptyIncludeUnitBasesProcessingDiagnostics : AEmptyUniqueItemListProcessingDiagnostics<string?, string, RawIncludeUnitBasesDefinition, IncludeUnitBasesLocations>, IIncludeUnitBasesProcessingDiagnostics
{
    public static EmptyIncludeUnitBasesProcessingDiagnostics Instance { get; } = new();

    private EmptyIncludeUnitBasesProcessingDiagnostics() { }

    Diagnostic? IIncludeUnitBasesProcessingDiagnostics.EmptyItem(IUniqueItemListProcessingContext<string> context, RawIncludeUnitBasesDefinition definition, int index) => null;
    Diagnostic? IIncludeUnitBasesProcessingDiagnostics.UnrecognizedInclusionStackingMode(IUniqueItemListProcessingContext<string> context, RawIncludeUnitBasesDefinition definition) => null;
}
