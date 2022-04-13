namespace SharpMeasures.Generators.Scalars.Pipeline;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Scalars;
using SharpMeasures.Generators.Providers;

using System.Threading;

internal static class ThirdStage
{
    public readonly record struct Result(MarkedDeclarationSyntaxProvider.OutputData Declaration, INamedTypeSymbol TypeSymbol,
        GeneratedScalarQuantityAttributeParameters Parameters);

    public static IncrementalValuesProvider<Result> Perform(IncrementalValuesProvider<SecondStage.Result> provider)
        => provider.Select(AddParameters).WhereNotNull();

    private static Result? AddParameters(SecondStage.Result input, CancellationToken _)
    {
        if (GeneratedScalarQuantityAttributeParameters.Parse(input.TypeSymbol) is GeneratedScalarQuantityAttributeParameters parameters)
        {
            return new(input.Declaration, input.TypeSymbol, parameters);
        }

        return null;
    }
}
