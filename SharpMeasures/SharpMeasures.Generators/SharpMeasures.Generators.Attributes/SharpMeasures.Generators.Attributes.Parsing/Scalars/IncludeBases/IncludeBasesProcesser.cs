namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System;
using System.Collections.Generic;

public class IncludeBasesProcesser : AItemListProcesser<string?, string, IItemListProcessingContext<string>, RawIncludeBases, IncludeBases>
{
    public IncludeBasesProcesser(IItemListDiagnostics<string, RawIncludeBases> diagnostics) : base(diagnostics) { }

    protected override IncludeBases ConstructProduct(IReadOnlyList<string> items, RawIncludeBases definition)
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
