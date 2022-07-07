namespace SharpMeasures.Generators.Scalars.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Scalars.Parsing.Abstractions;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

public static class ScalarParsingStage
{
    public static ScalarGenerator Attach(IncrementalGeneratorInitializationContext context)
    {
        var baseScalars = new BaseScalarParsingStage().Attach(context);
        var specializedScalars = new SpecializedScalarParsingStage().Attach(context);

        var baseInterfaces = baseScalars.Select(ConstructInterface);
        var specializedInterfaces = specializedScalars.Select(ConstructInterface);

        var population = ScalarPopulation.Build(baseInterfaces, specializedInterfaces);

        return new(population, baseScalars, specializedScalars);
    }

    private static IBaseScalarType ConstructInterface(BaseScalarType scalarType, CancellationToken _) => scalarType;
    private static ISpecializedScalarType ConstructInterface(SpecializedScalarType scalarType, CancellationToken _) => scalarType;
}
