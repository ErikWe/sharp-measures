namespace SharpMeasures.Generators.Quantities.Refinement;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing;
using SharpMeasures.Generators.Quantities.Parsing.Abstractions;
using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;
using System.Linq;

public interface IAssociatedUnitInclusionRefinementDiagnostics<TIncludeDefinition, TExcludeDefinition>
    where TIncludeDefinition : IItemListDefinition<string>
    where TExcludeDefinition : IItemListDefinition<string>
{
    public abstract Diagnostic? UnrecognizedUnit<TUnitList>(IAssociatedUnitInclusionRefinementContext context, TUnitList definition, int index)
        where TUnitList : IItemListDefinition<string>;

    public abstract Diagnostic? UnitAlreadyIncluded(IAssociatedUnitInclusionRefinementContext context, TIncludeDefinition definition, int index);
    public abstract Diagnostic? UnitNotIncluded(IAssociatedUnitInclusionRefinementContext context, TExcludeDefinition definition, int index);
    public abstract Diagnostic? UnitNotExcluded(IAssociatedUnitInclusionRefinementContext context, TIncludeDefinition definition, int index);
    public abstract Diagnostic? UnitAlreadyExcluded(IAssociatedUnitInclusionRefinementContext context, TExcludeDefinition definition, int index);
}

public interface IAssociatedUnitInclusionRefinementContext : IProcessingContext
{
    public abstract UnitInterface Unit { get; }

    public abstract NamedType AssociatedType { get; }

    public abstract UnitInclusionPopulation UnitInclusionPopulation { get; }
}

