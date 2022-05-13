namespace SharpMeasures.Generators.Units.Pipeline.DerivableUnitPipeline;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Units.Extraction;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal static class DerivableStage
{
    public static IncrementalValuesProvider<DataModel> ExtractDerivableDefinitions(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<DocumentationStage.Result> inputProvider)
    {
        var resultsWithDiagnostics = inputProvider.Select(ExtractDefinitionsAndDiagnostics);

        context.ReportDiagnostics(resultsWithDiagnostics);
        return resultsWithDiagnostics.ExtractResult().Where(HasAnyDefinitions);
    }

    private static ResultWithDiagnostics<DataModel> ExtractDefinitionsAndDiagnostics(DocumentationStage.Result input, CancellationToken token)
    {
        AExtractor<DerivableUnitDefinition> derivable = DerivableUnitExtractor.Extract(input.TypeSymbol);

        DataModel result = new(DefinedType.FromSymbol(input.TypeSymbol), NamedType.FromSymbol(input.Definition.Quantity!),
            MinimalLocation.FromLocation(input.Declaration.GetLocation()), ToCacheableDefinitions(derivable.ValidDefinitions), input.Documentation);

        return new ResultWithDiagnostics<DataModel>(result, derivable.Diagnostics);
    }

    private static bool HasAnyDefinitions(DataModel result) => result.DefinedDerivations.Count > 0;

    private static CacheableDerivableUnitDefinition[] ToCacheableDefinitions(IReadOnlyCollection<DerivableUnitDefinition> definitions)
    {
        CacheableDerivableUnitDefinition[] cacheableDefinitions = new CacheableDerivableUnitDefinition[definitions.Count];

        int index = 0;
        foreach (DerivableUnitDefinition definition in definitions)
        {
            cacheableDefinitions[index++] = definition.ToCacheable();
        }

        return cacheableDefinitions;
    }
}
