namespace SharpMeasures.Generators.Attributes.Parsing.InclusionExclusion;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Diagnostics;

using System;
using System.Collections.Generic;
using System.Linq;

public interface IAssociatedInclusionExclusionRefinementDiagnostics<TItem, TIncludeDefinition, TExcludeDefinition, TLocations>
    where TIncludeDefinition : IItemListDefinition<TItem, TLocations>
    where TExcludeDefinition : IItemListDefinition<TItem, TLocations>
    where TLocations : IItemListLocations
{
    public abstract Diagnostic? ItemAlreadyIncluded(IAssociatedInclusionExclusionRefinementContext<TItem> context, TIncludeDefinition definition, int index);
    public abstract Diagnostic? ItemNotIncluded(IAssociatedInclusionExclusionRefinementContext<TItem> context, TExcludeDefinition definition, int index);
    public abstract Diagnostic? ItemNotExcluded(IAssociatedInclusionExclusionRefinementContext<TItem> context, TIncludeDefinition definition, int index);
    public abstract Diagnostic? ItemAlreadyExcluded(IAssociatedInclusionExclusionRefinementContext<TItem> context, TExcludeDefinition definition, int index);
}

public interface IAssociatedInclusionExclusionRefinementContext<TItem> : IProcessingContext
{
    public abstract NamedType AssociatedType { get; }

    public abstract InclusionPopulation<TItem> UnitInclusionPopulation { get; }
}

