namespace SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Quantities.Parsing.Abstractions;

using System;
using System.Collections.Generic;

public class IncludeUnitsProcesser : AItemListProcesser<string, string, IItemListProcessingContext<string>, RawIncludeUnitsDefinition, IncludeUnitsDefinition>
{
    public IncludeUnitsProcesser(IUnitInclusionOrExclusionListProcessingDiagnostics<RawIncludeUnitsDefinition> diagnostics) : base(diagnostics) { }

    protected override IncludeUnitsDefinition ConstructProduct(IReadOnlyList<string> items, RawIncludeUnitsDefinition definition)
    {
        return new(items, definition.Locations);
    }

    protected override string UpgradeItem(string item) => item;
    protected override string UpgradeNullItem(string? item)
    {
        if (item is null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        return item;
    }
}
