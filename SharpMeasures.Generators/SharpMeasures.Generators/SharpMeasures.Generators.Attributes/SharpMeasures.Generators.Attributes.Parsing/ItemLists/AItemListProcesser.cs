namespace SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;

using System.Collections.Generic;
using System.Linq;

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
        if (VerifyRequiredPropertiesSet(definition) is false)
        {
            return OptionalWithDiagnostics.Empty<TProduct>();
        }

        var validity = CheckValidity(context, definition);
        IEnumerable<Diagnostic> allDiagnostics = validity.Diagnostics;

        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<TProduct>(allDiagnostics);
        }

        var processedItems = ProcessItems(context, definition);
        allDiagnostics = allDiagnostics.Concat(processedItems);

        if (DisallowEmpty && processedItems.Result.Items.Count is 0)
        {
            return OptionalWithDiagnostics.Empty<TProduct>(allDiagnostics);
        }

        return ResultWithDiagnostics.Construct(processedItems.Result, allDiagnostics);
    }

    protected virtual bool VerifyRequiredPropertiesSet(TDefinition definition)
    {
        return definition.Locations.ExplicitlySetItems;
    }

    protected abstract TProduct ConstructProduct(IReadOnlyList<TProductItem> items, TDefinition definition);
    protected abstract TProductItem UpgradeItem(TDefinitionItem item);
    protected abstract TProductItem UpgradeNullItem(TDefinitionItem? item);
    protected virtual bool DisallowNull => true;
    protected virtual bool DisallowEmpty => true;

    protected virtual IOptionalWithDiagnostics<TProductItem> ProcessItem(TContext context, TDefinition definition, int index)
    {
        if (DisallowNull && definition.Items[index] is null)
        {
            return OptionalWithDiagnostics.Empty<TProductItem>(Diagnostics.NullItem(context, definition, index));
        }

        if (definition.Items[index] is not TDefinitionItem definiteItem)
        {
            return OptionalWithDiagnostics.Result(UpgradeNullItem(definition.Items[index]));
        }

        return OptionalWithDiagnostics.Result(UpgradeItem(definiteItem));
    }

    private IResultWithDiagnostics<TProduct> ProcessItems(TContext context, TDefinition definition)
    {
        List<TProductItem> listedItems = new();
        List<Diagnostic> allDiagnostics = new();

        for (int i = 0; i < definition.Items.Count; i++)
        {
            var processedItem = ProcessItem(context, definition, i);

            allDiagnostics.AddRange(processedItem.Diagnostics);

            if (processedItem.HasResult)
            {
                listedItems.Add(processedItem.Result);
            }
        }

        TProduct product = ConstructProduct(listedItems, definition);
        return ResultWithDiagnostics.Construct(product, allDiagnostics);
    }

    private IValidityWithDiagnostics CheckValidity(TContext context, TDefinition definition)
    {
        if (DisallowEmpty && definition.Items.Count is 0)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.EmptyItemList(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
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

    public override IOptionalWithDiagnostics<TProduct> Process(TContext context, TDefinition definition)
    {
        LocallyListedItems.Clear();

        return base.Process(context, definition);
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
        var result = base.ProcessItem(context, definition, index);

        if (result.LacksResult)
        {
            return result;
        }

        if (context.ListedItems.Contains(result.Result) || LocallyListedItems.Add(result.Result) is false)
        {
            return OptionalWithDiagnostics.Empty<TProductItem>(Diagnostics.DuplicateItem(context, definition, index));
        }

        return result;
    }
}
