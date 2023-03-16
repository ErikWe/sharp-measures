namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Empty.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;

public sealed class EmptyIncludeUnitsProcessingDiagnostics : AEmptyUniqueItemListProcessingDiagnostics<string?, string, RawIncludeUnitsDefinition, IncludeUnitsLocations>, IIncludeUnitsProcessingDiagnostics
{
    public static EmptyIncludeUnitsProcessingDiagnostics Instance { get; } = new();

    private EmptyIncludeUnitsProcessingDiagnostics() { }

    public Diagnostic? EmptyItem(IUniqueItemListProcessingContext<string> context, RawIncludeUnitsDefinition definition, int index) => null;
    public Diagnostic? UnrecognizedInclusionStackingMode(IUniqueItemListProcessingContext<string> context, RawIncludeUnitsDefinition definition) => null;
}
