namespace SharpMeasures.Generators.Scalars.Pipeline.Biased;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Scalars;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Providers;

using System.Threading;

internal static class SixthStage
{
    public readonly record struct Result(MarkedDeclarationSyntaxProvider.OutputData Declaration, INamedTypeSymbol TypeSymbol,
        GeneratedScalarQuantityAttributeParameters Parameters, PowerOperations PowerOperations, IncludeUnitsAttributeParameters? IncludedUnits,
        ExcludeUnitsAttributeParameters? ExcludedUnits, GeneratedBiasedUnitAttributeParameters UnitParameters);

    public static IncrementalValuesProvider<Result> Perform(IncrementalValuesProvider<FifthStage.Result> provider)
        => provider.Select(AddUnitParameters).WhereNotNull();

    private static Result? AddUnitParameters(FifthStage.Result input, CancellationToken _)
    {
        if (input.Parameters.Unit is null
            || GeneratedBiasedUnitAttributeParameters.Parse(input.Parameters.Unit) is not GeneratedBiasedUnitAttributeParameters unitParameters)
        {
            return null;
        }

        return new Result(input.Declaration, input.TypeSymbol, input.Parameters, input.PowerOperations, input.IncludedUnits, input.ExcludedUnits, unitParameters);
    }
}
