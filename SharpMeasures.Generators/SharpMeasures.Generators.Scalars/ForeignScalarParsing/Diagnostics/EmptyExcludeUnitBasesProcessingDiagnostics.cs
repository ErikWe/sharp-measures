namespace SharpMeasures.Generators.Scalars.ForeignScalarParsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeUnitBases;

internal class EmptyExcludeUnitBasesProcessingDiagnostics : AEmptyUniqueItemListProcessingDiagnostics<string?, string, RawExcludeUnitBasesDefinition, ExcludeUnitBasesLocations>, IExcludeUnitBasesProcessingDiagnostics
{
    public static EmptyExcludeUnitBasesProcessingDiagnostics Instance { get; } = new();

    Diagnostic? IExcludeUnitBasesProcessingDiagnostics.EmptyItem(IUniqueItemListProcessingContext<string> context, RawExcludeUnitBasesDefinition definition, int index) => null;
}
