namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeBases;

internal class ExcludeBasesProcessingDiagnostics : IExcludeBasesProcessingDiagnostics
{
    public static ExcludeBasesProcessingDiagnostics Instance { get; } = new();

    private ExcludeBasesProcessingDiagnostics() { }

    public Diagnostic EmptyItemList(IProcessingContext context, RawExcludeBasesDefinition definition)
    {
        if (definition.Locations.ExplicitlySetExcludedBases)
        {
            return DiagnosticConstruction.EmptyUnitList(definition.Locations.ExcludedBasesCollection?.AsRoslynLocation());
        }

        return DiagnosticConstruction.EmptyUnitList(definition.Locations.Attribute.AsRoslynLocation());
    }

    public Diagnostic NullItem(IProcessingContext context, RawExcludeBasesDefinition definition, int index)
    {
        return DiagnosticConstruction.NullUnrecognizedUnitNameUnknownType(definition.Locations.ExcludeBasesElements[index].AsRoslynLocation());
    }

    public Diagnostic EmptyItem(IUniqueItemListProcessingContext<string> context, RawExcludeBasesDefinition definition, int index) => NullItem(context, definition, index);

    public Diagnostic DuplicateItem(IUniqueItemListProcessingContext<string> context, RawExcludeBasesDefinition definition, int index)
    {
        return DiagnosticConstruction.ExcludingAlreadyExcludedUnit(definition.Locations.ExcludeBasesElements[index].AsRoslynLocation(), definition.ExcludedBases[index]!);
    }
}
