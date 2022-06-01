namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Diagnostics;

internal class UnitListDiagnostics<TDefinition> : IItemListDiagnostics<string, TDefinition>
    where TDefinition : IItemListDefinition<string?>
{
    public static UnitListDiagnostics<TDefinition> Instance { get; } = new();

    private UnitListDiagnostics() { }

    public Diagnostic EmptyItemList(IItemListProcessingContext<string> context, TDefinition definition)
    {
        return DiagnosticConstruction.EmptyList_Unit(definition.Locations.ItemsCollection?.AsRoslynLocation());
    }

    public Diagnostic NullItem(IItemListProcessingContext<string> context, TDefinition definition, int index)
    {
        return DiagnosticConstruction.TypeNotUnit_Null(definition.Locations.ItemsElements[index].AsRoslynLocation());
    }

    public Diagnostic DuplicateItem(IItemListProcessingContext<string> context, TDefinition definition, int index)
    {
        return DiagnosticConstruction.DuplicateListing_Unit(definition.Locations.ItemsElements[index].AsRoslynLocation(), definition.Items[index]!);
    }
}
