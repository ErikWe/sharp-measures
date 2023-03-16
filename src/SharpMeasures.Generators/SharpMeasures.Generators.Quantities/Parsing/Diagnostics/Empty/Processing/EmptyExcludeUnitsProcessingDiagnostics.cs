namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Empty.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;

public sealed class EmptyExcludeUnitsProcessingDiagnostics : AEmptyUniqueItemListProcessingDiagnostics<string?, string, RawExcludeUnitsDefinition, ExcludeUnitsLocations>, IExcludeUnitsProcessingDiagnostics
{
    public static EmptyExcludeUnitsProcessingDiagnostics Instance { get; } = new();

    private EmptyExcludeUnitsProcessingDiagnostics() { }

    public Diagnostic? EmptyItem(IUniqueItemListProcessingContext<string> context, RawExcludeUnitsDefinition definition, int index) => null;
}
