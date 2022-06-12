namespace SharpMeasures.Generators.Quantities.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Refinement;

using System.Diagnostics.CodeAnalysis;

public class AssociatedUnitInclusionRefinementDiagnostics<TIncludeDefinition, TExcludeDefinition> :
    IAssociatedUnitInclusionRefinementDiagnostics<TIncludeDefinition, TExcludeDefinition>
    where TIncludeDefinition : IItemListDefinition<string>
    where TExcludeDefinition : IItemListDefinition<string>
{
    [SuppressMessage("Design", "CA1000", Justification = "Property")]
    public static AssociatedUnitInclusionRefinementDiagnostics<TIncludeDefinition, TExcludeDefinition> Instance { get; } = new();

    private AssociatedUnitInclusionRefinementDiagnostics() { }

    public Diagnostic UnrecognizedUnit<TUnitList>(IAssociatedUnitInclusionRefinementContext context, TUnitList definition, int index)
        where TUnitList : IItemListDefinition<string>
    {
        return DiagnosticConstruction.TypeNotUnit(definition.Locations.ItemsElements[index].AsRoslynLocation(), definition.Items[index]);
    }

    public Diagnostic UnitAlreadyIncluded(IAssociatedUnitInclusionRefinementContext context, TIncludeDefinition definition, int index)
    {
        return DiagnosticConstruction.UnitAlreadyIncluded(definition.Locations.ItemsElements[index].AsRoslynLocation(), definition.Items[index]);
    }

    public Diagnostic UnitNotExcluded(IAssociatedUnitInclusionRefinementContext context, TIncludeDefinition definition, int index)
    {
        return DiagnosticConstruction.UnitNotExcluded(definition.Locations.ItemsElements[index].AsRoslynLocation(), definition.Items[index]);
    }

    public Diagnostic UnitAlreadyExcluded(IAssociatedUnitInclusionRefinementContext context, TExcludeDefinition definition, int index)
    {
        return DiagnosticConstruction.UnitAlreadyExcluded(definition.Locations.ItemsElements[index].AsRoslynLocation(), definition.Items[index]);
    }

    public Diagnostic UnitNotIncluded(IAssociatedUnitInclusionRefinementContext context, TExcludeDefinition definition, int index)
    {
        return DiagnosticConstruction.UnitNotExcluded(definition.Locations.ItemsElements[index].AsRoslynLocation(), definition.Items[index]);
    }
}
