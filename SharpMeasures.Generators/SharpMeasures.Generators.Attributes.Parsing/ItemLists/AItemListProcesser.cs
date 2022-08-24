namespace SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;

using System.Collections.Generic;

public interface IItemListProcessingDiagnostics<TItem, TDefinition, TLocations>
    where TDefinition : IItemListDefinition<TItem, TLocations>
    where TLocations : IItemListLocations
{
    public abstract Diagnostic? EmptyItemList(IProcessingContext context, TDefinition definition);
    public abstract Diagnostic? NullItem(IProcessingContext context, TDefinition definition, int index);
}

public interface IUniqueItemListProcessingDiagnostics<TDefinitionItem, TProductItem, TDefinition, TLocations> : IItemListProcessingDiagnostics<TDefinitionItem, TDefinition, TLocations>
    where TDefinition : IItemListDefinition<TDefinitionItem, TLocations>
    where TLocations : IItemListLocations
{
    public abstract Diagnostic? DuplicateItem(IUniqueItemListProcessingContext<TProductItem> context, TDefinition definition, int index);
}

public interface IUniqueItemListProcessingContext<TItem> : IProcessingContext
{
    public abstract HashSet<TItem> ListedItems { get; }
}

public abstract class AItemListProcesser<TDefinitionItem, TProductItem, TContext, TDefinition, TLocations, TProduct>
    : AActionableProcesser<TContext, TDefinition, TProduct>
    where TContext : IProcessingContext
    where TDefinition : IItemListDefinition<TDefinitionItem?, TLocations>
    where TLocations : IItemListLocations
    where TProduct : IItemListDefinition<TProductItem, TLocations>
{
    private IItemListProcessingDiagnostics<TDefinitionItem?, TDefinition, TLocations> Diagnostics { get; }

    protected AItemListProcesser(IItemListProcessingDiagnostics<TDefinitionItem?, TDefinition, TLocations> diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<TProduct> Process(TContext context, TDefinition definition)
    {
        return ValidateListNotUnexpectedlyEmpty(context, definition)
            .Merge(() => ProcessItems(context, definition))
            .Validate((product) => ValidateProductListNotUnexpectedlyEmpty(product));
    }

    private IValidityWithDiagnostics ValidateListNotUnexpectedlyEmpty(TContext context, TDefinition definition)
    {
        var listUnexpectedlyEmpty = DisallowEmptyList && definition.Items.Count is 0;

        return ValidityWithDiagnostics.Conditional(listUnexpectedlyEmpty is false, () => Diagnostics.EmptyItemList(context, definition));
    }

    private IValidityWithDiagnostics ValidateProductListNotUnexpectedlyEmpty(TProduct product)
    {
        var listUnexpectedlyEmpty = DisallowEmptyList && product.Items.Count is 0;

        return ValidityWithDiagnostics.ConditionalWithoutDiagnostics(listUnexpectedlyEmpty is false);
    }

    protected abstract TProduct ProduceResult(IReadOnlyList<TProductItem> items, TDefinition definition, IReadOnlyList<int> locationMap);
    protected abstract TProductItem UpgradeItem(TDefinitionItem item);
    protected abstract TProductItem UpgradeNullItem(TDefinitionItem? item);
    protected virtual bool DisallowNull => true;
    protected virtual bool DisallowEmptyList => true;

    private IResultWithDiagnostics<TProduct> ProcessItems(TContext context, TDefinition definition)
    {
        List<TProductItem> listedItems = new();
        List<int> locationMap = new();

        List<Diagnostic> allDiagnostics = new();

        for (int i = 0; i < definition.Items.Count; i++)
        {
            var processedItem = ValidateItemNotUnexpectedlyNull(context, definition, i)
                .Merge(() => ProcessItem(context, definition, i));

            allDiagnostics.AddRange(processedItem);

            if (processedItem.HasResult)
            {
                listedItems.Add(processedItem.Result);
                locationMap.Add(i);
            }
        }

        TProduct product = ProduceResult(listedItems, definition, locationMap);
        return ResultWithDiagnostics.Construct(product, allDiagnostics);
    }

    protected virtual IOptionalWithDiagnostics<TProductItem> ProcessItem(TContext context, TDefinition definition, int index)
    {
        if (definition.Items[index] is not TDefinitionItem definiteItem)
        {
            return OptionalWithDiagnostics.Result(UpgradeNullItem(definition.Items[index]));
        }

        return OptionalWithDiagnostics.Result(UpgradeItem(definiteItem));
    }

    private IValidityWithDiagnostics ValidateItemNotUnexpectedlyNull(TContext context, TDefinition definition, int index)
    {
        var itemUnexpectedlyNull = DisallowNull && definition.Items[index] is null;

        return ValidityWithDiagnostics.Conditional(itemUnexpectedlyNull is false, () => Diagnostics.NullItem(context, definition, index));
    }
}

public abstract class AUniqueItemListProcesser<TDefinitionItem, TProductItem, TContext, TDefinition, TLocations, TProduct>
    : AItemListProcesser<TDefinitionItem, TProductItem, TContext, TDefinition, TLocations, TProduct>
    where TContext : IUniqueItemListProcessingContext<TProductItem>
    where TDefinition : IItemListDefinition<TDefinitionItem?, TLocations>
    where TLocations : IItemListLocations
    where TProduct : IItemListDefinition<TProductItem, TLocations>
{
    private IUniqueItemListProcessingDiagnostics<TDefinitionItem?, TProductItem, TDefinition, TLocations> Diagnostics { get; }
    private HashSet<TProductItem> LocallyListedItems { get; } = new();

    protected AUniqueItemListProcesser(IUniqueItemListProcessingDiagnostics<TDefinitionItem?, TProductItem, TDefinition, TLocations> diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override void OnStartProcessing(TContext context, TDefinition definition)
    {
        LocallyListedItems.Clear();
    }

    public override void OnSuccessfulProcess(TContext context, TDefinition definition, TProduct product)
    {
        foreach (var item in product.Items)
        {
            context.ListedItems.Add(item);
        }
    }

    protected override IOptionalWithDiagnostics<TProductItem> ProcessItem(TContext context, TDefinition definition, int index)
    {
        return base.ProcessItem(context, definition, index)
            .Validate((product) => ValidateItemNotDuplicate(context, definition, product, index));
    }

    private IValidityWithDiagnostics ValidateItemNotDuplicate(TContext context, TDefinition definition, TProductItem product, int index)
    {
        var itemDuplicate = context.ListedItems.Contains(product) || LocallyListedItems.Add(product) is false;

        return ValidityWithDiagnostics.Conditional(itemDuplicate is false, () => Diagnostics.DuplicateItem(context, definition, index));
    }
}
