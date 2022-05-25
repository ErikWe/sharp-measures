namespace SharpMeasures.Generators.Scalars.Pipeline.UnitsPipeline;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Quantities;
using SharpMeasures.Generators.Attributes.Parsing.Scalars;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Quantities.Extraction;
using SharpMeasures.Generators.Scalars.Extraction;
using SharpMeasures.Generators.Units.Parsing;
using SharpMeasures.Generators.Utility;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal static class ParsingStage
{
    public static IncrementalValuesProvider<DataModel> Parse(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<DocumentationStage.Result> inputProvider)
    {
        var resultsWithDiagnostics = inputProvider.Select(ParseAndExtractDiagnostics);

        context.ReportDiagnostics(resultsWithDiagnostics);
        return resultsWithDiagnostics.ExtractResult();
    }

    private static ResultWithDiagnostics<DataModel> ParseAndExtractDiagnostics(DocumentationStage.Result input, CancellationToken token)
    {
        if (input.Definition.Unit is null)
        {
            throw new NotSupportedException("Scalar had missing unit.");
        }

        AAttributeParser<IncludeBasesDefinition> includedBases = IncludeBasesExtractor.Extract(input.TypeSymbol);
        AAttributeParser<ExcludeBasesDefinition> excludedBases = ExcludeBasesExtractor.Extract(input.TypeSymbol);

        AAttributeParser<IncludeUnitsDefinition> includedUnits = IncludeUnitsExtractor.Extract(input.TypeSymbol);
        AAttributeParser<ExcludeUnitsDefinition> excludedUnits = ExcludeUnitsExtractor.Extract(input.TypeSymbol);

        CacheableIncludeBasesDefinition? cacheableIncludedBases = includedBases.Definitions.Count > 0
            ? includedBases.Definitions[0].ToCacheable()
            : null;

        CacheableExcludeBasesDefinition? cacheableExcludedBases = excludedBases.Definitions.Count > 0
            ? excludedBases.Definitions[0].ToCacheable()
            : null;

        CacheableIncludeUnitsDefinition? cacheableIncludedUnits = includedUnits.Definitions.Count > 0
            ? includedUnits.Definitions[0].ToCacheable()
            : null;

        CacheableExcludeUnitsDefinition? cacheableExcludedUnits = excludedUnits.Definitions.Count > 0
            ? excludedUnits.Definitions[0].ToCacheable()
            : null;

        IEnumerable<Diagnostic> allDiagnostics = includedBases.Diagnostics.Concat(excludedBases.Diagnostics)
            .Concat(includedUnits.Diagnostics).Concat(excludedUnits.Diagnostics);

        DataModel data = new(DefinedType.FromSymbol(input.TypeSymbol), input.Definition.ToCacheable(), GetAllUnitNames(input.Definition.Unit),
            cacheableIncludedBases, cacheableExcludedBases, cacheableIncludedUnits, cacheableExcludedUnits, input.Documentation);

        return new(data, allDiagnostics);
    }

    private static IReadOnlyList<UnitName> GetAllUnitNames(INamedTypeSymbol unitSymbol)
    {
        AAttributeParser<UnitAliasDefinition> unitAliases = UnitAliasExtractor.Extract(unitSymbol);
        AAttributeParser<DerivedUnitDefinition> derivedUnits = DerivedUnitExtractor.Extract(unitSymbol);
        AAttributeParser<FixedUnitDefinition> fixedUnits = FixedUnitExtractor.Extract(unitSymbol);
        AAttributeParser<OffsetUnitDefinition> offsetUnits = OffsetUnitExtractor.Extract(unitSymbol);
        AAttributeParser<PrefixedUnitDefinition> prefixedUnits = PrefixedUnitExtractor.Extract(unitSymbol);
        AAttributeParser<ScaledUnitDefinition> scaledUnits = ScaledUnitExtractor.Extract(unitSymbol);

        List<UnitName> unitNames = new();

        addUnitName(unitAliases.Definitions);
        addUnitName(derivedUnits.Definitions);
        addUnitName(fixedUnits.Definitions);
        addUnitName(offsetUnits.Definitions);
        addUnitName(prefixedUnits.Definitions);
        addUnitName(scaledUnits.Definitions);

        return unitNames;

        void addUnitName<T>(IReadOnlyCollection<T> definitions) where T : IUnitDefinition
        {
            foreach (T definition in definitions)
            {
                if (UnitName.FromDefinition(definition) is UnitName unitName)
                {
                    unitNames.Add(unitName);
                }
            }
        }
    }
}
