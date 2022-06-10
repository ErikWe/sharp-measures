namespace SharpMeasures.Generators.Scalars.Parsing.IncludeBases;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System;
using System.Collections.Generic;

internal class IncludeBasesProcesser : AItemListProcesser<string?, string, IItemListProcessingContext<string>, RawIncludeBasesDefinition, IncludeBasesDefinition>
{
    public IncludeBasesProcesser(IItemListProcessingDiagnostics<string, RawIncludeBasesDefinition> diagnostics) : base(diagnostics) { }

    protected override IncludeBasesDefinition ConstructProduct(IReadOnlyList<string> items, RawIncludeBasesDefinition definition)
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
