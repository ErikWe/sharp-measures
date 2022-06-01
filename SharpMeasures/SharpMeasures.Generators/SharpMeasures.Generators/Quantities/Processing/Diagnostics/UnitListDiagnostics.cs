namespace SharpMeasures.Generators.Quantities.Processing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Diagnostics;

internal static class UnitListDiagnostics<TDefinition> where TDefinition : IItemListDefinition<string>
{
    public static Diagnostic TypeNotUnit(TDefinition definition, int index)
    {
        return DiagnosticConstruction.TypeNotUnit(definition.Locations.ItemsElements[index].AsRoslynLocation(), definition.Items[index]);
    }
}
