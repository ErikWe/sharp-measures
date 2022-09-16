namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System;
using System.Collections.Generic;

internal abstract class AForeignVectorProcesser<TRawType, TRawDefinition, TProductType, TProductDefinition>
    where TRawType : ARawVectorType<TRawDefinition>
    where TProductDefinition : IVector
{
    public Optional<TProductType> Process(TRawType rawVector)
    {
        var vector = ProcessVector(rawVector.Type, rawVector.Definition);

        if (vector.LacksResult)
        {
            return new Optional<TProductType>();
        }

        var derivations = CommonProcessing.ProcessDerivations(rawVector.Type, rawVector.Derivations);
        var constants = CommonProcessing.ProcessConstants(rawVector.Type, rawVector.Constants);
        var conversions = CommonProcessing.ProcessConversions(rawVector.Type, rawVector.Conversions);

        var includeUnitInstances = CommonProcessing.ProcessIncludeUnits(rawVector.Type, rawVector.UnitInstanceInclusions);
        var excludeUnitInstances = CommonProcessing.ProcessExcludeUnits(rawVector.Type, rawVector.UnitInstanceExclusions);

        if (includeUnitInstances.HasResult && includeUnitInstances.Result.Count > 0 && excludeUnitInstances.HasResult && excludeUnitInstances.Result.Count > 0)
        {
            excludeUnitInstances = ResultWithDiagnostics.Construct(Array.Empty<ExcludeUnitsDefinition>() as IReadOnlyList<ExcludeUnitsDefinition>);
        }

        return ProduceResult(rawVector.Type, rawVector.TypeLocation, vector.Result, derivations.Result, constants.Result, conversions.Result, includeUnitInstances.Result, excludeUnitInstances.Result);
    }

    protected abstract TProductType ProduceResult(DefinedType type, MinimalLocation typeLocation, TProductDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations,
        IReadOnlyList<VectorConstantDefinition> constants, IReadOnlyList<ConvertibleVectorDefinition> conversions, IReadOnlyList<IncludeUnitsDefinition> unitInstanceInclusions,
        IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions);

    protected abstract IOptionalWithDiagnostics<TProductDefinition> ProcessVector(DefinedType type, TRawDefinition rawDefinition);
}
