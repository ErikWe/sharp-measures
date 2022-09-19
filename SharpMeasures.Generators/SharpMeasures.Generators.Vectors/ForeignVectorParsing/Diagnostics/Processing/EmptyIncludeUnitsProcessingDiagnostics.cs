namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;

internal sealed class EmptyIncludeUnitsProcessingDiagnostics : AEmptyUniqueItemListProcessingDiagnostics<string?, string, RawIncludeUnitsDefinition, IncludeUnitsLocations>, IIncludeUnitsProcessingDiagnostics
{
    public static EmptyIncludeUnitsProcessingDiagnostics Instance { get; } = new();

    private EmptyIncludeUnitsProcessingDiagnostics() { }

    Diagnostic? IIncludeUnitsProcessingDiagnostics.EmptyItem(IUniqueItemListProcessingContext<string> context, RawIncludeUnitsDefinition definition, int index) => null;
    Diagnostic? IIncludeUnitsProcessingDiagnostics.UnrecognizedInclusionStackingMode(IUniqueItemListProcessingContext<string> context, RawIncludeUnitsDefinition definition) => null;
}
