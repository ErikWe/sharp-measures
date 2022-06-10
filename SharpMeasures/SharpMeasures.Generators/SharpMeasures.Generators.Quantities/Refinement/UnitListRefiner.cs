namespace SharpMeasures.Generators.Quantities.Refinement;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;

public interface IUnitListRefinementDiagnostics<TDefinition>
    where TDefinition : IItemListDefinition<string>
{
    public abstract Diagnostic? UnrecognizedUnit(IUnitListRefinementContext context, TDefinition definition, int index);
}

public interface IUnitListRefinementContext : IProcessingContext
{
    public UnitInterface Unit { get; }
}

public class UnitListRefiner<TDefinition> : IReprocesser<IUnitListRefinementContext, TDefinition, RefinedUnitListDefinition.IBuilder>
    where TDefinition : IItemListDefinition<string>
{
    private IUnitListRefinementDiagnostics<TDefinition> Diagnostics { get; }

    public UnitListRefiner(IUnitListRefinementDiagnostics<TDefinition> diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<RefinedUnitListDefinition.IBuilder> Reprocess(IUnitListRefinementContext context, TDefinition definition,
        RefinedUnitListDefinition.IBuilder product)
    {
        List<Diagnostic> allDiagnostics = new();

        for (int i = 0; i < definition.Items.Count; i++)
        {
            if (context.Unit.UnitsByName.TryGetValue(definition.Items[i], out UnitInstance unit) is false)
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
