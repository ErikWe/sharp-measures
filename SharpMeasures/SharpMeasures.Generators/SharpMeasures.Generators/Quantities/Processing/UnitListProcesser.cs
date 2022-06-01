namespace SharpMeasures.Generators.Quantities.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Processing.Diagnostics;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal class UnitListProcessingContext : IProcessingContext
{
    public DefinedType Type { get; }

    public UnitInterface Unit { get; }

    public UnitListProcessingContext(DefinedType type, UnitInterface unit)
    {
        Type = type;
        Unit = unit;
    }
}

internal class UnitListProcesser<TDefinition> : IReprocesser<UnitListProcessingContext, TDefinition, ProcessedUnitList>
    where TDefinition : IItemListDefinition<string>
{
    public static UnitListProcesser<TDefinition> Instance { get; } = new();

    private UnitListProcesser() { }

    public IOptionalWithDiagnostics<ProcessedUnitList> Reprocess(UnitListProcessingContext context, TDefinition definition, ProcessedUnitList product)
    {
        List<Diagnostic> allDiagnostics = new();

        for (int i = 0; i < definition.Items.Count; i++)
        {
            if (context.Unit.UnitsByName.TryGetValue(definition.Items[i], out UnitInstance unit) is false)
            {
                allDiagnostics.Add(UnitListDiagnostics<TDefinition>.TypeNotUnit(definition, i));
                continue;
            }

            product.UnitList.Add(unit);
        }

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }
}
