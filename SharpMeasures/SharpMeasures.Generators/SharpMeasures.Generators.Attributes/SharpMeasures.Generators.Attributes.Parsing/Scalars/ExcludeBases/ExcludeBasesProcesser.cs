namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System;
using System.Collections.Generic;

public class ExcludeBasesProcesser : AItemListProcesser<string?, string, IItemListProcessingContext<string>, RawExcludeBasesDefinition, ExcludeBasesDefinition>
{
    public ExcludeBasesProcesser(IItemListDiagnostics<string, RawExcludeBasesDefinition> diagnostics) : base(diagnostics) { }

    protected override ExcludeBasesDefinition ConstructProduct(IReadOnlyList<string> items, RawExcludeBasesDefinition definition)
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
