namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;

public class IncludeUnitsProcessingDiagnostics : IIncludeUnitsProcessingDiagnostics
{
    public static IncludeUnitsProcessingDiagnostics Instance { get; } = new();

    private IncludeUnitsProcessingDiagnostics() { }

    public Diagnostic UnrecognizedInclusionStackingMode(IUniqueItemListProcessingContext<string> context, RawIncludeUnitsDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedEnumValue(definition.Locations.StackingMode?.AsRoslynLocation(), definition.StackingMode);
    }

    public Diagnostic EmptyItemList(IProcessingContext context, RawIncludeUnitsDefinition definition)
    {
        if (definition.Locations.ExplicitlySetUnitInstances)
        {
            return DiagnosticConstruction.EmptyUnitList(definition.Locations.UnitInstancesCollection?.AsRoslynLocation());
        }

        return DiagnosticConstruction.EmptyUnitList(definition.Locations.Attribute.AsRoslynLocation());
    }

    public Diagnostic NullItem(IProcessingContext context, RawIncludeUnitsDefinition definition, int index)
    {
        return DiagnosticConstruction.NullUnrecognizedUnitInstanceNameUnknownType(definition.Locations.UnitInstancesElements[index].AsRoslynLocation());
    }

    public Diagnostic EmptyItem(IUniqueItemListProcessingContext<string> context, RawIncludeUnitsDefinition definition, int index) => NullItem(context, definition, index);

    public Diagnostic DuplicateItem(IUniqueItemListProcessingContext<string> context, RawIncludeUnitsDefinition definition, int index)
    {
        return DiagnosticConstruction.IncludingAlreadyIncludedUnitInstanceWithIntersection(definition.Locations.UnitInstancesElements[index].AsRoslynLocation(), definition.UnitInstances[index]!);
    }
}
