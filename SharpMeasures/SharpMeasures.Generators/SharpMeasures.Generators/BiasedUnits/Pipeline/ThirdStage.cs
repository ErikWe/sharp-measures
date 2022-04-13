namespace SharpMeasures.Generators.BiasedUnits.Pipeline;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Providers;

using System.Threading;

internal static class ThirdStage
{
    public readonly record struct Result(MarkedDeclarationSyntaxProvider.OutputData Declaration, INamedTypeSymbol TypeSymbol,
        GeneratedBiasedUnitAttributeParameters Parameters);

    public static IncrementalValuesProvider<Result> Perform(IncrementalValuesProvider<SecondStage.Result> provider)
        => provider.Select(AddParameters).WhereNotNull();

    private static Result? AddParameters(SecondStage.Result input, CancellationToken _)
    {
        if (GeneratedBiasedUnitAttributeParameters.Parse(input.TypeSymbol) is GeneratedBiasedUnitAttributeParameters parameters)
        {
            return new(input.Declaration, input.TypeSymbol, parameters);
        }

        return null;
    }
}
