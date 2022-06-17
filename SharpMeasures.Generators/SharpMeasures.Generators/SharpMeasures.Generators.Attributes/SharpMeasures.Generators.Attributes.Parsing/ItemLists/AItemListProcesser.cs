﻿namespace SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;

using System.Collections.Generic;
using System.Linq;

public interface IItemListProcessingDiagnostics<TItem, TDefinition>
{
    public abstract Diagnostic? EmptyItemList(IItemListProcessingContext<TItem> context, TDefinition definition);
    public abstract Diagnostic? NullItem(IItemListProcessingContext<TItem> context, TDefinition definition, int index);
    public abstract Diagnostic? DuplicateItem(IItemListProcessingContext<TItem> context, TDefinition definition, int index);
}

public interface IItemListProcessingContext<TItem> : IProcessingContext
{
    public abstract HashSet<TItem> ListedItems { get; }
}

public abstract class AItemListProcesser<TDefinitionItem, TProductItem, TContext, TDefinition, TProduct> : AActionableProcesser<TContext, TDefinition, TProduct>
    where TContext : IItemListProcessingContext<TProductItem>
    where TDefinition : IItemListDefinition<TDefinitionItem?>
    where TProduct : IItemListDefinition<TProductItem>
{
    private IItemListProcessingDiagnostics<TProductItem, TDefinition> Diagnostics { get; }

    protected AItemListProcesser(IItemListProcessingDiagnostics<TProductItem, TDefinition> diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override void OnSuccessfulProcess(TContext context, TDefinition definition, TProduct product)
    {
        if (DisallowDuplicate is false)
        {
            return;
        }

        foreach (var item in product.Items)
        {
            context.ListedItems.Add(item);
        }
    }

    public override IOptionalWithDiagnostics<TProduct> Process(TContext context, TDefinition definition)
    {
        var validity = CheckValidity(context, definition);
        IEnumerable<Diagnostic> allDiagnostics = validity.Diagnostics;

        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<TProduct>(allDiagnostics);
        }

        var processed = DisallowDuplicate ? ProcessNonDuplicateItems(context, definition) : ProcessItems(context, definition);
        allDiagnostics = allDiagnostics.Concat(processed);

        if (DisallowEmpty && processed.Result.Items.Count is 0)
        {
            return OptionalWithDiagnostics.Empty<TProduct>(allDiagnostics);
        }

        return processed.ReplaceDiagnostics(allDiagnostics);
    }

    protected abstract TProduct ConstructProduct(IReadOnlyList<TProductItem> items, TDefinition definition);
    protected abstract TProductItem UpgradeItem(TDefinitionItem item);
    protected abstract TProductItem UpgradeNullItem(TDefinitionItem? item);
    protected virtual bool DisallowNull => true;
    protected virtual bool DisallowEmpty => true;
    protected virtual bool DisallowDuplicate => true;

    private IResultWithDiagnostics<TProduct> ProcessItems(TContext context, TDefinition definition)
    {
        List<TProductItem> listedItems = new();
        List<Diagnostic> allDiagnostics = new();

        for (int i = 0; i < definition.Items.Count; i++)
        {
            if (DisallowNull && definition.Items[i] is null)
            {
                if (Diagnostics.NullItem(context, definition, i) is Diagnostic diagnostics)
                {
                    allDiagnostics.Add(diagnostics);
                }

                continue;
            }

            if (definition.Items[i] is not TDefinitionItem definiteItem)
            {
                listedItems.Add(UpgradeNullItem(definition.Items[i]));
                continue;
            }

            listedItems.Add(UpgradeItem(definiteItem));
        }

        TProduct product = ConstructProduct(listedItems, definition);
        return ResultWithDiagnostics.Construct(product, allDiagnostics);
    }

    private IResultWithDiagnostics<TProduct> ProcessNonDuplicateItems(TContext context, TDefinition definition)
    {
        HashSet<TProductItem> locallyListedItems = new();
        List<Diagnostic> allDiagnostics = new();

        for (int i = 0; i < definition.Items.Count; i++)
        {
            if (DisallowNull && definition.Items[i] is null)
            {
                if (Diagnostics.NullItem(context, definition, i) is Diagnostic diagnostics)
                {
                    allDiagnostics.Add(diagnostics);
                }

                continue;
            }

            TProductItem upgradedItem = definition.Items[i] is TDefinitionItem definiteItem
                ? UpgradeItem(definiteItem)
                : UpgradeNullItem(definition.Items[i]);

            if (context.ListedItems.Contains(upgradedItem) || locallyListedItems.Add(upgradedItem) is false)
            {
                if (Diagnostics.DuplicateItem(context, definition, i) is Diagnostic diagnostics)
                {
                    allDiagnostics.Add(diagnostics);
                }

                continue;
            }
        }

        TProduct product = ConstructProduct(locallyListedItems.ToList(), definition);
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