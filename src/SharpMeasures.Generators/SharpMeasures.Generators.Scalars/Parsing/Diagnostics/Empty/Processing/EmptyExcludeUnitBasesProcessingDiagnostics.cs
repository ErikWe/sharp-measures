namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Empty.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Empty.Processing;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeUnitBases;

internal sealed class EmptyExcludeUnitBasesProcessingDiagnostics : AEmptyUniqueItemListProcessingDiagnostics<string?, string, RawExcludeUnitBasesDefinition, ExcludeUnitBasesLocations>, IExcludeUnitBasesProcessingDiagnostics
{
    public static EmptyExcludeUnitBasesProcessingDiagnostics Instance { get; } = new();

    private EmptyExcludeUnitBasesProcessingDiagnostics() { }

    public Diagnostic? EmptyItem(IUniqueItemListProcessingContext<string> context, RawExcludeUnitBasesDefinition definition, int index) => null;
}
