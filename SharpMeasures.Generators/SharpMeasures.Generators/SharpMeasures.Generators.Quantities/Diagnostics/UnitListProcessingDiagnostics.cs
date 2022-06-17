namespace SharpMeasures.Generators.Quantities.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.Abstractions;

using System.Diagnostics.CodeAnalysis;

public class UnitListProcessingDiagnostics<TDefinition> : IUnitInclusionOrExclusionListProcessingDiagnostics<TDefinition>
    where TDefinition : IItemListDefinition<string?>
{
    [SuppressMessage("Design", "CA1000", Justification = "Property")]
    public static UnitListProcessingDiagnostics<TDefinition> Instance { get; } = new();

    private UnitListProcessingDiagnostics() { }

    public Diagnostic EmptyItemList(IItemListProcessingContext<string> context, TDefinition definition)
    {
        return DiagnosticConstruction.EmptyUnitList(definition.Locations.ItemsCollection?.AsRoslynLocation());
    }

    public Diagnostic NullItem(IItemListProcessingContext<string> context, TDefinition definition, int index)
    {
        return DiagnosticConstruction.NullTypeNotUnit(definition.Locations.ItemsElements[index].AsRoslynLocation());
    }

    public Diagnostic DuplicateItem(IItemListProcessingContext<string> context, TDefinition definition, int index)
    {
        return DiagnosticConstruction.UnitAlreadyListed(definition.Locations.ItemsElements[index].AsRoslynLocation(), definition.Items[index]!);
    }
}
