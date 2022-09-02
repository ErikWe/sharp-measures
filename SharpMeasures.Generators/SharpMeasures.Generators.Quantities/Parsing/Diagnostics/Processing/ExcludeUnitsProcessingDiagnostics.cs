namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;

public class ExcludeUnitsProcessingDiagnostics : IExcludeUnitsProcessingDiagnostics
{
    public static ExcludeUnitsProcessingDiagnostics Instance { get; } = new();

    private ExcludeUnitsProcessingDiagnostics() { }

    public Diagnostic EmptyItemList(IProcessingContext context, RawExcludeUnitsDefinition definition)
    {
        if (definition.Locations.ExplicitlySetUnitInstances)
        {
            return DiagnosticConstruction.EmptyUnitList(definition.Locations.UnitInstancesCollection?.AsRoslynLocation());
        }

        return DiagnosticConstruction.EmptyUnitList(definition.Locations.Attribute.AsRoslynLocation());
    }

    public Diagnostic NullItem(IProcessingContext context, RawExcludeUnitsDefinition definition, int index)
    {
        return DiagnosticConstruction.NullUnrecognizedUnitInstanceNameUnknownType(definition.Locations.UnitInstancesElements[index].AsRoslynLocation());
    }

    public Diagnostic EmptyItem(IUniqueItemListProcessingContext<string> context, RawExcludeUnitsDefinition definition, int index) => NullItem(context, definition, index);

    public Diagnostic DuplicateItem(IUniqueItemListProcessingContext<string> context, RawExcludeUnitsDefinition definition, int index)
    {
        return DiagnosticConstruction.ExcludingAlreadyExcludedUnitInstance(definition.Locations.UnitInstancesElements[index].AsRoslynLocation(), definition.UnitInstances[index]!);
    }
}
