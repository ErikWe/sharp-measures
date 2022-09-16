namespace SharpMeasures.Generators.Scalars.ForeignScalarParsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;

internal class EmptyExcludeUnitsProcessingDiagnostics : AEmptyUniqueItemListProcessingDiagnostics<string?, string, RawExcludeUnitsDefinition, ExcludeUnitsLocations>, IExcludeUnitsProcessingDiagnostics
{
    public static EmptyExcludeUnitsProcessingDiagnostics Instance { get; } = new();

    Diagnostic? IExcludeUnitsProcessingDiagnostics.EmptyItem(IUniqueItemListProcessingContext<string> context, RawExcludeUnitsDefinition definition, int index) => null;
}
