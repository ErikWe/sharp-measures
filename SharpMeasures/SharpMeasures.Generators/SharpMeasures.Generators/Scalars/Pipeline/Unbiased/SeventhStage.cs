namespace SharpMeasures.Generators.Scalars.Pipeline.Unbiased;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Scalars;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Providers;

using System.Threading;

internal static class SeventhStage
{
    public readonly record struct Result(MarkedDeclarationSyntaxProvider.OutputData Declaration, INamedTypeSymbol TypeSymbol,
        GeneratedScalarQuantityAttributeParameters Parameters, PowerOperations PowerOperations, IncludeUnitsAttributeParameters? IncludedUnits,
        ExcludeUnitsAttributeParameters? ExcludedUnits, GeneratedUnitAttributeParameters UnitParameters, AggregateUnitInstances UnitInstances);

    public static IncrementalValuesProvider<Result> Perform(IncrementalValuesProvider<SixthStage.Result> provider)
        => provider.Select(AddUnitInstances).WhereNotNull();

    private static Result? AddUnitInstances(SixthStage.Result input, CancellationToken _)
    {
        if (input.Parameters.Unit is null)
        {
            return null;
        }

        AggregateUnitInstances unitInstances = AggregateUnitInstances.Parse(input.Parameters.Unit);

        return new Result(input.Declaration, input.TypeSymbol, input.Parameters, input.PowerOperations, input.IncludedUnits, input.ExcludedUnits,
            input.UnitParameters, unitInstances);
    }
}
