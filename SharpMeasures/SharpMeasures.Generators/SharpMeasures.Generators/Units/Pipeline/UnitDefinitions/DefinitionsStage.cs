namespace SharpMeasures.Generators.Units.Pipeline.UnitDefinitions;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Units.Parsing;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal static class DefinitionsStage
{
    public static IncrementalValuesProvider<DataModel> ExtractDefinitions(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<DocumentationStage.Result> inputProvider)
    {
        var resultsWithDiagnostics = inputProvider.Select(ExtractDefinitionsAndDiagnostics);

        context.ReportDiagnostics(resultsWithDiagnostics);
        return resultsWithDiagnostics.ExtractResult();
    }

    private static ResultWithDiagnostics<DataModel> ExtractDefinitionsAndDiagnostics(DocumentationStage.Result input, CancellationToken token)
    {
        AAttributeParser<UnitAliasDefinition> unitAliases = UnitAliasExtractor.Extract(input.TypeSymbol);
        AAttributeParser<DerivedUnitDefinition> derivedUnits = DerivedUnitExtractor.Extract(input.TypeSymbol);
        AAttributeParser<FixedUnitDefinition> fixedUnits = FixedUnitExtractor.Extract(input.TypeSymbol);
        AAttributeParser<OffsetUnitDefinition> offsetUnits = OffsetUnitExtractor.Extract(input.TypeSymbol);
        AAttributeParser<PrefixedUnitDefinition> prefixedUnits = PrefixedUnitExtractor.Extract(input.TypeSymbol);
        AAttributeParser<ScaledUnitDefinition> scaledUnits = ScaledUnitExtractor.Extract(input.TypeSymbol);

        var cacheableUnitAliases = unitAliases.Definitions.Select(static (x) => x.ToCacheable()).ToList();
        var cacheableDerivedUnits = derivedUnits.Definitions.Select(static (x) => x.ToCacheable()).ToList();
        var cacheableFixedUnits = fixedUnits.Definitions.Select(static (x) => x.ToCacheable()).ToList();
        var cacheableOffsetUnits = offsetUnits.Definitions.Select(static (x) => x.ToCacheable()).ToList();
        var cacheablePrefixedUnits = prefixedUnits.Definitions.Select(static (x) => x.ToCacheable()).ToList();
        var cacheableScaledUnits = scaledUnits.Definitions.Select(static (x) => x.ToCacheable()).ToList();

        DataModel result = new(DefinedType.FromSymbol(input.TypeSymbol), NamedType.FromSymbol(input.Definition.Quantity!),
            MinimalLocation.FromLocation(input.Declaration.GetLocation()), input.Definition.AllowBias, input.Documentation, cacheableUnitAliases,
            cacheableDerivedUnits, cacheableFixedUnits, cacheableOffsetUnits, cacheablePrefixedUnits, cacheableScaledUnits);

        IEnumerable<Diagnostic> allDiagnostics = unitAliases.Diagnostics.Concat(derivedUnits.Diagnostics).Concat(fixedUnits.Diagnostics)
            .Concat(offsetUnits.Diagnostics).Concat(prefixedUnits.Diagnostics).Concat(scaledUnits.Diagnostics);

        return new ResultWithDiagnostics<DataModel>(result, allDiagnostics);
    }
}
