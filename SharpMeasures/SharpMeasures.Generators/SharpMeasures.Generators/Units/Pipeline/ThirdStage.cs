namespace SharpMeasures.Generators.Units.Pipeline;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Providers;

using System.Threading;

internal static class ThirdStage
{
    public readonly record struct Result(MarkedDeclarationSyntaxProvider.OutputData Declaration, INamedTypeSymbol TypeSymbol,
        GeneratedUnitAttributeParameters Parameters);

    public static IncrementalValuesProvider<Result> Perform(IncrementalValuesProvider<SecondStage.Result> provider)
        => provider.Select(AddParameters).WhereNotNull();

    private static Result? AddParameters(SecondStage.Result input, CancellationToken _)
    {
        if (GeneratedUnitAttributeParameters.Parse(input.TypeSymbol) is GeneratedUnitAttributeParameters parameters)
        {
            return new(input.Declaration, input.TypeSymbol, parameters);
        }

        return null;
    }
}