public abstract class AAssociatedInclusionExclusionRefiner<TItem, TIncludeDefinition, TExcludeDefinition, TDefinition, TLocations, TProduct> :
    IProcesser<IAssociatedInclusionExclusionRefinementContext<TItem>, TDefinition, TProduct>
    where TIncludeDefinition : IItemListDefinition<TItem, TLocations>
    where TExcludeDefinition : IItemListDefinition<TItem, TLocations>
    where TDefinition : IInclusionExclusion<TItem, TIncludeDefinition, TExcludeDefinition, TLocations>
    where TLocations : IItemListLocations
{
    private IAssociatedInclusionExclusionRefinementDiagnostics<TItem, TIncludeDefinition, TExcludeDefinition, TLocations> Diagnostics { get; }

    protected AAssociatedInclusionExclusionRefiner(IAssociatedInclusionExclusionRefinementDiagnostics<TItem, TIncludeDefinition, TExcludeDefinition, TLocations> diagnostics)
    {
        Diagnostics = diagnostics;
    }

    protected abstract IReadOnlyList<TItem> GetDefaultItems();
    protected abstract TProduct CreateProduct(IReadOnlyList<TItem> items);

    IOptionalWithDiagnostics<TProduct> IProcesser<IAssociatedInclusionExclusionRefinementContext<TItem>, TDefinition, TProduct>
        .Process(IAssociatedInclusionExclusionRefinementContext<TItem> context, TDefinition definition)
    {
        var processed = Process(context, definition);

        return OptionalWithDiagnostics.Result(processed.Result, processed.Diagnostics);
    }

    public IResultWithDiagnostics<TProduct> Process(IAssociatedInclusionExclusionRefinementContext<TItem> context, TDefinition definition)
    {
        if (context.UnitInclusionPopulation.TryGetValue(context.AssociatedType, out var associatedUnitInclusion) is false)
        {
            return ResultWithDiagnostics.Construct<TProduct>(CreateProduct(GetDefaultItems()));
        }

        IReadOnlyHashSet<TItem> listedItems = associatedUnitInclusion.Items;
        List<Diagnostic> allDiagnostics = new();

        if (associatedUnitInclusion.Mode is InclusionMode.Include)
        {
            if (definition.Inclusions.Any())
            {
                ProduceInclusionDiagnostics(context, listedItems, allDiagnostics, definition.Inclusions);
            }
            else
            {
                ProduceInclusionDiagnostics(context, listedItems, allDiagnostics, definition.Exclusions);
            }
        }
        else
        {
            if (definition.Inclusions.Any())
            {
                ProduceExclusionDiagnostics(context, listedItems, allDiagnostics, definition.Inclusions);
            }
            else
            {
                ProduceExclusionDiagnostics(context, listedItems, allDiagnostics, definition.Exclusions);
            }
        }

        List<TItem> includedItems = new();

        if (associatedUnitInclusion.Mode is InclusionMode.Exclude)
        {
            includedItems.AddRange(GetDefaultItems());
        }

        Action<TItem> inclusionListAction = associatedUnitInclusion.Mode is InclusionMode.Include
            ? includedItems.Add
            : (x) => includedItems.Remove(x);

        foreach (var item in associatedUnitInclusion.Items)
        {
            inclusionListAction(item);
        }

        if (definition.Inclusions.Any())
        {
            foreach (var inclusionList in definition.Inclusions)
            {
                foreach (var includedItem in inclusionList.Items)
                {
                    inclusionListAction(includedItem);
                }
            }
        }

        return ResultWithDiagnostics.Construct(CreateProduct(includedItems), allDiagnostics);
    }

    private void ProduceInclusionDiagnostics(IAssociatedInclusionExclusionRefinementContext<TItem> context, IReadOnlyHashSet<TItem> list, List<Diagnostic> allDiagnostics,
        IEnumerable<TIncludeDefinition> inclusionLists)
    {
        foreach (TIncludeDefinition inclusionList in inclusionLists)
        {
            for (int i = 0; i < inclusionList.Items.Count; i++)
            {
                if (list.Contains(inclusionList.Items[i]) && Diagnostics.ItemAlreadyIncluded(context, inclusionList, i) is Diagnostic diagnostics)
                {
                    allDiagnostics.Add(diagnostics);
                }
            }
        }
    }

    private void ProduceInclusionDiagnostics(IAssociatedInclusionExclusionRefinementContext<TItem> context, IReadOnlyHashSet<TItem> list, List<Diagnostic> allDiagnostics,
        IEnumerable<TExcludeDefinition> exclusionLists)
    {
        foreach (TExcludeDefinition exclusionList in exclusionLists)
        {
            for (int i = 0; i < exclusionList.Items.Count; i++)
            {
                if (list.Contains(exclusionList.Items[i]) is false && Diagnostics.ItemNotIncluded(context, exclusionList, i) is Diagnostic diagnostics)
                {
                    allDiagnostics.Add(diagnostics);
                }
            }
        }
    }

    private void ProduceExclusionDiagnostics(IAssociatedInclusionExclusionRefinementContext<TItem> context, IReadOnlyHashSet<TItem> list, List<Diagnostic> allDiagnostics,
        IEnumerable<TIncludeDefinition> inclusionLists)
    {
        foreach (TIncludeDefinition inclusionList in inclusionLists)
        {
            for (int i = 0; i < inclusionList.Items.Count; i++)
            {
                if (list.Contains(inclusionList.Items[i]) is false && Diagnostics.ItemNotExcluded(context, inclusionList, i) is Diagnostic diagnostics)
                {
                    allDiagnostics.Add(diagnostics);
                }
            }
        }
    }

    private void ProduceExclusionDiagnostics(IAssociatedInclusionExclusionRefinementContext<TItem> context, IReadOnlyHashSet<TItem> list, List<Diagnostic> allDiagnostics,
        IEnumerable<TExcludeDefinition> exclusionLists)
    {
        foreach (TExcludeDefinition exclusionList in exclusionLists)
        {
            for (int i = 0; i < exclusionList.Items.Count; i++)
            {
                if (list.Contains(exclusionList.Items[i]) && Diagnostics.ItemAlreadyExcluded(context, exclusionList, i) is Diagnostic diagnostics)
                {
                    allDiagnostics.Add(diagnostics);
                }
            }
        }
    }
}
