namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars.Parsing.IncludeUnitBases;

internal sealed class IncludeUnitBasesProcessingDiagnostics : IIncludeUnitBasesProcessingDiagnostics
{
    public static IncludeUnitBasesProcessingDiagnostics Instance { get; } = new();

    private IncludeUnitBasesProcessingDiagnostics() { }

    public Diagnostic UnrecognizedInclusionStackingMode(IUniqueItemListProcessingContext<string> context, RawIncludeUnitBasesDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedEnumValue(definition.Locations.StackingMode?.AsRoslynLocation(), definition.StackingMode);
    }

    public Diagnostic EmptyItemList(IProcessingContext context, RawIncludeUnitBasesDefinition definition)
    {
        if (definition.Locations.ExplicitlySetUnitInstances)
        {
            return DiagnosticConstruction.EmptyUnitList(definition.Locations.UnitInstancesCollection?.AsRoslynLocation());
        }

        return DiagnosticConstruction.EmptyUnitList(definition.Locations.Attribute.AsRoslynLocation());
    }

    public Diagnostic NullItem(IProcessingContext context, RawIncludeUnitBasesDefinition definition, int index)
    {
        return DiagnosticConstruction.NullUnrecognizedUnitInstanceNameUnknownType(definition.Locations.UnitInstancesElements[index].AsRoslynLocation());
    }

    public Diagnostic EmptyItem(IUniqueItemListProcessingContext<string> context, RawIncludeUnitBasesDefinition definition, int index) => NullItem(context, definition, index);

    public Diagnostic DuplicateItem(IUniqueItemListProcessingContext<string> context, RawIncludeUnitBasesDefinition definition, int index)
    {
        return DiagnosticConstruction.IncludingAlreadyIncludedUnitInstanceWithIntersection(definition.Locations.UnitInstancesElements[index].AsRoslynLocation(), definition.UnitInstances[index]!);
    }
}
