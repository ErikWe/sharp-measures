namespace SharpMeasures.Generators.Scalars.Parsing.IncludeBases;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System;
using System.Collections.Generic;

internal class IncludeBasesProcesser : AItemListProcesser<string, string, IItemListProcessingContext<string>, RawIncludeBasesDefinition, IncludeBasesDefinition>
{
    public IncludeBasesProcesser(IItemListProcessingDiagnostics<string, RawIncludeBasesDefinition> diagnostics) : base(diagnostics) { }

    protected override IncludeBasesDefinition ConstructProduct(IReadOnlyList<string> items, RawIncludeBasesDefinition definition)
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
