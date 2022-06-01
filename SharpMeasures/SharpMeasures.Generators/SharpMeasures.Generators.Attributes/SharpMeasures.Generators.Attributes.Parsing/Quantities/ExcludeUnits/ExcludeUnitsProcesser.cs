namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System;
using System.Collections.Generic;

public class ExcludeUnitsProcesser : AItemListProcesser<string?, string, IItemListProcessingContext<string>, RawExcludeUnitsDefinition, ExcludeUnitsDefinition>
{
    public ExcludeUnitsProcesser(IItemListDiagnostics<string, RawExcludeUnitsDefinition> diagnostics) : base(diagnostics) { }

    protected override ExcludeUnitsDefinition ConstructProduct(IReadOnlyList<string> items, RawExcludeUnitsDefinition definition)
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
