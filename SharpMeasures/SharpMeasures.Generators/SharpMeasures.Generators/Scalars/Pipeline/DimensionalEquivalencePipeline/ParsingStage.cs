namespace SharpMeasures.Generators.Scalars.Pipeline.DimensionalEquivalencePipeline;

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

        var dimensionalEquivalence = DimensionalEquivalenceExtractor.ExtractForScalar(input.TypeSymbol);

        DataModel data = new(DefinedType.FromSymbol(input.TypeSymbol), );

        return new(data, dimensionalEquivalence.Diagnostics);
    }
}
