namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using Microsoft.CodeAnalysis;

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

        if (vector.HasValue is false)
        {
            return new Optional<TProductType>();
        }

        var derivations = CommonProcessing.ProcessDerivations(rawVector.Type, rawVector.Derivations);
        var constants = CommonProcessing.ProcessConstants(rawVector.Type, rawVector.Constants);
        var conversions = CommonProcessing.ProcessConversions(rawVector.Type, rawVector.Conversions);

        var includeUnitInstances = CommonProcessing.ProcessIncludeUnits(rawVector.Type, rawVector.UnitInstanceInclusions);
        var excludeUnitInstances = CommonProcessing.ProcessExcludeUnits(rawVector.Type, rawVector.UnitInstanceExclusions);

        if (includeUnitInstances.Count > 0 && excludeUnitInstances.Count > 0)
        {
            excludeUnitInstances = Array.Empty<ExcludeUnitsDefinition>();
        }

        return ProduceResult(rawVector.Type, rawVector.TypeLocation, vector.Value, derivations, constants, conversions, includeUnitInstances, excludeUnitInstances);
    }

    protected abstract TProductType ProduceResult(DefinedType type, MinimalLocation typeLocation, TProductDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations, IReadOnlyList<VectorConstantDefinition> constants, IReadOnlyList<ConvertibleVectorDefinition> conversions,
        IReadOnlyList<IncludeUnitsDefinition> unitInstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions);

    protected abstract Optional<TProductDefinition> ProcessVector(DefinedType type, TRawDefinition rawDefinition);
}
