namespace SharpMeasures.Generators.Quantities.Parsing.Abstractions;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Diagnostics;

using System;

public abstract class AUnitListProcesser<TDefinition, TProduct>
    : AUniqueItemListProcesser<string?,  string, IUniqueItemListProcessingContext<string>, TDefinition, TProduct>
    where TDefinition : IItemListDefinition<string?>
    where TProduct : IItemListDefinition<string>
{
    private IUnitListProcessingDiagnostics<TDefinition> Diagnostics { get; }

    protected AUnitListProcesser(IUnitListProcessingDiagnostics<TDefinition> diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    protected override IOptionalWithDiagnostics<string> ProcessItem(IUniqueItemListProcessingContext<string> context, TDefinition definition, int index)
    {
        if (definition.Items[index]?.Length is 0)
        {
            return OptionalWithDiagnostics.Empty<string>(Diagnostics.EmptyItem(context, definition, index));
        }

        return base.ProcessItem(context, definition, index);
    }

    protected override string UpgradeItem(string? item) => UpgradeNullItem(item);
    protected override string UpgradeNullItem(string? item) => item ?? throw new ArgumentNullException(nameof(item));
}
