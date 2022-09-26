namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Empty.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;

internal sealed class EmptyExcludeUnitsProcessingDiagnostics : AEmptyUniqueItemListProcessingDiagnostics<string?, string, RawExcludeUnitsDefinition, ExcludeUnitsLocations>, IExcludeUnitsProcessingDiagnostics
{
    public static EmptyExcludeUnitsProcessingDiagnostics Instance { get; } = new();

    private EmptyExcludeUnitsProcessingDiagnostics() { }

    Diagnostic? IExcludeUnitsProcessingDiagnostics.EmptyItem(IUniqueItemListProcessingContext<string> context, RawExcludeUnitsDefinition definition, int index) => null;
}
