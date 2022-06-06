namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System;
using System.Collections.Generic;

public class IncludeUnitsProcesser : AItemListProcesser<string?, string, IItemListProcessingContext<string>, RawIncludeUnits, IncludeUnits>
{
    public IncludeUnitsProcesser(IItemListDiagnostics<string, RawIncludeUnits> diagnostics) : base(diagnostics) { }

    protected override IncludeUnits ConstructProduct(IReadOnlyList<string> items, RawIncludeUnits definition)
    {
        if (definition is null)
        {
            throw new ArgumentNullException(nameof(definition));
        }

        return new(items, definition.Locations);
    }

    protected override string UpgradeItem(string? item)
    {
        if (item is null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        return item;
    }
}
