namespace SharpMeasures.Generators.Quantities.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Refinement.UnitList;

using System.Diagnostics.CodeAnalysis;

public class UnitListRefinementDiagnostics<TDefinition, TLocations> : IUnitListRefinementDiagnostics<TDefinition, TLocations>
    where TDefinition : IItemListDefinition<string, TLocations>
    where TLocations : IItemListLocations
{
    [SuppressMessage("Design", "CA1000", Justification = "Property")]
    public static UnitListRefinementDiagnostics<TDefinition, TLocations> Instance { get; } = new();

    private UnitListRefinementDiagnostics() { }

    public Diagnostic UnrecognizedUnit(IUnitListRefinementContext context, TDefinition definition, int index)
    {
        return DiagnosticConstruction.UnrecognizedUnitName(definition.Locations.ItemsElements[index].AsRoslynLocation(), definition.Items[index],
            context.Unit.Type.Name);
    }
}
