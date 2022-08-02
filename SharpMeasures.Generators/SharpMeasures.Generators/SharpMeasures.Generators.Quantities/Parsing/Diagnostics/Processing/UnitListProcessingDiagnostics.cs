namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;

public class UnitListProcessingDiagnostics : IUnitListProcessingDiagnostics
{
    public static UnitListProcessingDiagnostics Instance { get; } = new();

    private UnitListProcessingDiagnostics() { }

    public Diagnostic EmptyItemList(IProcessingContext context, RawUnitListDefinition definition)
    {
        if (definition.Locations.ExplicitlySetUnits)
        {
            return DiagnosticConstruction.EmptyUnitList(definition.Locations.UnitsCollection?.AsRoslynLocation());
        }

        return DiagnosticConstruction.EmptyUnitList(definition.Locations.Attribute.AsRoslynLocation());
    }

    public Diagnostic NullItem(IProcessingContext context, RawUnitListDefinition definition, int index)
    {
        return DiagnosticConstruction.NullUnrecognizedUnitNameUnknownType(definition.Locations.UnitsElements[index].AsRoslynLocation());
    }

    public Diagnostic EmptyItem(IUniqueItemListProcessingContext<string> context, RawUnitListDefinition definition, int index) => NullItem(context, definition, index);

    public Diagnostic DuplicateItem(IUniqueItemListProcessingContext<string> context, RawUnitListDefinition definition, int index)
    {
        return DiagnosticConstruction.UnitAlreadyListed(definition.Locations.UnitsElements[index].AsRoslynLocation(), definition.Units[index]!);
    }
}
