namespace SharpMeasures.Generators.Units.Pipeline.DerivableUnitPipeline;

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
    public readonly record struct Result(DocumentationFile Documentation, DefinedType TypeDefinition, NamedType Quantity, bool Biased,
        IEnumerable<DerivableUnitParameters> DefinedDerivations);

    public static IncrementalValuesProvider<Result> Attach(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Stage3.Result> inputProvider)
    {
        var resultsWithDiagnostics = inputProvider.Select(ExtractDefinitionsAndDiagnostics);

        context.ReportDiagnostics(resultsWithDiagnostics);
        return resultsWithDiagnostics.ExtractResult().Where(HasAnyDefinitions);
    }

    private static ResultWithDiagnostics<Result> ExtractDefinitionsAndDiagnostics(Stage3.Result input, CancellationToken token)
    {
        AExtractor<DerivableUnitParameters> derivable = DerivableUnitExtractor.Extract(input.TypeSymbol);

        Result result = new(input.Documentation, DefinedType.FromSymbol(input.TypeSymbol), input.Quantity, input.Biased,
            derivable.ValidDefinitions);

        return new ResultWithDiagnostics<Result>(result, derivable.Diagnostics);
    }

    private static bool HasAnyDefinitions(Result result) => result.DefinedDerivations.GetEnumerator().MoveNext();
}
