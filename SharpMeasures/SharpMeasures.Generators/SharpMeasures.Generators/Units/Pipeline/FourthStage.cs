namespace SharpMeasures.Generators.Units.Pipeline;

using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Providers;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;
using System.Threading;

internal static class FourthStage
{
    public readonly record struct Result(MarkedDeclarationSyntaxProvider.OutputData Declaration, INamedTypeSymbol TypeSymbol,
        GeneratedUnitAttributeParameters Parameters, IEnumerable<DerivedUnitAttributeParameters> Derivations, AggregateUnitInstances UnitInstances);

    public static IncrementalValuesProvider<Result> Perform(IncrementalValuesProvider<ThirdStage.Result> provider)
        => provider.Select(AddDerivationsAndInstances);

    private static Result AddDerivationsAndInstances(ThirdStage.Result input, CancellationToken _)
    {
        IEnumerable<DerivedUnitAttributeParameters> derivations = DerivedUnitAttributeParameters.Parse(input.TypeSymbol);
        AggregateUnitInstances unitInstances = AggregateUnitInstances.Parse(input.TypeSymbol);

        return new Result(input.Declaration, input.TypeSymbol, input.Parameters, derivations, unitInstances);
    }
}
