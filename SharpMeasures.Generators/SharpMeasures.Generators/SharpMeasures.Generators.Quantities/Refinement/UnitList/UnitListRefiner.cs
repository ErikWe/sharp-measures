namespace SharpMeasures.Generators.Quantities.Refinement.UnitList;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Units.UnitInstances;

using System.Collections.Generic;

public interface IUnitListRefinementDiagnostics<TDefinition, TLocations>
    where TDefinition : IItemListDefinition<string, TLocations>
    where TLocations : IItemListLocations
{
    public abstract Diagnostic? UnrecognizedUnit(IUnitListRefinementContext context, TDefinition definition, int index);
}

public interface IUnitListRefinementContext : IProcessingContext
{
    public IUnitType Unit { get; }
}

public class UnitListRefiner<TDefinition, TLocations> : IReprocesser<IUnitListRefinementContext, TDefinition, RefinedUnitListDefinition.IBuilder>
    where TDefinition : IItemListDefinition<string, TLocations>
    where TLocations : IItemListLocations
{
    private IUnitListRefinementDiagnostics<TDefinition, TLocations> Diagnostics { get; }

    public UnitListRefiner(IUnitListRefinementDiagnostics<TDefinition, TLocations> diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<RefinedUnitListDefinition.IBuilder> Reprocess(IUnitListRefinementContext context, TDefinition definition,
        RefinedUnitListDefinition.IBuilder product)
    {
        List<Diagnostic> allDiagnostics = new();

        for (var i = 0; i < definition.Items.Count; i++)
        {
            if (context.Unit.UnitsByName.TryGetValue(definition.Items[i], out IUnitInstance unit) is false)
            {
                if (Diagnostics.UnrecognizedUnit(context, definition, i) is Diagnostic diagnostics)
                {
                    allDiagnostics.Add(diagnostics);
                }

                continue;
            }

            product.AddUnit(unit);
        }

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }
}
