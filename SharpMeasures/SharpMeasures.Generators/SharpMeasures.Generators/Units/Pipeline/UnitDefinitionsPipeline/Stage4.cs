namespace SharpMeasures.Generators.Units.Pipeline.UnitDefinitionsPipeline;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Units.Extraction;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal static class Stage4
{
    public readonly record struct Result(IEnumerable<DocumentationFile> Documentation, DefinedType TypeDefinition,
        NamedType Quantity, bool Biased, IEnumerable<UnitAliasParameters> UnitAliases,
        IEnumerable<DerivedUnitParameters> DerivedUnits, IEnumerable<FixedUnitParameters> FixedUnits,
        IEnumerable<OffsetUnitParameters> OffsetUnits, IEnumerable<PrefixedUnitParameters> PrefixedUnits,
        IEnumerable<ScaledUnitParameters> ScaledUnits);

    public static IncrementalValuesProvider<Result> Perform(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Stage3.Result> provider)
    {
        IncrementalValuesProvider<ResultWithDiagnostics<Result>> resultsWithDiagnostics = provider.Select(ExtractDefinitionsAndDiagnostics);
        IncrementalValuesProvider<Result> validResults = resultsWithDiagnostics.ExtractResult();

        context.ReportDiagnostics(resultsWithDiagnostics);
        return validResults;
    }

    private static ResultWithDiagnostics<Result> ExtractDefinitionsAndDiagnostics(Stage3.Result input, CancellationToken token)
    {
        AExtractor<UnitAliasParameters> unitAliases = UnitAliasExtractor.Extract(input.TypeSymbol);
        AExtractor<DerivedUnitParameters> derivedUnits = DerivedUnitExtractor.Extract(input.TypeSymbol);
        AExtractor<FixedUnitParameters> fixedUnits = FixedUnitExtractor.Extract(input.TypeSymbol);
        AExtractor<OffsetUnitParameters> offsetUnits = OffsetUnitExtractor.Extract(input.TypeSymbol);
        AExtractor<PrefixedUnitParameters> prefixedUnits = PrefixedUnitExtractor.Extract(input.TypeSymbol);
        AExtractor<ScaledUnitParameters> scaledUnits = ScaledUnitExtractor.Extract(input.TypeSymbol);
        
        Result result = new(input.Documentation, DefinedType.FromSymbol(input.TypeSymbol), input.Quantity, input.Biased, unitAliases.ValidDefinitions,
            derivedUnits.ValidDefinitions, fixedUnits.ValidDefinitions, offsetUnits.ValidDefinitions,
            prefixedUnits.ValidDefinitions, scaledUnits.ValidDefinitions);

        IEnumerable<Diagnostic> allDiagnostics = unitAliases.Diagnostics.Concat(derivedUnits.Diagnostics).Concat(fixedUnits.Diagnostics)
            .Concat(offsetUnits.Diagnostics).Concat(prefixedUnits.Diagnostics).Concat(scaledUnits.Diagnostics);

        return new ResultWithDiagnostics<Result>(result, allDiagnostics);
    }
}
