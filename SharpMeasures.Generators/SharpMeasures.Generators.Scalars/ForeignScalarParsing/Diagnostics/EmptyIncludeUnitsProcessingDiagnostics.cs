namespace SharpMeasures.Generators.Scalars.ForeignScalarParsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;

internal class EmptyIncludeUnitsProcessingDiagnostics : AEmptyUniqueItemListProcessingDiagnostics<string?, string, RawIncludeUnitsDefinition, IncludeUnitsLocations>, IIncludeUnitsProcessingDiagnostics
{
    public static EmptyIncludeUnitsProcessingDiagnostics Instance { get; } = new();

    Diagnostic? IIncludeUnitsProcessingDiagnostics.EmptyItem(IUniqueItemListProcessingContext<string> context, RawIncludeUnitsDefinition definition, int index) => null;
    Diagnostic? IIncludeUnitsProcessingDiagnostics.UnrecognizedInclusionStackingMode(IUniqueItemListProcessingContext<string> context, RawIncludeUnitsDefinition definition) => null;
}
