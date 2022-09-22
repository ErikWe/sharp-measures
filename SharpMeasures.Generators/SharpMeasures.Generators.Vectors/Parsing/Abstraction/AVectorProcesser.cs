namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal abstract class AVectorProcesser<TRawType, TRawDefinition, TProductType, TProductDefinition>
    where TRawType : ARawVectorType<TRawDefinition>
    where TProductDefinition : IVector
{
    public IOptionalWithDiagnostics<TProductType> Process(Optional<TRawType> rawVector, CancellationToken token)
    {
        if (token.IsCancellationRequested || rawVector.HasValue is false)
        {
            return OptionalWithDiagnostics.Empty<TProductType>();
        }

        return Process(rawVector.Value);
    }

    private IOptionalWithDiagnostics<TProductType> Process(TRawType rawVector)
    {
        var vector = ProcessVector(rawVector.Type, rawVector.Definition);

        if (vector.LacksResult)
        {
            return vector.AsEmptyOptional<TProductType>();
        }

        var unit = GetUnit(vector.Result);

        var derivations = CommonProcessing.ProcessDerivations(rawVector.Type, rawVector.Derivations);
        var constants = CommonProcessing.ProcessConstants(rawVector.Type, rawVector.Constants, unit);
        var conversions = CommonProcessing.ProcessConversions(rawVector.Type, GetOriginalQuantity(vector.Result), ConversionFromOriginalQuantitySpecified(vector.Result), ConversionToOriginalQuantitySpecified(vector.Result), rawVector.Conversions);

        var includeUnitInstances = CommonProcessing.ProcessIncludeUnitInstances(rawVector.Type, rawVector.UnitInstanceInclusions);
        var excludeUnitInstances = CommonProcessing.ProcessExcludeUnitInstances(rawVector.Type, rawVector.UnitInstanceExclusions);

        var allDiagnostics = vector.Diagnostics.Concat(derivations).Concat(constants).Concat(conversions).Concat(includeUnitInstances).Concat(excludeUnitInstances);

        if (includeUnitInstances.HasResult && includeUnitInstances.Result.Count > 0 && excludeUnitInstances.HasResult && excludeUnitInstances.Result.Count > 0)
        {
            allDiagnostics = allDiagnostics.Concat(new[] { VectorTypeDiagnostics.ContradictoryAttributes<IncludeUnitsAttribute, ExcludeUnitsAttribute>(rawVector.TypeLocation) });
            excludeUnitInstances = ResultWithDiagnostics.Construct(Array.Empty<ExcludeUnitsDefinition>() as IReadOnlyList<ExcludeUnitsDefinition>);
        }

        TProductType product = ProduceResult(rawVector.Type, rawVector.TypeLocation, vector.Result, derivations.Result, constants.Result, conversions.Result, includeUnitInstances.Result, excludeUnitInstances.Result);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    protected abstract TProductType ProduceResult(DefinedType type, MinimalLocation typeLocation, TProductDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations, IReadOnlyList<VectorConstantDefinition> constants, IReadOnlyList<ConvertibleVectorDefinition> conversions, IReadOnlyList<IncludeUnitsDefinition> unitInstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions);

    protected abstract IOptionalWithDiagnostics<TProductDefinition> ProcessVector(DefinedType type, TRawDefinition rawDefinition);

    protected abstract NamedType? GetUnit(TProductDefinition vector);
    protected abstract NamedType? GetOriginalQuantity(TProductDefinition vector);
    protected abstract bool ConversionFromOriginalQuantitySpecified(TProductDefinition vector);
    protected abstract bool ConversionToOriginalQuantitySpecified(TProductDefinition vector);
}
