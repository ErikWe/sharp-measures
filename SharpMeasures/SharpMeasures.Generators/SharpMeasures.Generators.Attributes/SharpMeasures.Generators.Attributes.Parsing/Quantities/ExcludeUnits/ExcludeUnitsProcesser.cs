namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System;
using System.Collections.Generic;

public class ExcludeUnitsProcesser : AItemListProcesser<string?, string, IItemListProcessingContext<string>, RawExcludeUnits, ExcludeUnits>
{
    public ExcludeUnitsProcesser(IItemListDiagnostics<string, RawExcludeUnits> diagnostics) : base(diagnostics) { }

    protected override ExcludeUnits ConstructProduct(IReadOnlyList<string> items, RawExcludeUnits definition)
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
