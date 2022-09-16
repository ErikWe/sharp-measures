namespace SharpMeasures.Generators.Scalars.ForeignScalarParsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Scalars.Parsing.IncludeUnitBases;

internal class EmptyIncludeUnitBasesProcessingDiagnostics : AEmptyUniqueItemListProcessingDiagnostics<string?, string, RawIncludeUnitBasesDefinition, IncludeUnitBasesLocations>, IIncludeUnitBasesProcessingDiagnostics
{
    public static EmptyIncludeUnitBasesProcessingDiagnostics Instance { get; } = new();

    Diagnostic? IIncludeUnitBasesProcessingDiagnostics.EmptyItem(IUniqueItemListProcessingContext<string> context, RawIncludeUnitBasesDefinition definition, int index) => null;
    Diagnostic? IIncludeUnitBasesProcessingDiagnostics.UnrecognizedInclusionStackingMode(IUniqueItemListProcessingContext<string> context, RawIncludeUnitBasesDefinition definition) => null;
}