public class AssociatedUnitInclusionRefiner<TDefinition, TIncludeDefinition, TExcludeDefinition> :
    IProcesser<IAssociatedUnitInclusionRefinementContext, TDefinition, RefinedUnitListDefinition>
    where TDefinition : IUnitListInclusionExclusion<TIncludeDefinition, TExcludeDefinition>
    where TIncludeDefinition : IItemListDefinition<string>
    where TExcludeDefinition : IItemListDefinition<string>
{
    private IAssociatedUnitInclusionRefinementDiagnostics<TIncludeDefinition, TExcludeDefinition> Diagnostics { get; }

    public AssociatedUnitInclusionRefiner(IAssociatedUnitInclusionRefinementDiagnostics<TIncludeDefinition, TExcludeDefinition> diagnostics)
    {
        Diagnostics = diagnostics;
    }

    IOptionalWithDiagnostics<RefinedUnitListDefinition> IProcesser<IAssociatedUnitInclusionRefinementContext, TDefinition, RefinedUnitListDefinition>
        .Process(IAssociatedUnitInclusionRefinementContext context, TDefinition definition)
    {
        var processed = Process(context, definition);

        return OptionalWithDiagnostics.Result(processed.Result, processed.Diagnostics);
    }

    public IResultWithDiagnostics<RefinedUnitListDefinition> Process(IAssociatedUnitInclusionRefinementContext context, TDefinition definition)
    {
        if (context.UnitInclusionPopulation.TryGetValue(context.AssociatedType, out var associatedUnitInclusion) is false)
        {
            return ResultWithDiagnostics.Construct<RefinedUnitListDefinition>(new(context.Unit.UnitsByName.Values.ToList()));
        }

        IReadOnlyHashSet<string> listedUnits = associatedUnitInclusion.ListedUnits;
        List<Diagnostic> allDiagnostics = new();

        if (associatedUnitInclusion.Mode is UnitInclusion.InclusionMode.Include)
        {
            if (definition.IncludeUnits.Any())
            {
                ProduceInclusionDiagnostics(context, listedUnits, allDiagnostics, definition.IncludeUnits);
            }
            else
            {
                ProduceInclusionDiagnostics(context, listedUnits, allDiagnostics, definition.ExcludeUnits);
            }
        }
        else
        {
            if (definition.IncludeUnits.Any())
            {
                ProduceExclusionDiagnostics(context, listedUnits, allDiagnostics, definition.IncludeUnits);
            }
            else
            {
                ProduceExclusionDiagnostics(context, listedUnits, allDiagnostics, definition.ExcludeUnits);
            }
        }

        List<UnitInstance> unitList = new();

        if (associatedUnitInclusion.Mode is UnitInclusion.InclusionMode.Exclude)
        {
            unitList.AddRange(context.Unit.UnitsByName.Values);
        }

        Action<UnitInstance> unitListAction = associatedUnitInclusion.Mode is UnitInclusion.InclusionMode.Include
            ? unitList.Add
            : (x) => unitList.Remove(x);

        foreach (var unitName in associatedUnitInclusion.ListedUnits)
        {
            if (context.Unit.UnitsByName.TryGetValue(unitName, out var unit))
            {
                unitListAction(unit);
            }
        }

        if (definition.IncludeUnits.Any())
        {
            foreach (var includeUnits in definition.IncludeUnits)
            {
                for (int i = 0; i < includeUnits.Items.Count; i++)
                {
                    if (context.Unit.UnitsByName.TryGetValue(includeUnits.Items[i], out var unit) is false)
                    {
                        if (Diagnostics.UnrecognizedUnit(context, includeUnits, i) is Diagnostic diagnostics)
                        {
                            allDiagnostics.Add(diagnostics);
                        }

                        continue;
                    }

                    unitListAction(unit);
                }
            }
        }

        return ResultWithDiagnostics.Construct(new RefinedUnitListDefinition(unitList), allDiagnostics);
    }

    private void ProduceInclusionDiagnostics(IAssociatedUnitInclusionRefinementContext context, IReadOnlyHashSet<string> list, List<Diagnostic> allDiagnostics,
        IEnumerable<TIncludeDefinition> unitInclusions)
    {
        foreach (TIncludeDefinition includeUnits in unitInclusions)
        {
            for (int i = 0; i < includeUnits.Items.Count; i++)
            {
                if (list.Contains(includeUnits.Items[i]) && Diagnostics.UnitAlreadyIncluded(context, includeUnits, i) is Diagnostic diagnostics)
                {
                    allDiagnostics.Add(diagnostics);
                }
            }
        }
    }

    private void ProduceInclusionDiagnostics(IAssociatedUnitInclusionRefinementContext context, IReadOnlyHashSet<string> list, List<Diagnostic> allDiagnostics,
        IEnumerable<TExcludeDefinition> unitExclusions)
    {
        foreach (TExcludeDefinition excludeUnits in unitExclusions)
        {
            for (int i = 0; i < excludeUnits.Items.Count; i++)
            {
                if (list.Contains(excludeUnits.Items[i]) is false && Diagnostics.UnitNotIncluded(context, excludeUnits, i) is Diagnostic diagnostics)
                {
                    allDiagnostics.Add(diagnostics);
                }
            }
        }
    }

    private void ProduceExclusionDiagnostics(IAssociatedUnitInclusionRefinementContext context, IReadOnlyHashSet<string> list, List<Diagnostic> allDiagnostics,
        IEnumerable<TIncludeDefinition> unitInclusions)
    {
        foreach (TIncludeDefinition includeUnits in unitInclusions)
        {
            for (int i = 0; i < includeUnits.Items.Count; i++)
            {
                if (list.Contains(includeUnits.Items[i]) is false && Diagnostics.UnitNotExcluded(context, includeUnits, i) is Diagnostic diagnostics)
                {
                    allDiagnostics.Add(diagnostics);
                }
            }
        }
    }

    private void ProduceExclusionDiagnostics(IAssociatedUnitInclusionRefinementContext context, IReadOnlyHashSet<string> list, List<Diagnostic> allDiagnostics,
        IEnumerable<TExcludeDefinition> unitExclusions)
    {
        foreach (TExcludeDefinition excludeUnits in unitExclusions)
        {
            for (int i = 0; i < excludeUnits.Items.Count; i++)
            {
                if (list.Contains(excludeUnits.Items[i]) && Diagnostics.UnitAlreadyExcluded(context, excludeUnits, i) is Diagnostic diagnostics)
                {
                    allDiagnostics.Add(diagnostics);
                }
            }
        }
    }
}
