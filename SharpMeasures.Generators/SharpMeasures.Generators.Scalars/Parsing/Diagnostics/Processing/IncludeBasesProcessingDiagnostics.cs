namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars.Parsing.IncludeBases;

internal class IncludeBasesProcessingDiagnostics : IIncludeBasesProcessingDiagnostics
{
    public static IncludeBasesProcessingDiagnostics Instance { get; } = new();

    private IncludeBasesProcessingDiagnostics() { }

    public Diagnostic UnrecognizedInclusionStackingMode(IUniqueItemListProcessingContext<string> context, RawIncludeBasesDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedEnumValue(definition.Locations.StackingMode?.AsRoslynLocation(), definition.StackingMode);
    }

    public Diagnostic EmptyItemList(IProcessingContext context, RawIncludeBasesDefinition definition)
    {
        if (definition.Locations.ExplicitlySetIncludedBases)
        {
            return DiagnosticConstruction.EmptyUnitList(definition.Locations.IncludedBasesCollection?.AsRoslynLocation());
        }

        return DiagnosticConstruction.EmptyUnitList(definition.Locations.Attribute.AsRoslynLocation());
    }

    public Diagnostic NullItem(IProcessingContext context, RawIncludeBasesDefinition definition, int index)
    {
        return DiagnosticConstruction.NullUnrecognizedUnitNameUnknownType(definition.Locations.IncludeBasesElements[index].AsRoslynLocation());
    }

    public Diagnostic EmptyItem(IUniqueItemListProcessingContext<string> context, RawIncludeBasesDefinition definition, int index) => NullItem(context, definition, index);

    public Diagnostic DuplicateItem(IUniqueItemListProcessingContext<string> context, RawIncludeBasesDefinition definition, int index)
    {
        return DiagnosticConstruction.IncludingAlreadyIncludedUnitWithIntersection(definition.Locations.IncludeBasesElements[index].AsRoslynLocation(), definition.IncludedBases[index]!);
    }
}
