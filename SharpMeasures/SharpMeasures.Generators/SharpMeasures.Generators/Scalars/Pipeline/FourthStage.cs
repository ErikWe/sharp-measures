namespace SharpMeasures.Generators.Scalars.Pipeline;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Scalars;
using SharpMeasures.Generators.Providers;

using System.Threading;

internal static class FourthStage
{
    public readonly record struct Result(MarkedDeclarationSyntaxProvider.OutputData Declaration, INamedTypeSymbol TypeSymbol,
        GeneratedScalarQuantityAttributeParameters Parameters, PowerOperations PowerOperations);

    public static IncrementalValuesProvider<Result> Perform(IncrementalValuesProvider<ThirdStage.Result> provider)
        => provider.Select(AddPowerOperations);

    private static Result AddPowerOperations(ThirdStage.Result input, CancellationToken _)
    {
        PowerOperations powerOperations = PowerOperations.Parse(input.TypeSymbol);

        return new Result(input.Declaration, input.TypeSymbol, input.Parameters, powerOperations);
    }
}
