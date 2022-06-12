namespace SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Quantities.Parsing.Abstractions;

using System;
using System.Collections.Generic;

public class ExcludeUnitsProcesser : AItemListProcesser<string, string, IItemListProcessingContext<string>, RawExcludeUnitsDefinition, ExcludeUnitsDefinition>
{
    public ExcludeUnitsProcesser(IUnitInclusionOrExclusionListProcessingDiagnostics<RawExcludeUnitsDefinition> diagnostics) : base(diagnostics) { }

    protected override ExcludeUnitsDefinition ConstructProduct(IReadOnlyList<string> items, RawExcludeUnitsDefinition definition)
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
