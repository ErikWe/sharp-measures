namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;

using System;
using System.Collections.Generic;

internal abstract class AForeignGroupProcesser<TRawType, TRawDefinition, TProductType, TProductDefinition>
    where TRawType : ARawGroupType<TRawDefinition>
    where TProductDefinition : IVectorGroup
{
    public Optional<TProductType> Process(TRawType rawGroup)
    {
        var group = ProcessGroup(rawGroup.Type, rawGroup.Definition);

        if (group.LacksResult)
        {
            return new Optional<TProductType>();
        }

        var derivations = CommonProcessing.ProcessDerivations(rawGroup.Type, rawGroup.Derivations);
        var conversions = CommonProcessing.ProcessConversions(rawGroup.Type, rawGroup.Conversions);

        var includeUnitInstances = CommonProcessing.ProcessIncludeUnits(rawGroup.Type, rawGroup.UnitInstanceInclusions);
        var excludeUnitInstances = CommonProcessing.ProcessExcludeUnits(rawGroup.Type, rawGroup.UnitInstanceExclusions);

        if (includeUnitInstances.HasResult && includeUnitInstances.Result.Count > 0 && excludeUnitInstances.HasResult && excludeUnitInstances.Result.Count > 0)
        {
            excludeUnitInstances = ResultWithDiagnostics.Construct(Array.Empty<ExcludeUnitsDefinition>() as IReadOnlyList<ExcludeUnitsDefinition>);
        }

        return ProduceResult(rawGroup.Type, rawGroup.TypeLocation, group.Result, derivations.Result, conversions.Result, includeUnitInstances.Result, excludeUnitInstances.Result);
    }

    protected abstract TProductType ProduceResult(DefinedType type, MinimalLocation typeLocation, TProductDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations,
        IReadOnlyList<ConvertibleVectorDefinition> conversions, IReadOnlyList<IncludeUnitsDefinition> unitInstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions);

    protected abstract IOptionalWithDiagnostics<TProductDefinition> ProcessGroup(DefinedType type, TRawDefinition rawDefinition);
}
