namespace SharpMeasures.Generators.Quantities.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Refinement;

using System.Diagnostics.CodeAnalysis;

public class UnitListRefinementDiagnostics<TDefinition> : IUnitListRefinementDiagnostics<TDefinition>
    where TDefinition : IItemListDefinition<string>
{
    [SuppressMessage("Design", "CA1000", Justification = "Property")]
    public static UnitListRefinementDiagnostics<TDefinition> Instance { get; } = new();

    private UnitListRefinementDiagnostics() { }

    public Diagnostic UnrecognizedUnit(IUnitListRefinementContext context, TDefinition definition, int index)
    {
        return DiagnosticConstruction.TypeNotUnit(definition.Locations.ItemsElements[index].AsRoslynLocation(), definition.Items[index]);
    }
}
