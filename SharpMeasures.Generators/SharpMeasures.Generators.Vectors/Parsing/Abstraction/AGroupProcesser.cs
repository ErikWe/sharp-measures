namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.Diagnostics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal abstract class AGroupProcesser<TRawType, TRawDefinition, TProductType, TProductDefinition>
    where TRawType : ARawGroupType<TRawDefinition>
    where TProductDefinition : IVectorGroup
{
    public IOptionalWithDiagnostics<TProductType> Process(Optional<TRawType> rawGroup, CancellationToken token)
    {
        if (token.IsCancellationRequested || rawGroup.HasValue is false)
        {
            return OptionalWithDiagnostics.Empty<TProductType>();
        }

        return Process(rawGroup.Value);
    }

    private IOptionalWithDiagnostics<TProductType> Process(TRawType rawGroup)
    {
        var vector = ProcessVector(rawGroup.Type, rawGroup.Definition);

        if (vector.LacksResult)
        {
            return vector.AsEmptyOptional<TProductType>();
        }

        var derivations = CommonProcessing.ProcessDerivations(rawGroup.Type, rawGroup.Derivations);
        var conversions = CommonProcessing.ProcessConversions(rawGroup.Type, rawGroup.Conversions);

        var includeUnitInstances = CommonProcessing.ProcessIncludeUnitInstances(rawGroup.Type, rawGroup.UnitInstanceInclusions);
        var excludeUnitInstances = CommonProcessing.ProcessExcludeUnitInstances(rawGroup.Type, rawGroup.UnitInstanceExclusions);

        var allDiagnostics = vector.Diagnostics.Concat(derivations).Concat(conversions).Concat(includeUnitInstances).Concat(excludeUnitInstances);

        if (includeUnitInstances.HasResult && includeUnitInstances.Result.Count > 0 && excludeUnitInstances.HasResult && excludeUnitInstances.Result.Count > 0)
        {
            allDiagnostics = allDiagnostics.Concat(new[] { VectorTypeDiagnostics.ContradictoryAttributes<IncludeUnitsAttribute, ExcludeUnitsAttribute>(rawGroup.TypeLocation) });
            excludeUnitInstances = ResultWithDiagnostics.Construct(Array.Empty<ExcludeUnitsDefinition>() as IReadOnlyList<ExcludeUnitsDefinition>);
        }

        TProductType product = ProduceResult(rawGroup.Type, rawGroup.TypeLocation, vector.Result, derivations.Result, conversions.Result, includeUnitInstances.Result, excludeUnitInstances.Result);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    protected abstract TProductType ProduceResult(DefinedType type, MinimalLocation typeLocation, TProductDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations, IReadOnlyList<ConvertibleVectorDefinition> conversions, IReadOnlyList<IncludeUnitsDefinition> unitInstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions);

    protected abstract IOptionalWithDiagnostics<TProductDefinition> ProcessVector(DefinedType type, TRawDefinition rawDefinition);
}
