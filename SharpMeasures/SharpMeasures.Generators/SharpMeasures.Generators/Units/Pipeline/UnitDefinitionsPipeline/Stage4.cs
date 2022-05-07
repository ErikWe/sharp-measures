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
    public readonly record struct Result(DefinedType TypeDefinition, NamedType Quantity, bool Biased, DocumentationFile Documentation,
        IEnumerable<UnitAliasParameters> UnitAliases, IEnumerable<DerivedUnitParameters> DerivedUnits,
        IEnumerable<FixedUnitParameters> FixedUnits, IEnumerable<OffsetUnitParameters> OffsetUnits,
        IEnumerable<PrefixedUnitParameters> PrefixedUnits, IEnumerable<ScaledUnitParameters> ScaledUnits);

    public static IncrementalValuesProvider<Result> Attach(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Stage3.Result> inputProvider)
    {
        var resultsWithDiagnostics = inputProvider.Select(ExtractDefinitionsAndDiagnostics);

        context.ReportDiagnostics(resultsWithDiagnostics);
        return resultsWithDiagnostics.ExtractResult();
    }

    private static ResultWithDiagnostics<Result> ExtractDefinitionsAndDiagnostics(Stage3.Result input, CancellationToken token)
    {
        AExtractor<UnitAliasParameters> unitAliases = UnitAliasExtractor.Extract(input.TypeSymbol);
        AExtractor<DerivedUnitParameters> derivedUnits = DerivedUnitExtractor.Extract(input.TypeSymbol);
        AExtractor<FixedUnitParameters> fixedUnits = FixedUnitExtractor.Extract(input.TypeSymbol);
        AExtractor<OffsetUnitParameters> offsetUnits = OffsetUnitExtractor.Extract(input.TypeSymbol);
        AExtractor<PrefixedUnitParameters> prefixedUnits = PrefixedUnitExtractor.Extract(input.TypeSymbol);
        AExtractor<ScaledUnitParameters> scaledUnits = ScaledUnitExtractor.Extract(input.TypeSymbol);
        
        Result result = new(DefinedType.FromSymbol(input.TypeSymbol), input.Quantity, input.Biased, input.Documentation, unitAliases.ValidDefinitions,
            derivedUnits.ValidDefinitions, fixedUnits.ValidDefinitions, offsetUnits.ValidDefinitions,
            prefixedUnits.ValidDefinitions, scaledUnits.ValidDefinitions);

        IEnumerable<Diagnostic> allDiagnostics = unitAliases.Diagnostics.Concat(derivedUnits.Diagnostics).Concat(fixedUnits.Diagnostics)
            .Concat(offsetUnits.Diagnostics).Concat(prefixedUnits.Diagnostics).Concat(scaledUnits.Diagnostics);

        return new ResultWithDiagnostics<Result>(result, allDiagnostics);
    }
}
