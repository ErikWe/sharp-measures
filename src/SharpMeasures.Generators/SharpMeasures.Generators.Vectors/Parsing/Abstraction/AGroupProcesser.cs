namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorOperation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal interface IGroupTypeProcessingDiagnostics
{
    public abstract Diagnostic? ContradictoryUnitInstanceInclusionAndExclusion(IVectorGroup group);
}

internal abstract class AGroupProcesser<TRawType, TRawDefinition, TProductType, TProductDefinition>
    where TRawType : ARawGroupType<TRawDefinition>
    where TProductDefinition : IVectorGroup
{
    protected IVectorProcessingDiagnosticsStrategy DiagnosticsStrategy { get; }

    protected AGroupProcesser(IVectorProcessingDiagnosticsStrategy diagnosticsStrategy)
    {
        DiagnosticsStrategy = diagnosticsStrategy;
    }

    public IOptionalWithDiagnostics<TProductType> Process(Optional<TRawType> rawGroup, CancellationToken token)
    {
        if (token.IsCancellationRequested || rawGroup.HasValue is false)
        {
            return OptionalWithDiagnostics.Empty<TProductType>();
        }

        return Process(rawGroup.Value);
    }

    public IOptionalWithDiagnostics<TProductType> Process(TRawType rawGroup)
    {
        var group = ProcessVector(rawGroup.Type, rawGroup.Definition);

        if (group.LacksResult)
        {
            return group.AsEmptyOptional<TProductType>();
        }

        HashSet<(string, NamedType)> reservedOperatorSignatures = new();

        var operations = CommonProcessing.ProcessOperations(rawGroup.Type, rawGroup.Operations, reservedOperatorSignatures, DiagnosticsStrategy);
        var vectorOperations = CommonProcessing.ProcessVectorOperations(rawGroup.Type, rawGroup.VectorOperations, reservedOperatorSignatures, DiagnosticsStrategy);
        var conversions = CommonProcessing.ProcessConversions(rawGroup.Type, GetOriginalQuantity(group.Result), ConversionFromOriginalQuantitySpecified(group.Result), ConversionToOriginalQuantitySpecified(group.Result), rawGroup.Conversions, DiagnosticsStrategy);

        var includeUnitInstances = CommonProcessing.ProcessIncludeUnitInstances(rawGroup.Type, rawGroup.UnitInstanceInclusions, DiagnosticsStrategy);
        var excludeUnitInstances = CommonProcessing.ProcessExcludeUnitInstances(rawGroup.Type, rawGroup.UnitInstanceExclusions, DiagnosticsStrategy);

        var allDiagnostics = group.Diagnostics.Concat(operations).Concat(vectorOperations).Concat(conversions).Concat(includeUnitInstances).Concat(excludeUnitInstances);

        if (includeUnitInstances.HasResult && includeUnitInstances.Result.Count > 0 && excludeUnitInstances.HasResult && excludeUnitInstances.Result.Count > 0)
        {
            if (DiagnosticsStrategy.GroupTypeDiagnostics.ContradictoryUnitInstanceInclusionAndExclusion(group.Result) is Diagnostic contradictoryDiagnostics)
            {
                allDiagnostics = allDiagnostics.Concat(new[] { contradictoryDiagnostics });
            }

            excludeUnitInstances = ResultWithDiagnostics.Construct(Array.Empty<ExcludeUnitsDefinition>() as IReadOnlyList<ExcludeUnitsDefinition>);
        }

        var product = ProduceResult(rawGroup.Type, group.Result, operations.Result, vectorOperations.Result, conversions.Result, includeUnitInstances.Result, excludeUnitInstances.Result);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    protected abstract TProductType ProduceResult(DefinedType type, TProductDefinition definition, IReadOnlyList<QuantityOperationDefinition> operations, IReadOnlyList<VectorOperationDefinition> vectorOperations, IReadOnlyList<ConvertibleVectorDefinition> conversions, IReadOnlyList<IncludeUnitsDefinition> unitInstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions);

    protected abstract IOptionalWithDiagnostics<TProductDefinition> ProcessVector(DefinedType type, TRawDefinition rawDefinition);

    protected abstract NamedType? GetOriginalQuantity(TProductDefinition groyp);
    protected abstract bool ConversionFromOriginalQuantitySpecified(TProductDefinition group);
    protected abstract bool ConversionToOriginalQuantitySpecified(TProductDefinition group);
}
