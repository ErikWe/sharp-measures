namespace SharpMeasures.Generators.Quantities.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.Abstractions;

using System.Diagnostics.CodeAnalysis;

public class UnitListProcessingDiagnostics<TDefinition, TLocations> : IUnitListProcessingDiagnostics<TDefinition, TLocations>
    where TDefinition : IItemListDefinition<string?, TLocations>
    where TLocations : IItemListLocations
{
    [SuppressMessage("Design", "CA1000", Justification = "Property")]
    public static UnitListProcessingDiagnostics<TDefinition, TLocations> Instance { get; } = new();

    private UnitListProcessingDiagnostics() { }

    public Diagnostic EmptyItemList(IProcessingContext context, TDefinition definition)
    {
        return DiagnosticConstruction.EmptyUnitList(definition.Locations.ItemsCollection?.AsRoslynLocation());
    }

    public Diagnostic NullItem(IProcessingContext context, TDefinition definition, int index)
    {
        return DiagnosticConstruction.NullUnrecognizedUnitNameUnknownType(definition.Locations.ItemsElements[index].AsRoslynLocation());
    }

    public Diagnostic EmptyItem(IUniqueItemListProcessingContext<string> context, TDefinition definition, int index)
        => NullItem(context, definition, index);

    public Diagnostic DuplicateItem(IUniqueItemListProcessingContext<string> context, TDefinition definition, int index)
    {
        return DiagnosticConstruction.UnitAlreadyListed(definition.Locations.ItemsElements[index].AsRoslynLocation(), definition.Items[index]!);
    }
}
