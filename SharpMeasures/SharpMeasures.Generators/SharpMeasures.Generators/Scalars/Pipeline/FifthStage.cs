namespace SharpMeasures.Generators.Scalars.Pipeline;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Scalars;
using SharpMeasures.Generators.Providers;

using System.Threading;

internal static class FifthStage
{
    public readonly record struct Result(MarkedDeclarationSyntaxProvider.OutputData Declaration, INamedTypeSymbol TypeSymbol,
        GeneratedScalarQuantityAttributeParameters Parameters, PowerOperations PowerOperations, IncludeUnitsAttributeParameters? IncludedUnits,
        ExcludeUnitsAttributeParameters? ExcludedUnits, IncludeBasesAttributeParameters? IncludedBases, ExcludeBasesAttributeParameters? ExcludedBases);

    public static IncrementalValuesProvider<Result> Perform(IncrementalValuesProvider<FourthStage.Result> provider)
        => provider.Select(AddIncludedAndExcludedUnits);

    private static Result AddIncludedAndExcludedUnits(FourthStage.Result input, CancellationToken _)
    {
        IncludeUnitsAttributeParameters? includedUnits = IncludeUnitsAttributeParameters.Parse(input.TypeSymbol);
        ExcludeUnitsAttributeParameters? excludedUnits = ExcludeUnitsAttributeParameters.Parse(input.TypeSymbol);
        IncludeBasesAttributeParameters? includedBases = IncludeBasesAttributeParameters.Parse(input.TypeSymbol);
        ExcludeBasesAttributeParameters? excludedBases = ExcludeBasesAttributeParameters.Parse(input.TypeSymbol);

        return new Result(input.Declaration, input.TypeSymbol, input.Parameters, input.PowerOperations, includedUnits, excludedUnits, includedBases, excludedBases);
    }
}
