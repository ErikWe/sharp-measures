namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeUnitBases;

internal sealed class ExcludeUnitBasesProcessingDiagnostics : IExcludeUnitBasesProcessingDiagnostics
{
    public static ExcludeUnitBasesProcessingDiagnostics Instance { get; } = new();

    private ExcludeUnitBasesProcessingDiagnostics() { }

    public Diagnostic EmptyItemList(IProcessingContext context, RawExcludeUnitBasesDefinition definition)
    {
        if (definition.Locations.ExplicitlySetUnitInstances)
        {
            return DiagnosticConstruction.EmptyUnitList(definition.Locations.UnitInstancesCollection?.AsRoslynLocation());
        }

        return DiagnosticConstruction.EmptyUnitList(definition.Locations.Attribute.AsRoslynLocation());
    }

    public Diagnostic NullItem(IProcessingContext context, RawExcludeUnitBasesDefinition definition, int index)
    {
        return DiagnosticConstruction.NullUnrecognizedUnitInstanceNameUnknownType(definition.Locations.UnitInstancesElements[index].AsRoslynLocation());
    }

    public Diagnostic EmptyItem(IUniqueItemListProcessingContext<string> context, RawExcludeUnitBasesDefinition definition, int index) => NullItem(context, definition, index);

    public Diagnostic DuplicateItem(IUniqueItemListProcessingContext<string> context, RawExcludeUnitBasesDefinition definition, int index)
    {
        return DiagnosticConstruction.ExcludingAlreadyExcludedUnitInstance(definition.Locations.UnitInstancesElements[index].AsRoslynLocation(), definition.UnitInstances[index]!);
    }
}
