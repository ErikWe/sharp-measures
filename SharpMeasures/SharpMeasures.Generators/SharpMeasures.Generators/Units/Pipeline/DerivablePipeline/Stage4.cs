namespace SharpMeasures.Generators.Units.Pipeline.DerivablePipeline;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Attributes.Parsing.Units.Caching;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;
using System.Threading;

internal static class Stage4
{
    public readonly record struct Result(IEnumerable<DocumentationFile> Documentation, DefinedType TypeDefinition,
        NamedType Quantity, bool Biased, IEnumerable<CachedDerivableUnitAttributeParameters> DefinedDerivations);

    public static IncrementalValuesProvider<Result> Perform(IncrementalValuesProvider<Stage3.Result> provider)
        => provider.Select(ExtractDerivations);

    private static Result ExtractDerivations(Stage3.Result input, CancellationToken token)
        => new(input.Documentation, input.TypeDefinition, input.Quantity, input.Biased, ExtractValidDerivations(input, token));

    private static IEnumerable<CachedDerivableUnitAttributeParameters> ExtractValidDerivations(Stage3.Result input, CancellationToken _)
    {
        IEnumerable<CachedDerivableUnitAttributeParameters> definedDerivations
            = CachedDerivableUnitAttributeParameters.From(DerivableUnitAttributeParameters.Parse(input.TypeSymbol));

        foreach (CachedDerivableUnitAttributeParameters definedDerivation in definedDerivations)
        {
            if (definedDerivation.Expression.Length > 0 && definedDerivation.Signature is not null && definedDerivation.Quantities is not null
                && definedDerivation.Signature.Count > 0)
            {
                yield return definedDerivation;
            }
        }
    }
}
